using UnityEngine;
using VIDE_Data;


public class DialogInteractable : Interactable {
	public StoryCheckpoint[] dialogDestinations;
	bool inDialog = true;
	GUIStyle style;
	public override void PerformAction() { }
	public override void UpdateState() { }

	public void DialogActionGoToLocation(int checkpointIndex) {
		StoryManager.story.ChangeCheckpoint(dialogDestinations[checkpointIndex]);
	}

	void Start() {
		style = new GUIStyle();
		style.fontSize = 25;
		style.normal.textColor = Color.white;
		gameObject.AddComponent<VD>();
	}
	void OnGUI() {
		if (VD.isActive) {
			GUILayout.BeginArea(new Rect(Screen.width * 0.1f, Screen.height * 0.8f, Screen.width * 0.8f, Screen.height * 0.2f));
			GUILayout.Label("Dialog", style);

			var data = VD.nodeData; //Quick reference
			if (data.isPlayer) // If it's a player node, let's show all of the available options as buttons
						{
				for (int i = 0; i < data.comments.Length; i++) {
					if (GUILayout.Button(data.comments[i])) //When pressed, set the selected option and call Next()
					{
						data.commentIndex = i;
						VD.Next();
					}
				}
			} else //if it's a NPC node, Let's show the comment and add a button to continue
			{
				GUILayout.Label(data.comments[data.commentIndex]);

				if (GUILayout.Button(">")) {
					VD.Next();
				}
			}
			if (data.isEnd) // If it's the end, let's just call EndDialogue
				{
				VD.EndDialogue();
			}
			GUILayout.EndArea();
		} else {
			if (Vector3.Distance(Player.player.transform.position, transform.position) < 10) {
				if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 200, 100), "Start Convo", style)) {
					VD.BeginDialogue(GetComponent<VIDE_Assign>()); //We've attached a VIDE_Assign to this same gameobject, so we just call the component
				}
			}
		}

	}

}
