using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour {
	GUIStyle style1, style2;

	void Start() {
		style1 = new GUIStyle();
		style1.normal.textColor = Color.white;
		style1.fontSize = 20;
		style1.alignment = TextAnchor.MiddleCenter;

		style2 = new GUIStyle();
		style2.normal.textColor = Color.white;
		style2.fontSize = 30;
		style2.alignment = TextAnchor.MiddleCenter;
	}

	void OnGUI() {
		GUILayout.BeginArea(new Rect(0, 0, 300, 500));
		GUILayout.Label("Left Hand", style2);
		GUILayout.Label("Thumb: " + Player.player.input.leftThumb, style1);
		GUILayout.Label("Point: " + Player.player.input.leftPoint, style1);
		GUILayout.Label("Middle: " + Player.player.input.leftMiddle, style1);
		GUILayout.Label("Ring: " + Player.player.input.leftRing, style1);
		GUILayout.Label("Pink: " + Player.player.input.leftPink, style1);
		GUILayout.EndArea();
		GUILayout.BeginArea(new Rect(Screen.width - 300, 0, 300, 500));
		GUILayout.Label("Right Hand", style2);
		GUILayout.Label("Thumb: " + Player.player.input.rightThumb, style1);
		GUILayout.Label("Point: " + Player.player.input.rightPoint, style1);
		GUILayout.Label("Middle: " + Player.player.input.rightMiddle, style1);
		GUILayout.Label("Ring: " + Player.player.input.rightRing, style1);
		GUILayout.Label("Pink: " + Player.player.input.rightPink, style1);
		GUILayout.EndArea();
	}
}
