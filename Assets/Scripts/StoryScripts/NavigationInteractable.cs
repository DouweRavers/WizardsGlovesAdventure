using UnityEngine;

public class NavigationInteractable : Interactable {
	public StoryCheckpoint nextCheckpoint;

	public override void PerformAction() { }
	public override void UpdateState() {
		if (Input.GetKeyDown(KeyCode.Space)) StoryManager.story.ChangeCheckpoint(nextCheckpoint);
	}



}