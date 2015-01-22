
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using NLua;


//  toLuaRegister.cs
//  Author: Lu Zexi
//  2015-01-22




namespace toLua
{
    //register helper
    public partial class toLuaLib
    {
		public const string toLuaIndex = "toLua_Index";			//tolua index
		public const string toLuaNewIndex = "toLua_NewIndex";	//tolua newindex
		public const string toLuaTableCall = "tolua_TableCall";	//tolua call

		public const int LUA_REGISTRYINDEX = -10000;	//regist index
		public const int LUA_ENVIRONINDEX = -10001;		//env index
		public const int LUA_GLOBALSINDEX = -10002;		//upvalue index

		public const string luaIndex =
			@"        
        local rawget = rawget
        local getmetatable = getmetatable      
        local type = type  
        local function index(obj,name)  
            local o = obj
            repeat                      
	            local meta = getmetatable(o)
	            local v = rawget(meta, name)

	            if type(v) == 'function' then
		            return v
                elseif type(v) == 'table' then
                    local func = v[1]
                    if func ~= nil then
                        return func(obj)
                    end
                end

                o = rawget(meta,'base')
            until o == nil

            error('unknown member name '..name, 2)
            return null	        
        end
        return index";
		
		public const string luaNewIndex =
			@"
        local rawget = rawget
        local getmetatable = getmetatable        
        local function newindex(obj, name, val)
            local o = obj
            repeat
		        local meta = getmetatable(o)
	            local v = rawget(meta, name)

		        if v ~= nil then
			        local func = v[2]
                    if func ~= nil then
                        return func(obj, name, val)
                    end
                end

                o = rawget(meta, 'base')
            until o == nil
       
            error('field or property '..name..' does not exist', 2)
            return null		
        end
        return newindex";
		
		public const string luaTableCall =
			@"
        local rawget = rawget
        local getmetatable = getmetatable     

        local function call(obj, ...)
            local meta = getmetatable(obj)
            local fun = rawget(meta, 'New')
            
            if fun ~= nil then
                return fun(...)
            else
                error('unknow function __call',2)
            end
        end

        return call
    ";

		//======================== private static ==================

		//__gc override
		private static int __gc(KeraLua.LuaState luaState)
		{
			var translator = ObjectTranslatorPool.Instance.Find (luaState);
			int udata = LuaLib.LuaNetRawNetObj (luaState, 1);
			
			if (udata != -1)
				translator.CollectObject (udata);
			return 0;
		}

		//========================= public static ========================

		//create all of the path table
		public static void CreateTable( KeraLua.LuaState L , string fullPath )
		{
			string[] path = fullPath.Split(new char[] { '.' });
			int oldTop = LuaLib.LuaGetTop(L);
			
			if (path.Length > 1)
			{            
				LuaLib.LuaGetGlobal(L, path[0]);
				LuaTypes type = (LuaTypes)LuaLib.LuaType(L, -1);
				
				if (type == LuaTypes.Nil)
				{
					LuaLib.LuaPop(L, 1);
					LuaLib.LuaCreateTable(L, 0, 0);
					LuaLib.LuaPushString(L, path[0]);
					LuaLib.LuaPushValue(L, -2);
					LuaLib.LuaSetTable(L, LuaIndexes.Registry);
				}
				
				for (int i = 1; i < path.Length - 1; i++)
				{
					LuaLib.LuaPushString(L, path[i]);
					LuaLib.LuaRawGet(L, -2);
					
					type = LuaLib.LuaType(L, -1);
					
					if (type == LuaTypes.Nil)
					{
						LuaLib.LuaPop(L, 1);
						LuaLib.LuaCreateTable(L, 0, 0);
						LuaLib.LuaPushString(L, path[i]);
						LuaLib.LuaPushValue(L, -2);
						LuaLib.LuaRawSet(L, -4);
					}
				}
				
				LuaLib.LuaPushString(L, path[path.Length - 1]);
				LuaLib.LuaRawGet(L, -2);
				
				type = LuaLib.LuaType(L, -1);
				
				if (type == LuaTypes.Nil)
				{
					LuaLib.LuaPop(L, 1);
					LuaLib.LuaCreateTable(L, 0, 0);
					LuaLib.LuaPushString(L, path[path.Length - 1]);
					LuaLib.LuaPushValue(L, -2);           
					LuaLib.LuaRawSet(L, -4);
				}
			}
			else
			{
				LuaLib.LuaGetGlobal(L, path[0]);
				LuaTypes type = LuaLib.LuaType(L, -1);
				
				if (type == LuaTypes.Nil)
				{
					LuaLib.LuaPop(L, 1);
					LuaLib.LuaCreateTable(L, 0, 0);
					LuaLib.LuaPushString(L, path[0]);
					LuaLib.LuaPushValue(L, -2);
					LuaLib.LuaRawSet(L, LuaIndexes.Registry);
				}
			}
			
			LuaLib.LuaInsert(L, oldTop + 1);
			LuaLib.LuaSetTop(L, oldTop + 1);
		}

