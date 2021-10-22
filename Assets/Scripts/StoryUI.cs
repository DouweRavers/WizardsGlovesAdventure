using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryUI : MonoBehaviour
{
	GUIStyle style1, style2;

	void Start()
	{
		style1 = new GUIStyle();
		style1.normal.textColor = Color.white;
		style1.fontSize = 20;
		style1.alignment = TextAnchor.MiddleCenter;

		style2 = new GUIStyle();
		style2.normal.textColor = Color.white;
		style2.fontSize = 30;
		style2.alignment = TextAnchor.MiddleCenter;
	}

	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, Screen.height - 250, 400, 250));
		GUILayout.Label("Mission Menu", style2);
		GUILayout.Label("Message", style1);
		GUILayout.EndArea();
	}
}
