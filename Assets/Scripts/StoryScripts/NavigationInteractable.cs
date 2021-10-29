using UnityEngine;

public class NavigationInteractable : Interactable {
	public StoryCheckpoint nextCheckpoint;

	public override void PerformAction() { }
	public override void UpdateState() {
		if (Player.player.input.IsCombinationPressed(fingers)) StoryManager.story.ChangeCheckpoint(nextCheckpoint);
	}



}