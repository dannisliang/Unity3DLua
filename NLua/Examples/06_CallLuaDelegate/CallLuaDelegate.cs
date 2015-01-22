using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NLua;

public class CallLuaDelegate : MonoBehaviour {

  public delegate int FooDelegate(string msg, out string finalMsg);

  public class LuaFooDelegate : NLua.Method.LuaDelegate {
    int CallFunction(string msg, out string finalMsg) {
      object [] args = new object [] {msg, ""};
      object [] inArgs = new object [] {msg};
      int [] outArgs = new int [] {1};
      var ret = base.CallFunction(args, inArgs, outArgs);
      finalMsg = (string)args[1];
      return (int)ret;
    }
  }

  private string msg;

  public FooDelegate fooE;

  void Awake() {
    Application.RegisterLogCallback(logCB);
  }

  private void logCB(string msg, string stackTrace, LogType type) {
    this.msg = msg;
  }

  void Start() {
    using (Lua lua = new Lua()) {
      // iOS need register delegate type.
      lua.RegisterLuaDelegateType(typeof(FooDelegate), typeof(LuaFooDelegate));
      lua.LoadCLRPackage();
      var code = @"
        local UnityEngine = CLRPackage(""UnityEngine"", ""UnityEngine"")

        function foo(self)
          self.fooE = fooDelegate
        end

        function fooDelegate(msg)
          UnityEngine.Debug.Log(msg)
          local finalMsg = msg .. ' ' .. tostring(42)
          return 42, finalMsg
        end
      ";
      lua.DoString(code);
      
      LuaFunction fn = lua.GetFunction("foo");
      fn.Call(this);

      string finalMsg;
      var ret = fooE("Hello Lua!", out finalMsg);

      msg += "\nOut Argument Value: " + finalMsg;
      msg += "\nReturn Value: " + ret.ToString();
    }
  }

  void OnGUI() {
    GUILayout.BeginVertical(GUILayout.Width(400f));
    {
      GUILayout.Label(msg);
      if (GUILayout.Button("Back", GUILayout.Width(120), GUILayout.Height(60))) {
        Application.LoadLevel(0);
      }
    }
    GUILayout.EndVertical();
  }

}
