
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using NLua;



//	toLuaLib_Wrap.cs
//	Author: Lu Zexi
//	2015-01-22


namespace toLua
{
	//tolua lib
	public partial class toLuaLib
	{
		//====================== public static =====================
		
		//check args count
		public static void CheckArgsCount(KeraLua.LuaState L, int count)
		{
			int c = LuaLib.LuaGetTop(L);
			
			if (c != count)
			{
				string str = string.Format("no overload for method '{0}' takes '{1}' arguments", ErrorFunc(1), c);
				LuaLib.LuaLError(L, str);
			}
		}

		//======================== private static ===================

		//error func
		private static string ErrorFunc(int skip)
		{
			StackFrame sf = null;
			string file = string.Empty;
			StackTrace st = new StackTrace(skip, true);
			int pos = 0;
			
			do
			{
				sf = st.GetFrame(pos++);
				file = sf.GetFileName();
			} while (!file.Contains("Wrap"));
			
			int index1 = file.LastIndexOf('\\');
			int index2 = file.LastIndexOf("Wrap");
			string className = file.Substring(index1 + 1, index2 - index1 - 1);
			return string.Format("{0}.{1}", className, sf.GetMethod().Name);                
		}
	}
}
