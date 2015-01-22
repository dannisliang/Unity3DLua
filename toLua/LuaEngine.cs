
using UnityEngine;
using NLua;

//  LuaEngine.cs
//  Author: Lu Zexi
//  2015-01-20

namespace toLua
{
    //unity lua engine
    public class LuaEngine
    {
		private static LuaEngine s_cInstance;
		public static LuaEngine sInstance
        {
            get
            {
                if(s_cInstance == null)
                {
                    s_cInstance = new LuaEngine();
                }
                return s_cInstance;
            }
        }

        private Lua m_cLuaState;

		public LuaEngine()
        {
            this.m_cLuaState = new Lua();
            this.m_cLuaState.LoadCLRPackage();

			//set __index , __newindex , __call
			LuaLib.LuaPushString(this.m_cLuaState.luastate, toLuaLib.toLuaIndex);
			LuaLib.LuaLDoString(this.m_cLuaState.luastate, toLuaLib.luaIndex);  
			LuaLib.LuaRawSet(this.m_cLuaState.luastate, (int)LuaIndexes.Registry);
			
			LuaLib.LuaPushString(this.m_cLuaState.luastate, toLuaLib.toLuaNewIndex);
			LuaLib.LuaLDoString(this.m_cLuaState.luastate, toLuaLib.luaNewIndex);
			LuaLib.LuaRawSet(this.m_cLuaState.luastate, (int)LuaIndexes.Registry);
			
			LuaLib.LuaPushString(this.m_cLuaState.luastate, toLuaLib.toLuaTableCall);
			LuaLib.LuaLDoString(this.m_cLuaState.luastate, toLuaLib.luaTableCall);
			LuaLib.LuaRawSet(this.m_cLuaState.luastate, (int)LuaIndexes.Registry);
        }

        //string libName, Type t, LuaMethod[] regs, LuaField[] fields, string baseName
        public void RegisterLib( string libNname , LuaMethod[] methods , LuaField[] fields , string baseName )
        {
            // create table

            // set metatable

            //

            // for (int i = 0; i < method.Length; i++)
            // {            
            //     this.m_cLuaState.L_SetFuncs(method[i].name);
            //     LuaDLL.lua_pushstdcallcfunction(L, method[i].func);
            //     LuaDLL.lua_rawset(L, -3);            
            // }
        }
    }

}
