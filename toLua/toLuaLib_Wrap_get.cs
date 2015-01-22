
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using NLua;

//	toLuaLib_Wrap_get.cs
//	Author: Lu Zexi
//	2015-01-22


namespace toLua
{
	//tolua lib
	public partial class toLuaLib
	{
		public static double GetNumber(Lua L, int stackPos)
		{
			if (LuaLib.LuaIsNumber(L.luastate, stackPos))
			{
				return LuaLib.LuaToNumber(L.luastate, stackPos);
			}
			
			LuaLib.LuaLError(L.luastate, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));
			return 0;
		}
		
		public static bool GetBoolean(Lua L, int stackPos)
		{
			if (LuaLib.LuaIsBoolean(L.luastate, stackPos))
			{
				return LuaLib.LuaToBoolean(L.luastate, stackPos);
			}
			
			LuaLib.LuaLError(L.luastate, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));
			return false;
		}

		public static string GetString(Lua L, int stackPos)
		{
			LuaTypes luatype = LuaLib.LuaType(L.luastate, stackPos);
			string retVal = null;
			
			if (luatype == LuaTypes.String)
			{
				retVal = LuaLib.LuaToString(L.luastate, stackPos);
			}
			else if (luatype == LuaTypes.UserData)
			{
				object obj = GetLuaObject(L, stackPos);
				
				if (obj.GetType() == typeof(string))
				{
					retVal = (string)obj;
				}
				else
				{
					retVal = obj.ToString();
				}
			}
			else if (luatype == LuaTypes.Number)
			{
				double d = LuaLib.LuaToNumber(L.luastate, stackPos);
				retVal = d.ToString();
			}
			else if (luatype == LuaTypes.Boolean)
			{
				bool b = LuaLib.LuaToBoolean(L.luastate, stackPos);
				retVal = b.ToString();
			}
			else if (luatype == LuaTypes.Nil)
			{
				return retVal;
			}
			else
			{
				LuaLib.LuaGetGlobal(L.luastate, "tostring");
				LuaLib.LuaPushValue(L.luastate, stackPos);
				LuaLib.LuaPCall(L.luastate, 1, 1 , 0);
				retVal = LuaLib.LuaToString(L.luastate, -1);
				LuaLib.LuaPop(L.luastate, 1);
			}

			if(retVal == null)
			{
				LuaLib.LuaLError(L.luastate, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));
			}

			return retVal;
		}
		
		public static LuaFunction GetFunction(Lua L, int stackPos)
		{
			LuaTypes luatype = LuaLib.LuaType(L.luastate, stackPos);
			
			if (luatype != LuaTypes.Function)
			{
				LuaLib.LuaLError(L.luastate, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));
				return null;
			}
			
			LuaLib.LuaPushValue(L.luastate, stackPos);
			return new LuaFunction(LuaLib.LuaLRef(L.luastate, LuaIndexes.Registry), L);
		}
		
		public static LuaTable GetTable(Lua L, int stackPos)
		{
			LuaTypes luatype = LuaLib.LuaType(L.luastate, stackPos);
			
			if (luatype != LuaTypes.Table)
			{
				LuaLib.LuaLError(L.luastate, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));
				return null;
			}
			
			LuaLib.LuaPushValue(L.luastate, stackPos);
			return new LuaTable(LuaLib.LuaLRef(L.luastate, LuaIndexes.Registry), L);
		}
		
		public static object GetLuaObject(Lua L, int stackPos)
		{           
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find (L.luastate);
			LuaTypes luatype = LuaLib.LuaType(L.luastate, stackPos);
			object o = null;
			
			if (luatype == LuaTypes.UserData)
			{
				o = translator.GetRawNetObject(L.luastate, stackPos);
			}
			
			return o;
		}
		
		public static T GetNetObject<T>(Lua L, int stackPos)
		{
			object obj = GetLuaObject(L, stackPos);
			
			if (obj == null || (obj.GetType() != typeof(T) && !typeof(T).IsAssignableFrom(obj.GetType())))
			{
				LuaLib.LuaLError(L.luastate, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));
			}
			
			return (T)obj;
		}
		
		public static Type GetTypeObject(Lua L, int stackPos)
		{
			object obj = GetLuaObject(L, stackPos);
			
			if (obj == null || !obj.GetType().Name.Contains("MonoType"))
			{
				LuaLib.LuaLError(L.luastate, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));
			}
			
			return (Type)obj;    
		}
	}
}