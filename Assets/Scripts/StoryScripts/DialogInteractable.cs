 using UnityEngine;
using System.Collections;
using VIDE_Data;
using UnityEngine.Events;

[RequireComponent(typeof(VIDE_Assign))]
public class DialogInteractable : Interactable {
	//[HideInInspector]
	public GameObject UI;
	public AudioSource mumble;
	public string title = "Dialog";
	public UnityEvent OnDisabled;

	DialogUI dialogUI;
	public StoryCheckpoint[] dialogDestinations;
	GUIStyle style;
	bool timerOver = false;
	bool isTiming = false;
	int selectOption = 1;
	void Start() {
		gameObject.AddComponent<VD>();
		GameObject InstantiatedUI = Instantiate(UI);
		InstantiatedUI.transform.SetParent(transform);
		InstantiatedUI.SetActive(false);
		dialogUI = InstantiatedUI.GetComponent<DialogUI>();
		dialogUI.title = title;
	}

	public override void PerformAction() {
		if (VD.isActive) {
			GetComponentInParent<StoryCheckpoint>().blockInput = true;
			dialogUI.gameObject.SetActive(true);
			var data = VD.nodeData;
			if (data.sprite != null) dialogUI.SetSprite(data.sprite);
			if (data.isPlayer) {
				if (!StoryManager.story.playerMumble.isPlaying) StoryManager.story.playerMumble.Play();
				if (mumble.isPlaying) mumble.Stop();
				if (data.sprite == null && GetComponent<VIDE_Assign>().defaultPlayerSprite != null)
					dialogUI.SetSprite(GetComponent<VIDE_Assign>().defaultPlayerSprite);
				string[] comments = (string[])data.comments.Clone();
				if (data.comments.Length == 1) {
					dialogUI.SetText(comments[0]);
					if (!isTiming) StartCoroutine(Timer(comments[0]));
					if (timerOver || Input.GetKeyDown(KeyCode.Space)) {
						timerOver = false;
						VD.Next();
					}
				} else {
					if (Player.player.input.IsRigthSwingGesturePerformed()) {
						selectOption++;
						if (selectOption == comments.Length) selectOption = 0;
					}
					if (Player.player.input.IsLeftSwingGesturePerformed()) {
						selectOption--;
						if (selectOption < 0) selectOption = comments.Length - 1;
					}
					comments[selectOption] = " >> " + comments[selectOption] + " << ";
					if (Player.player.input.IsForwardGesturePerformed()) {
						timerOver = false;
						data.commentIndex = selectOption;
						VD.Next();
					}
				}
				dialogUI.SetText(comments);
			} else {
				if (!mumble.isPlaying) mumble.Play();
				if (StoryManager.story.playerMumble.isPlaying) StoryManager.story.playerMumble.Stop();
				if (data.sprite == null && GetComponent<VIDE_Assign>().defaultNPCSprite != null)
					dialogUI.SetSprite(GetComponent<VIDE_Assign>().defaultNPCSprite);
				dialogUI.SetText(data.comments[data.commentIndex]);
				if (!isTiming) StartCoroutine(Timer(data.comments[data.commentIndex]));
				if (timerOver || Player.player.input.IsForwardGesturePerformed()) {
					timerOver = false;
					VD.Next();
				}
			}
			if (data.isEnd) {
				GetComponentInParent<StoryCheckpoint>().blockInput = false;
				VD.EndDialogue();
				dialogUI.gameObject.SetActive(false);
			}
		} else {
			dialogUI.gameObject.SetActive(false);
			if (Player.player.input.IsForwardGesturePerformed()) {
				StoryManager.story.select.Play();
				VD.BeginDialogue(GetComponent<VIDE_Assign>());
			}
		}
	}

	public override void UpdateState() { }

	public void DialogActionGoToLocation(int checkpointIndex) {
		StoryManager.story.ChangeCheckpoint(dialogDestinations[checkpointIndex]);
	}
	public void DisableDialog() {
		if (VD.isActive) {
			GetComponentInParent<StoryCheckpoint>().blockInput = false;
			VD.EndDialogue();
			dialogUI.gameObject.SetActive(false);
		}
		OnDisabled.Invoke();
		Destroy(this);
	}

	IEnumerator Timer(string text) {
		isTiming = true;
		yield return new WaitForSeconds(5.0f);
		timerOver = true;
		isTiming = false;
	}

}
