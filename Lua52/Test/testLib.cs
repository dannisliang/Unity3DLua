using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Collections;
using Lua52;



public class testLib : MonoBehaviour
{
	private IntPtr ip;

	// Use this for initialization
	void Start ()
	{
//		Debug.Log (luatest());

		Debug.Log (this.GetType().AssemblyQualifiedName);
		Debug.Log (this.GetType().Assembly.GetName());

		ip = Lua52Native.luaL_newstate ();
		Lua52Native.luaL_openlibs (ip);

		// BaseTest.TestPush(ip);

		// return;
		string str = @"
		-- require 'Main'
		local a = 233
		local b = 233
		return a+b
		";
		Lua52Native.lua_getglobal (ip, "package.path");
		LuaType ty = (LuaType)Lua52Native.lua_type (ip, -1);
		string path = "";
		Debug.Log (ty);
		if(ty == LuaType.LUA_TSTRING)
			path = Lua52Native.lua_tostring (ip, -1);
		path += Application.dataPath + "/App/Scripts/lua/?.lua";
		Lua52Native.lua_pop (ip, 1);
		Debug.Log (Lua52Native.lua_gettop (ip));
		// Lua52Native.lua_pushstring (ip,"package.path");
		Debug.Log (path);
		Lua52Native.lua_pushstring (ip, path);
		// Debug.Log (Lua52Native.lua_gettop (ip));
		Lua52Native.lua_setglobal(ip, "package.path");
		Lua52Native.lua_settop (ip,0);

		int pos = Lua52Native.luaL_loadstring (ip , str);
		Lua52Native.lua_callk (ip, 0, -1, 0, IntPtr.Zero);
		int top = Lua52Native.lua_gettop (ip);
		Debug.Log ("top : " + top);

		int isNum = Lua52Native.lua_isnumber(ip, -1);
		Debug.Log ("isNum " + isNum);
		double res = Lua52Native.lua_tonumber(ip, -1);
		Debug.Log ("result " + res);

		Lua52Native.lua_close (ip);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//
	}

}
