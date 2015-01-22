
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using NLua;
using LuaState = KeraLua.LuaState;


//	toLuaLib_Wrap_push.cs
//	Author: Lu Zexi
//	2015-01-22


namespace toLua
{
	//tolua lib
	public partial class toLuaLib
	{
		//压入一个object变量
		public static void PushVarObject(LuaState L, object o)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			translator.Push(L, o);
		}
		
		public static void Push(LuaState L, Type t)
		{
			PushObject(L, t);
		}
		
		public static void Push(LuaState L, UnityEngine.Object obj)
		{
			PushObject(L, obj == null ? null : obj);
		}
		
		//压入一个从object派生的变量
		public static void PushObject(LuaState L, object o)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			translator.PushObject(L, o, "luaNet_metatable");
		}
		
		public static void PushValue(LuaState L, object obj)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			//translator.PushValueResult(L.luastate, obj);
			translator.PushObject(L, obj,"luaNet_metatable");
		}
		
		public static void Push(LuaState L, bool b)
		{
			LuaLib.LuaPushBoolean(L, b);        
		}
		
		public static void Push(LuaState L, string str)
		{
			LuaLib.LuaPushString(L, str);
		}
		
		public static void Push(LuaState L, char d)
		{
			LuaLib.LuaPushNumber(L, d);
		}
		
		public static void Push(LuaState L, sbyte d)
		{
			LuaLib.LuaPushNumber(L, d);
		}
		
		public static void Push(LuaState L, byte d)
		{
			LuaLib.LuaPushNumber(L, d);
		}
		
		public static void Push(LuaState L, short d)
		{
			LuaLib.LuaPushNumber(L, d);
		}
		
		public static void Push(LuaState L, ushort d)
		{
			LuaLib.LuaPushNumber(L, d);
		}
		
		public static void Push(LuaState L, int d)
		{
			LuaLib.LuaPushNumber(L, d);
		}
		
		public static void Push(LuaState L, uint d)
		{
			LuaLib.LuaPushNumber(L, d);
		}
		
		public static void Push(LuaState L, long d)
		{
			LuaLib.LuaPushNumber(L, d);
		}
		
		public static void Push(LuaState L, ulong d)
		{
			LuaLib.LuaPushNumber(L, d);
		}
		
		public static void Push(LuaState L, float d)
		{
			LuaLib.LuaPushNumber(L, d);
		}
		
		public static void Push(LuaState L, decimal d)
		{
			LuaLib.LuaPushNumber(L, (double)d);
		}
		
		public static void Push(LuaState L, double d)
		{
			LuaLib.LuaPushNumber(L, d);
		}
		
		public static void Push(LuaState L, KeraLua.LuaTag p)
		{
			LuaLib.LuaPushLightUserData(L, p);
		}
		
		public static void Push(LuaState L, ILuaGeneratedType o)
		{
			if (o == null)
			{
				LuaLib.LuaPushNil(L);
			}
			else
			{
				LuaTable table = o.LuaInterfaceGetLuaTable();
				table.Push(L);
			}
		}
		
		public static void Push(LuaState L, LuaTable lt)
		{
			if (lt == null)
			{
				LuaLib.LuaPushNil(L);            
			}
			else
			{
				lt.Push(L);
			}
		}
		
		public static void Push(LuaState L, LuaFunction func)
		{
			if (func == null)
			{
				LuaLib.LuaPushNil(L);   
			}
			else
			{
				func.Push(L);
			}
		}
		
		public static void Push(LuaState L, KeraLua.LuaNativeFunction func)
		{
			if (func == null)
			{
				LuaLib.LuaPushNil(L);
				return;
			}
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			translator.PushFunction(L, func);
		}
	}
}