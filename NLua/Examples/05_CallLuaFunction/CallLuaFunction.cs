using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NLua;

public class CallLuaFunction : MonoBehaviour {

  private string msg;

  void Awake() {
    Application.RegisterLogCallback(logCB);
  }

  private void logCB(string msg, string stackTrace, LogType type) {
    this.msg = msg;
  }

  void Start() {
    using (Lua lua = new Lua()) {
      lua.LoadCLRPackage();
      var code = @"
        local UnityEngine = CLRPackage(""UnityEngine"", ""UnityEngine"")

        function foo(msg)
          UnityEngine.Debug.Log(msg)
          return 42
        end
      ";
      lua.DoString(code);

      LuaFunction fn = lua.GetFunction("foo");

      var rets = fn.Call("Hello Lua!");

      msg += "\nReturn Value: " + rets[0].ToString();
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
