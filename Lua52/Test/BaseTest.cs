
using System;
using UnityEngine;





namespace Lua52
{
    public class BaseTest
    {
        public static void TestPush( IntPtr state )
        {
            Lua52Native.lua_pushnil(state);
            Debug.Log(Lua52Native.lua_type(state , -1));

            Lua52Native.lua_pushboolean(state, 1);
            Debug.Log(Lua52Native.lua_type(state , -1));

            Lua52Native.lua_pushnumber(state, 0.4f);
            Debug.Log(Lua52Native.lua_type(state , -1));

            Lua52Native.lua_pushstring(state , "ok");
            Debug.Log(Lua52Native.lua_type(state , -1));

            Debug.Log ( "num in stack " + Lua52Native.lua_gettop(state));
        }
    }
}
