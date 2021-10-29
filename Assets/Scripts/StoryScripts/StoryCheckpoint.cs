using System;
using UnityEngine;
using Cinemachine;

public class StoryCheckpoint : MonoBehaviour {
	Interactable[] interactables = new Interactable[0];
	StoryManager storyManager;
	public Transform checkpointPosition;

	void Awake() {
		storyManager = GetComponentInParent<StoryManager>();
		checkpointPosition = GetComponentInChildren<CheckpointLocation>().transform;
	}

	public void OnAssignCheckpoint() { }
	public void OnDeassignCheckpoint() { }
	public void DoCurrentState() {
		foreach (Interactable interactable in interactables) {
			interactable.PerformAction();
		}
	}
	public void CheckNextState() {
		foreach (Interactable interactable in interactables) {
			interactable.UpdateState();
		}
	}

	public void TeleportPlayerToCheckpoint() {
		Player.player.transform.position = checkpointPosition.position;
		Player.player.transform.rotation = checkpointPosition.rotation;
	}

	public void AddInteractable(Interactable interactable) {
		int index = interactables.Length;
		Array.Resize(ref interactables, interactables.Length + 1);
		interactables[index] = interactable;
	}

}
