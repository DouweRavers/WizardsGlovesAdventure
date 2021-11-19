using UnityEngine;
using VIDE_Data;

[RequireComponent(typeof(VIDE_Assign))]
public class DialogInteractable : Interactable {
	[HideInInspector]
	public GameObject UI;

	public string title = "Dialog";
	DialogUI dialogUI;
	public StoryCheckpoint[] dialogDestinations;
	GUIStyle style;
	float timer = 0;
	new void Start() {
		base.Start();
		gameObject.AddComponent<VD>();
		GameObject InstantiatedUI = Instantiate(UI);
		InstantiatedUI.transform.SetParent(transform);
		InstantiatedUI.SetActive(false);
		dialogUI = InstantiatedUI.GetComponent<DialogUI>();
		dialogUI.title = title;
	}
	public override void PerformAction() {
		if (VD.isActive) {
			dialogUI.gameObject.SetActive(true);
			var data = VD.nodeData;
			if (data.sprite != null) dialogUI.SetSprite(data.sprite);
			if (data.isPlayer) {
				string[] comments = (string[])data.comments.Clone();
				if (data.comments.Length == 1) {
					dialogUI.SetText(comments[0]);
					if (registerPress(fingers)) {
						VD.Next();
					}
				} else {
					for (int i = 0; i < data.comments.Length; i++) {
						Finger local_finger = (Finger)i;
						comments[i] = "[" + local_finger.ToString() + "] " + comments[i];
						if (registerPress(new Finger[] { local_finger })) {
							data.commentIndex = i;
							VD.Next();
						}
					}
				}
				dialogUI.SetText(comments);
			} else {
				dialogUI.SetText(data.comments[data.commentIndex]);
				if (registerPress(fingers)) {
					VD.Next();
				}
			}
			if (data.isEnd) {
				VD.EndDialogue();
				dialogUI.gameObject.SetActive(false);
			}
		} else {
			dialogUI.gameObject.SetActive(false);
			if (registerPress(fingers)) {
				VD.BeginDialogue(GetComponent<VIDE_Assign>());
			}
		}
	}

	public override void UpdateState() { }

	public void DialogActionGoToLocation(int checkpointIndex) {
		StoryManager.story.ChangeCheckpoint(dialogDestinations[checkpointIndex]);
	}

	bool registerPress(Finger[] fingers) {
		bool result = Player.player.input.IsCombinationPressed(fingers);
		result &= Time.timeSinceLevelLoad - timer > 1;
		if (result) timer = Time.timeSinceLevelLoad;
		return result;
	}

}
