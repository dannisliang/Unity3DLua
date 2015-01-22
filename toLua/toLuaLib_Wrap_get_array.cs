using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using NLua;
using LuaState = KeraLua.LuaState;

//	toLuaLib_Wrap_get_array.cs
//	Author: Lu Zexi
//	2015-01-22


namespace toLua
{
	//tolua lib
	public partial class toLuaLib
	{
		public static object[] GetParamsObject(LuaState L, int stackPos, int count)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			List<object> list = new List<object>();    
			object obj = null;    
			
			while (count > 0)
			{
				//LuaTypes luatype = LuaLib.lua_type(L, stackPos);
				obj = translator.GetObject(L, stackPos);
				
				++stackPos;
				--count;
				
				if (obj != null)
				{
					list.Add(obj);
				}
				else
				{
					LuaLib.LuaLError(L, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));
					break;
				}
			} 
			
			return list.ToArray();
		}
		
		public static T[] GetParamsObject<T>(LuaState L, int stackPos, int count)
		{
			List<T> list = new List<T>();        
			T obj = default(T);
			
			while (count > 0)
			{
				obj = (T)GetLuaObject(L, stackPos);                        
				
				++stackPos;
				--count;
				
				if (obj != null && obj.GetType() == typeof(T))
				{
					list.Add(obj);
				}
				else
				{
					LuaLib.LuaLError(L, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));
					break;
				}
			} 
			
			return list.ToArray();
		}
		
		public static T[] GetArrayObject<T>(LuaState L, int stackPos)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes luatype = LuaLib.LuaType(L, stackPos);
			
			if (luatype == LuaTypes.Table)
			{
				int index = 1;
				T val = default(T);
				List<T> list = new List<T>();
				LuaLib.LuaPushValue(L, stackPos);
				
				do
				{                
					LuaLib.LuaRawGetI(L, -1, index);
					luatype = LuaLib.LuaType(L, -1);
					
					if (luatype == LuaTypes.Nil)
					{
						return list.ToArray(); ;
					}
					else if (luatype != LuaTypes.UserData)
					{
						break;
					}
					
					val = (T)translator.GetRawNetObject(L, -1);
					list.Add(val);
					LuaLib.LuaPop(L, 1);
					++index;
				} while (true);            
			}
			else if (luatype == LuaTypes.UserData)
			{
				T[] ret = GetNetObject<T[]>(L, stackPos);
				
				if (ret != null)
				{
					return (T[])ret;
				}            
			}
			
			LuaLib.LuaLError(L, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));
			return null;
		}

		public static string[] GetParamsString(LuaState L, int stackPos, int count)
		{
			List<string> list = new List<string>();
			string obj = null;
			
			while (count > 0)
			{
				obj = GetString(L, stackPos);
				++stackPos;
				--count;
				
				if (obj == null)
				{
					LuaLib.LuaLError(L, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));   
					break;
				}
				
				list.Add(obj);
			} 
			
			return list.ToArray();
		}
		
		public static string[] GetArrayString(LuaState L, int stackPos)
		{        
			LuaTypes luatype = LuaLib.LuaType(L, stackPos);
			
			if (luatype == LuaTypes.Table)
			{
				int index = 1;
				string retVal = null;
				List<string> list = new List<string>();
				LuaLib.LuaPushValue(L, stackPos);
				
				while(true)
				{                
					LuaLib.LuaRawGetI(L, -1, index);
					luatype = LuaLib.LuaType(L, -1);
					
					if (luatype == LuaTypes.Nil)
					{
						return list.ToArray();
					}
					else
					{
						retVal = GetString(L, -1);
					}
					
					list.Add(retVal);
					LuaLib.LuaPop(L, 1);
					++index;
				}
			}
			else if (luatype == LuaTypes.UserData)
			{
				string[] ret = GetNetObject<string[]>(L, stackPos);
				
				if (ret != null)
				{
					return (string[])ret;
				}
			}
			
			LuaLib.LuaLError(L, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));   
			return null;
		}
		
		public static T[] GetArrayNumber<T>(LuaState L, int stackPos)
		{
			LuaTypes luatype = LuaLib.LuaType(L, stackPos);
			
			if (luatype == LuaTypes.Table)
			{
				int index = 1;
				T ret = default(T);
				List<T> list = new List<T>();
				LuaLib.LuaPushValue(L, stackPos);
				
				while(true)
				{
					LuaLib.LuaRawGetI(L, -1, index);
					luatype = LuaLib.LuaType(L, -1);
					
					if (luatype == LuaTypes.Nil)
					{
						return list.ToArray();
					}
					else if (luatype != LuaTypes.Number)
					{
						break;
					}
					
					ret = (T)Convert.ChangeType(LuaLib.LuaToNumber(L, -1), typeof(T));
					list.Add(ret);
					LuaLib.LuaPop(L, 1);
					++index;
				}
			}
			else if (luatype == LuaTypes.UserData)
			{
				T[] ret = GetNetObject<T[]>(L, stackPos);
				
				if (ret != null)
				{
					return (T[])ret;
				}            
			}
			
			LuaLib.LuaLError(L, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));   
			return null;
		}
		
		public static bool[] GetArrayBool(LuaState L, int stackPos)
		{
			LuaTypes luatype = LuaLib.LuaType(L, stackPos);
			
			if (luatype == LuaTypes.Table)
			{
				int index = 1;            
				List<bool> list = new List<bool>();
				LuaLib.LuaPushValue(L, stackPos);
				
				while (true)
				{
					LuaLib.LuaRawGetI(L, -1, index);
					luatype = LuaLib.LuaType(L, -1);
					
					if (luatype == LuaTypes.Nil)
					{
						return list.ToArray();
					}
					else if (luatype != LuaTypes.Number)
					{
						break;
					}
					
					bool ret = LuaLib.LuaToBoolean(L, -1);
					list.Add(ret);
					LuaLib.LuaPop(L, 1);
					++index;
				}
			}
			else if (luatype == LuaTypes.UserData)
			{
				bool[] ret = GetNetObject<bool[]>(L, stackPos);
				
				if (ret != null)
				{
					return (bool[])ret;
				}
			}
			
			LuaLib.LuaLError(L, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));
			return null;
		}

//		public static LuaStringBuffer GetStringBuffer(LuaState L, int stackPos)
//		{
//			LuaTypes luatype = LuaLib.LuaType(L, stackPos);
//			
//			if (luatype != LuaTypes.String)
//			{
//				LuaLib.LuaLError(L, string.Format("invalid arguments to method: {0}", ErrorFunc(1)));
//				return null;
//			}
//			
//			int len = 0;
//			KeraLua.CharPtr buffer = KeraLua.Lua.LuaToLString(L, stackPos, out len);
//			return new LuaStringBuffer(buffer, len);                
//		}
	}
}
