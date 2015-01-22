using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

  void OnGUI() {
    GUILayout.BeginVertical(GUILayout.Width(200));
    {
      if (GUILayout.Button("01_HelloLua", GUILayout.Height(60))) {
        Application.LoadLevel(1);
      }
      if (GUILayout.Button("02_CreateGameObject", GUILayout.Height(60))) {
        Application.LoadLevel(2);
      }
      if (GUILayout.Button("03_AccessingLuaVariable", GUILayout.Height(60))) {
        Application.LoadLevel(3);
      }
      if (GUILayout.Button("04_CodeFromTextAsset", GUILayout.Height(60))) {
        Application.LoadLevel(4);
      }
      if (GUILayout.Button("05_CallLuaFunction", GUILayout.Height(60))) {
        Application.LoadLevel(5);
      }
      if (GUILayout.Button("06_CallLuaDelegate", GUILayout.Height(60))) {
        Application.LoadLevel(6);
      }
    }
    GUILayout.EndVertical();
  }

}
