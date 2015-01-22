
using UnityEngine;
using UniLua;

//  UniLuaEngine.cs
//  Author: Lu Zexi
//  2015-01-20

namespace UniLua
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

        private ILuaState m_cLuaState;

		public LuaEngine()
        {
            this.m_cLuaState = LuaAPI.NewState();
            this.m_cLuaState.L_OpenLibs();
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
