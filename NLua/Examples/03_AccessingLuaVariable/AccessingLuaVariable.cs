using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NLua;

public class AccessingLuaVariable : MonoBehaviour {

  private string msg;

  void Start() {
    using (Lua lua = new Lua()) {
      lua.LoadCLRPackage();
      var code = @"
        local UnityEngine = CLRPackage(""UnityEngine"", ""UnityEngine"")

        particles = {}

        for i = 1, createCount, 1 do
          local go = UnityEngine.GameObject('New GameObject')
          local ps = go:AddComponent('ParticleSystem')
          ps:Stop()

          table.insert(particles, ps)
        end

        globalVar = 42
      ";

      lua["createCount"] = 5;
      lua.DoString(code);

      msg = "Read from lua var \"globalVar\": " + lua["globalVar"];

      LuaTable particles = lua["particles"] as LuaTable;

      foreach (ParticleSystem ps in particles.Values) {
        ps.Play();
      }
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
