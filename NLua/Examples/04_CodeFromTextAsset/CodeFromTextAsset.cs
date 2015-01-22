using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NLua;

public class CodeFromTextAsset : MonoBehaviour {

  public TextAsset code;

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
      lua.DoString(code.text);
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
