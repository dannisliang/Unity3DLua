
using toLua;
using NLua;

//	DebugWrap.cs
//	Author: Lu Zexi
//	2015-01-22



//debug wrap class
public class DebugWrap
{
	public static LuaMethod[] methods = new LuaMethod[]
	{
		new LuaMethod("Log",Log),
		new LuaMethod("LogWaning",LogWarning),
		new LuaMethod("LogError",LogError)
	};

	public DebugWrap ()
	{
	}

	static int Log(KeraLua.LuaState luastate)
	{
		toLuaLib.CheckArgsCount(luastate, 1);
//		string str = LuaLib.get
		return 1;
	}

	static int LogWarning(KeraLua.LuaState luastate)
	{
		return 1;
	}

	static int LogError(KeraLua.LuaState luastate)
	{
		return 1;
	}
}

