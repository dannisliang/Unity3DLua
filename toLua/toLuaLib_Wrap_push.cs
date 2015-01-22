
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using NLua;


//	toLuaLib_Wrap_push.cs
//	Author: Lu Zexi
//	2015-01-22


namespace toLua
{
	//tolua lib
	public partial class toLuaLib
	{
		//压入一个object变量
		public static void PushVarObject(Lua L, object o)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L.luastate);
			translator.Push(L.luastate, o);
		}
		
		public static void Push(Lua L, Type t)
		{
			PushObject(L, t);
		}
		
		public static void Push(Lua L, UnityEngine.Object obj)
		{
			PushObject(L, obj == null ? null : obj);
		}
		
		//压入一个从object派生的变量
		public static void PushObject(Lua L, object o)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L.luastate);
			translator.PushObject(L.luastate, o, "luaNet_metatable");
		}
		
		public static void PushValue(Lua L, object obj)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L.luastate);
			//translator.PushValueResult(L.luastate, obj);
			translator.PushObject(L.luastate, obj,"luaNet_metatable");
		}
		
		public static void Push(Lua L, bool b)
		{
			LuaLib.LuaPushBoolean(L.luastate, b);        
		}
		
		public static void Push(Lua L, string str)
		{
			LuaLib.LuaPushString(L.luastate, str);
		}
		
		public static void Push(Lua L, char d)
		{
			LuaLib.LuaPushNumber(L.luastate, d);
		}
		
		public static void Push(Lua L, sbyte d)
		{
			LuaLib.LuaPushNumber(L.luastate, d);
		}
		
		public static void Push(Lua L, byte d)
		{
			LuaLib.LuaPushNumber(L.luastate, d);
		}
		
		public static void Push(Lua L, short d)
		{
			LuaLib.LuaPushNumber(L.luastate, d);
		}
		
		public static void Push(Lua L, ushort d)
		{
			LuaLib.LuaPushNumber(L.luastate, d);
		}
		
		public static void Push(Lua L, int d)
		{
			LuaLib.LuaPushNumber(L.luastate, d);
		}
		
		public static void Push(Lua L, uint d)
		{
			LuaLib.LuaPushNumber(L.luastate, d);
		}
		
		public static void Push(Lua L, long d)
		{
			LuaLib.LuaPushNumber(L.luastate, d);
		}
		
		public static void Push(Lua L, ulong d)
		{
			LuaLib.LuaPushNumber(L.luastate, d);
		}
		
		public static void Push(Lua L, float d)
		{
			LuaLib.LuaPushNumber(L.luastate, d);
		}
		
		public static void Push(Lua L, decimal d)
		{
			LuaLib.LuaPushNumber(L.luastate, (double)d);
		}
		
		public static void Push(Lua L, double d)
		{
			LuaLib.LuaPushNumber(L.luastate, d);
		}
		
		public static void Push(Lua L, KeraLua.LuaTag p)
		{
			LuaLib.LuaPushLightUserData(L.luastate, p);
		}
		
		public static void Push(Lua L, ILuaGeneratedType o)
		{
			if (o == null)
			{
				LuaLib.LuaPushNil(L.luastate);
			}
			else
			{
				LuaTable table = o.LuaInterfaceGetLuaTable();
				table.Push(L.luastate);
			}
		}
		
		public static void Push(Lua L, LuaTable lt)
		{
			if (lt == null)
			{
				LuaLib.LuaPushNil(L.luastate);            
			}
			else
			{
				lt.Push(L.luastate);
			}
		}
		
		public static void Push(Lua L, LuaFunction func)
		{
			if (func == null)
			{
				LuaLib.LuaPushNil(L.luastate);   
			}
			else
			{
				func.Push(L.luastate);
			}
		}
		
		public static void Push(Lua L, KeraLua.LuaNativeFunction func)
		{
			if (func == null)
			{
				LuaLib.LuaPushNil(L.luastate);
				return;
			}
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L.luastate);
			translator.PushFunction(L.luastate, func);
		}
	}
}