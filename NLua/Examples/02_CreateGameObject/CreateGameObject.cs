using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NLua;

public class CreateGameObject : MonoBehaviour {

  void Start() {
    using (Lua lua = new Lua()) {
      lua.LoadCLRPackage();
      var code = @"
        local UnityEngine = CLRPackage(""UnityEngine"", ""UnityEngine"")
        
        local go = UnityEngine.GameObject('New GameObject')
        go:AddComponent('ParticleSystem')
        local guiText = go:AddComponent('GUIText')
        guiText.text = 'This is an new GameObject'
        guiText.pixelOffset = UnityEngine.Vector2(UnityEngine.Screen.width / 2, UnityEngine.Screen.height / 2)
      ";
      lua.DoString(code);
    }
  }
  
  void OnGUI() {
    GUILayout.BeginVertical(GUILayout.Width(400f));
    {
      if (GUILayout.Button("Back", GUILayout.Width(120), GUILayout.Height(60))) {
        Application.LoadLevel(0);
      }
    }
    GUILayout.EndVertical();
  }

}
