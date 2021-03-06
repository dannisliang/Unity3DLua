﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;





namespace Lua52
{
    public class BaseTest
    {
        public static void TestPush( IntPtr state )
        {
            Lua52Native.lua_pushnil(state);
            Debug.Log(Lua52Native.lua_type(state , -1));
			Debug.Log ("type str " + Lua52Native.luaL_typename(state , -1));

            Lua52Native.lua_pushboolean(state, 1);
            Debug.Log(Lua52Native.lua_type(state , -1));
            Debug.Log ("type str " + Lua52Native.luaL_typename(state , -1));
            Debug.Log("bool is " + Lua52Native.lua_toboolean(state , -1));

            Lua52Native.lua_pushnumber(state, 0.4);
            Debug.Log(Lua52Native.lua_type(state , -1));
            Debug.Log ("type str " + Lua52Native.luaL_typename(state , -1));
            Debug.Log("num is " + Lua52Native.lua_tonumber(state , -1));

            Lua52Native.lua_pushstring(state , "ok");
            Debug.Log(Lua52Native.lua_type(state , -1));
            Debug.Log ("type str " + Lua52Native.luaL_typename(state , -1));
            Debug.Log("str is " + Lua52Native.lua_tostring(state , -1));

            Debug.Log ( "num in stack " + Lua52Native.lua_gettop(state));
        }
    }
}