		//////////////////// Regist Functions ////////////////////

        //Regist Lib
        public static void RegisterClass( Lua lua , string libNname , Type type ,
            LuaMethod[] methods , LuaField[] fields , string baseName )
        {
			//create table
			CreateTable(lua.luastate ,libNname);

			//set metatable
			LuaLib.LuaLGetMetatable (lua.luastate, type.AssemblyQualifiedName);
			if(LuaLib.LuaIsNil(lua.luastate , -1))
			{
				LuaLib.LuaPop(lua.luastate,1);
				LuaLib.LuaLNewMetatable(lua.luastate,type.AssemblyQualifiedName);
			}
			if(!string.IsNullOrEmpty(baseName))
			{
				LuaLib.LuaPushString(lua.luastate , "base");
				CreateTable(lua.luastate , baseName);
				LuaLib.LuaRawSet(lua.luastate , -3);
			}

			//set __index __newindex __call __gc
			LuaLib.LuaPushString(lua.luastate, "__index");
			LuaLib.LuaPushString(lua.luastate, toLuaIndex);
			LuaLib.LuaRawGet(lua.luastate, (int)LuaIndexes.Registry);
			LuaLib.LuaRawSet(lua.luastate, -3);
			
			LuaLib.LuaPushString(lua.luastate, "__newindex");
			LuaLib.LuaPushString(lua.luastate, toLuaNewIndex);
			LuaLib.LuaRawGet(lua.luastate, (int)LuaIndexes.Registry);        
			LuaLib.LuaRawSet(lua.luastate, -3);
			
			LuaLib.LuaPushString(lua.luastate, "__call");
			LuaLib.LuaPushString(lua.luastate, toLuaTableCall);
			LuaLib.LuaRawGet(lua.luastate, (int)LuaIndexes.Registry);
			LuaLib.LuaRawSet(lua.luastate, -3);

			LuaLib.LuaPushString(lua.luastate, "__gc");
			LuaLib.LuaPushStdCallCFunction(lua.luastate, new KeraLua.LuaNativeFunction(__gc));
			LuaLib.LuaRawSet(lua.luastate, -3);

            //set methods
			for(int i = 0 ; i<methods.Length ; i++)
			{
				LuaLib.LuaPushString(lua.luastate , methods[i].name);
				LuaLib.LuaPushStdCallCFunction(lua.luastate , methods[i].func);
				LuaLib.LuaRawSet(lua.luastate , -3);
			}
			//set fields
			for(int i = 0 ; i<fields.Length ; i++)
			{
				LuaLib.LuaPushString(lua.luastate , fields[i].name);
				LuaLib.LuaCreateTable(lua.luastate , 2 , 2);
				//set getter in field table
				if(fields[i].getter != null)
				{
					LuaLib.LuaPushStdCallCFunction(lua.luastate,fields[i].getter);
					LuaLib.LuaRawSetI(lua.luastate , -2 , 1);
				}
				//set setter in field table
				if(fields[i].setter != null)
				{
					LuaLib.LuaPushStdCallCFunction(lua.luastate , fields[i].setter);
					LuaLib.LuaRawSetI(lua.luastate , -2 , 2);
				}
				//set field table in table which create at front
				LuaLib.LuaRawSet(lua.luastate , -3);
			}

			//set meta table
			LuaLib.LuaSetMetatable (lua.luastate, -2);
			LuaLib.LuaSetTop(lua.luastate , 0);
        }
    }
}
