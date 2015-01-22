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
		public static bool CheckTypes(Lua L, Type[] types, int begin)
		{        
			for (int i = 0; i < types.Length; i++)
			{
				LuaTypes luaType = LuaLib.LuaType(L.luastate, i + begin);
				
				if (!CheckType(L, luaType, types[i], i + begin))
				{
					return false;
				}
			}
			
			return true;
		}
		
		public static bool CheckParamsType(Lua L, Type t, int begin, int count)
		{        
			//默认都可以转 object
			if (t == typeof(object))
			{
				return true;
			}
			
			for (int i = 0; i < count; i++)
			{
				LuaTypes luaType = LuaLib.LuaType(L.luastate, i + begin);
				
				if (!CheckType(L, luaType, t, i + begin))
				{
					return false;
				}
			}
			
			return true;
		}
		
		static bool CheckType(Lua L, LuaTypes luaType, Type t, int pos)
		{
			switch (luaType)
			{
			case LuaTypes.Number:
				return t.IsPrimitive;
			case LuaTypes.String:
				return t == typeof(string);
			case LuaTypes.UserData:
				return CheckUserData(L, luaType, t, pos);
			case LuaTypes.Boolean:
				return t == typeof(bool);
			case LuaTypes.Function:
				return t == typeof(LuaFunction);
			case LuaTypes.Table:
				return t == typeof(LuaTable) || t.IsArray;
			case LuaTypes.Nil:
				break;
			default:
				break;
			}
			
			return false;
		}
		
		static bool CheckUserData(Lua L, LuaTypes luaType, Type t, int pos)
		{
			if (t == typeof(object))
			{
				return true;
			}
			
			object obj = GetLuaObject(L, pos);
			
			if (t.IsEnum || t == typeof(string))
			{
				return obj.GetType() == t;
			}
			else if (t == typeof(Type))
			{
				string name = obj.GetType().Name;
				return name == "MonoType" || name == "System.MonoType";
			}
			else
			{
				return obj.GetType() == t || t.IsAssignableFrom(obj.GetType());
			}
		}
	}
}