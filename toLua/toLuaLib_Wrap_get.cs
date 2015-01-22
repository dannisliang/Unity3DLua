
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using NLua;
using LuaState = KeraLua.LuaState;

//	toLuaLib_Wrap_get.cs
//	Author: Lu Zexi
//	2015-01-22


namespace toLua
{
	//tolua lib
	public partial class toLuaLib
	{
		public static double GetNumber(LuaState L, int stackPos)
		{
			if (LuaLib.LuaIsNumber(L, stackPos))
			{
				return LuaLib.LuaToNumber(L, stackPos);
			}
			
			LuaLib.LuaLError(L, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));
			return 0;
		}
		
		public static bool GetBoolean(LuaState L, int stackPos)
		{
			if (LuaLib.LuaIsBoolean(L, stackPos))
			{
				return LuaLib.LuaToBoolean(L, stackPos);
			}
			
			LuaLib.LuaLError(L, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));
			return false;
		}

		public static string GetString(LuaState L, int stackPos)
		{
			LuaTypes luatype = LuaLib.LuaType(L, stackPos);
			string retVal = null;
			
			if (luatype == LuaTypes.String)
			{
				retVal = LuaLib.LuaToString(L, stackPos);
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
				double d = LuaLib.LuaToNumber(L, stackPos);
				retVal = d.ToString();
			}
			else if (luatype == LuaTypes.Boolean)
			{
				bool b = LuaLib.LuaToBoolean(L, stackPos);
				retVal = b.ToString();
			}
			else if (luatype == LuaTypes.Nil)
			{
				return retVal;
			}
			else
			{
				LuaLib.LuaGetGlobal(L, "tostring");
				LuaLib.LuaPushValue(L, stackPos);
				LuaLib.LuaPCall(L, 1, 1 , 0);
				retVal = LuaLib.LuaToString(L, -1);
				LuaLib.LuaPop(L, 1);
			}

			if(retVal == null)
			{
				LuaLib.LuaLError(L, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));
			}

			return retVal;
		}
		
//		public static LuaFunction GetFunction(LuaState L, int stackPos)
//		{
//			LuaTypes luatype = LuaLib.LuaType(L, stackPos);
//			
//			if (luatype != LuaTypes.Function)
//			{
//				LuaLib.LuaLError(L, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));
//				return null;
//			}
//			
//			LuaLib.LuaPushValue(L, stackPos);
//			return new LuaFunction(LuaLib.LuaLRef(L, LuaIndexes.Registry), L);
//		}
		
//		public static LuaTable GetTable(LuaState L, int stackPos)
//		{
//			LuaTypes luatype = LuaLib.LuaType(L, stackPos);
//			
//			if (luatype != LuaTypes.Table)
//			{
//				LuaLib.LuaLError(L, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));
//				return null;
//			}
//			
//			LuaLib.LuaPushValue(L, stackPos);
//			return new LuaTable(LuaLib.LuaLRef(L, LuaIndexes.Registry), L);
//		}
		
		public static object GetLuaObject(LuaState L, int stackPos)
		{           
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find (L);
			LuaTypes luatype = LuaLib.LuaType(L, stackPos);
			object o = null;
			
			if (luatype == LuaTypes.UserData)
			{
				o = translator.GetRawNetObject(L, stackPos);
			}
			
			return o;
		}
		
		public static T GetNetObject<T>(LuaState L, int stackPos)
		{
			object obj = GetLuaObject(L, stackPos);
			
			if (obj == null || (obj.GetType() != typeof(T) && !typeof(T).IsAssignableFrom(obj.GetType())))
			{
				LuaLib.LuaLError(L, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));
			}
			
			return (T)obj;
		}
		
		public static Type GetTypeObject(LuaState L, int stackPos)
		{
			object obj = GetLuaObject(L, stackPos);
			
			if (obj == null || !obj.GetType().Name.Contains("MonoType"))
			{
				LuaLib.LuaLError(L, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));
			}
			
			return (Type)obj;    
		}
		
		//读取object类型，object为万用类型
		public static object GetVarObject(LuaState L, int stackPos)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			return translator.GetObject(L, stackPos);
		}

		public static void SetValueObject(LuaState L, int pos, object obj)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
//			translator.SetValueObject(L, pos, obj);
			translator.PushObject(L, obj, "luaNet_metatable");
		}
	}
}