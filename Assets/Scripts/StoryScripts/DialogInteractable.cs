using UnityEngine;


public class DialogInteractable : Interactable {
	bool inDialog = true;
	GUIStyle style;
	public override void PerformAction() { }
	public override void UpdateState() { }
	void Start() {
		style = new GUIStyle();
		style.fontSize = 25;
		style.normal.textColor = Color.white;
	}

	void OnGUI() {
		if (!inDialog) return;
		GUILayout.BeginArea(new Rect(Screen.width * 0.1f, Screen.height * 0.8f, Screen.width * 0.8f, Screen.height * 0.2f));
		GUILayout.Label("Dialog", style);
		GUILayout.EndArea();
	}
}
