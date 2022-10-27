using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevelInteractable : Interactable {
	public GameObject[] gameObjects;
	bool isEnabled = true;
	public override void PerformAction() {
		if (isEnabled && Player.player.input.IsForwardGesturePerformed()) {
			StoryManager.story.select.Play();
			GameManager.game.LoadNextLevel();
		}
	}
	public override void UpdateState() {
	}

	void Start() {
		foreach (GameObject gameObject in gameObjects) {
			gameObject.SetActive(false);
		}
		isEnabled = false;
	}

	public void EnableVision() {
		foreach (GameObject gameObject in gameObjects) {
			gameObject.SetActive(true);
		}
		isEnabled = true;
	}

	public void LoadLevelOverride(){
		GameManager.game.LoadNextLevel();
	}
}
