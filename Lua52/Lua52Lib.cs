using System;


//	Lua52Lib.cs
//	Author: Lu Zexi
//	2015-01-28


namespace Lua52
{
	//lua 52 library
	public class Lua52Lib
	{
		public const string toLuaIndex = "toLua_Index";			//tolua index
		public const string toLuaNewIndex = "toLua_NewIndex";	//tolua newindex
		public const string toLuaTableCall = "tolua_TableCall";	//tolua call
		
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
		private static int __gc(IntPtr luaState)
		{
//			var translator = ObjectTranslatorPool.Instance.Find (luaState);
//			int udata = Lua52Native.LuaNetRawNetObj (luaState, 1);
//			
//			if (udata != -1)
//				translator.CollectObject (udata);
			return 0;
		}
		
		//========================= public static ========================
		
		//create all of the path table
		public static void CreateTable( IntPtr L , string fullPath )
		{
			string[] path = fullPath.Split(new char[] { '.' });
			int oldTop = Lua52Native.lua_gettop(L);
			
			if (path.Length > 1)
			{
				Lua52Native.lua_getglobal(L, path[0]);
				LuaType type = (LuaType)Lua52Native.lua_type(L, -1);
				
				if (type == LuaType.LUA_TNIL)
				{
					Lua52Native.lua_pop(L, 1);
					Lua52Native.lua_createtable(L, 0, 0);
					Lua52Native.lua_pushstring(L, path[0]);
					Lua52Native.lua_pushvalue(L, -2);
					Lua52Native.lua_settable(L, Lua52Native.LUA_REGISTRYINDEX);
				}
				
				for (int i = 1; i < path.Length - 1; i++)
				{
					Lua52Native.lua_pushstring(L, path[i]);
					Lua52Native.lua_rawget(L, -2);
					
					type = (LuaType)Lua52Native.lua_type(L, -1);
					
					if (type == LuaType.LUA_TNIL)
					{
						Lua52Native.lua_pop(L, 1);
						Lua52Native.lua_createtable(L, 0, 0);
						Lua52Native.lua_pushstring(L, path[i]);
						Lua52Native.lua_pushvalue(L, -2);
						Lua52Native.lua_rawset(L, -4);
					}
				}
				
				Lua52Native.lua_pushstring(L, path[path.Length - 1]);
				Lua52Native.lua_rawget(L, -2);
				
				type = (LuaType)Lua52Native.lua_type(L, -1);
				
				if (type == LuaType.LUA_TNIL)
				{
					Lua52Native.lua_pop(L, 1);
					Lua52Native.lua_createtable(L, 0, 0);
					Lua52Native.lua_pushstring(L, path[path.Length - 1]);
					Lua52Native.lua_pushvalue(L, -2);           
					Lua52Native.lua_rawset(L, -4);
				}
			}
			else
			{
				Lua52Native.lua_getglobal(L, path[0]);
				LuaType type = (LuaType)Lua52Native.lua_type(L, -1);
				
				if (type == LuaType.LUA_TNIL)
				{
					Lua52Native.lua_pop(L, 1);
					Lua52Native.lua_createtable(L, 0, 0);
					Lua52Native.lua_pushstring(L, path[0]);
					Lua52Native.lua_pushvalue(L, -2);
					Lua52Native.lua_rawset(L, Lua52Native.LUA_REGISTRYINDEX);
				}
			}
			
			Lua52Native.lua_insert(L, oldTop + 1);
			Lua52Native.lua_settop(L, oldTop + 1);
		}
		
		//////////////////// Regist Functions ////////////////////
		
		//Regist Lib
		public static void RegisterClass( IntPtr lua , string libNname , Type type ,
		                                 LuaMethod[] methods , LuaField[] fields , string baseName )
		{
			//create table
			CreateTable(lua ,libNname);
			
			//set metatable
			Lua52Native.luaL_getmetatable (lua, type.AssemblyQualifiedName);
			if(Lua52Native.lua_isnil(lua , -1))
			{
				Lua52Native.lua_pop(lua,1);
				Lua52Native.luaL_newmetatable(lua,type.AssemblyQualifiedName);
			}
			if(!string.IsNullOrEmpty(baseName))
			{
				Lua52Native.lua_pushstring(lua , "base");
				CreateTable(lua , baseName);
				Lua52Native.lua_rawset(lua , -3);
			}
			
			//set __index __newindex __call __gc
			Lua52Native.lua_pushstring(lua, "__index");
			Lua52Native.lua_pushstring(lua, toLuaIndex);
			Lua52Native.lua_rawget(lua, Lua52Native.LUA_REGISTRYINDEX);
			Lua52Native.lua_rawset(lua, -3);
			
			Lua52Native.lua_pushstring(lua, "__newindex");
			Lua52Native.lua_pushstring(lua, toLuaNewIndex);
			Lua52Native.lua_rawget(lua, Lua52Native.LUA_REGISTRYINDEX);
			Lua52Native.lua_rawset(lua, -3);
			
			Lua52Native.lua_pushstring(lua, "__call");
			Lua52Native.lua_pushstring(lua, toLuaTableCall);
			Lua52Native.lua_rawget(lua, Lua52Native.LUA_REGISTRYINDEX);
			Lua52Native.lua_rawset(lua, -3);
			
			Lua52Native.lua_pushstring(lua, "__gc");
			Lua52Native.lua_pushcfunction(lua, new LuaNativeFunction(__gc));
			Lua52Native.lua_rawset(lua, -3);
			
			//set methods
			if(methods != null)
				for(int i = 0 ; i<methods.Length ; i++)
			{
				Lua52Native.lua_pushstring(lua , methods[i].name);
				Lua52Native.lua_pushcfunction(lua , methods[i].func);
				Lua52Native.lua_rawset(lua , -3);
			}
			
			//set fields
			if(fields != null)
				for(int i = 0 ; i<fields.Length ; i++)
			{
				Lua52Native.lua_pushstring(lua , fields[i].name);
				Lua52Native.lua_createtable(lua , 2 , 2);
				//set getter in field table
				if(fields[i].getter != null)
				{
					Lua52Native.lua_pushcfunction(lua,fields[i].getter);
					Lua52Native.lua_rawseti(lua , -2 , 1);
				}
				//set setter in field table
				if(fields[i].setter != null)
				{
					Lua52Native.lua_pushcfunction(lua , fields[i].setter);
					Lua52Native.lua_rawseti(lua , -2 , 2);
				}
				//set field table in table which create at front
				Lua52Native.lua_rawset(lua , -3);
			}
			
			//set meta table
			Lua52Native.lua_setmetatable (lua, -2);
			Lua52Native.lua_settop(lua , 0);
		}
	}

}
