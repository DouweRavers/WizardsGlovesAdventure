using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StoryCheckpoint : MonoBehaviour {
	Interactable[] interactables;
	Interactable activeInteractable;
	CinemachineClearShot clearShot;
	int activeIndex = 0;
	StoryManager storyManager;
	public Transform checkpointPosition;
	public bool blockInput = false;
	void Start() {
		storyManager = GetComponentInParent<StoryManager>();
		checkpointPosition = GetComponentInChildren<CheckpointLocation>().transform;
		clearShot = GetComponentInChildren<CinemachineClearShot>();
		interactables = GetComponentsInChildren<Interactable>();
	}

	public void OnAssignCheckpoint() {
		if (interactables == null) interactables = GetComponentsInChildren<Interactable>();
		if (clearShot == null) clearShot = GetComponentInChildren<CinemachineClearShot>();
		activeInteractable = interactables[activeIndex];
		foreach (Interactable interactable in interactables) {
			interactable.Deselect();
		}
		activeInteractable.Select();
		clearShot.LookAt = activeInteractable.transform;
	}

	public void OnDeassignCheckpoint() {
		if (interactables == null) interactables = GetComponentsInChildren<Interactable>();
		foreach (Interactable interactable in interactables) {
			interactable.Deselect();
		}
	}

	public void DoCurrentState() {
		if (Player.player.input.IsCombinationPressedDown(Finger.THUMB_RIGHT, Finger.RING_RIGHT) && !blockInput) {
			if (activeIndex + 1 >= interactables.Length) activeIndex = 0;
			else activeIndex++;
			activeInteractable.Deselect();
			activeInteractable = interactables[activeIndex];
			activeInteractable.Select();
			storyManager.pop.Play();
			clearShot.LookAt = activeInteractable.transform;
		}
		if (Player.player.input.IsCombinationPressedDown(Finger.THUMB_RIGHT, Finger.PINK_RIGHT) && !blockInput) {
			if (activeIndex <= 0) activeIndex = interactables.Length - 1;
			else activeIndex--;
			activeInteractable.Deselect();
			activeInteractable = interactables[activeIndex];
			activeInteractable.Select();
			storyManager.GetComponent<AudioSource>().Play();
			clearShot.LookAt = activeInteractable.transform;
		}

		activeInteractable.PerformAction();
	}
	public void CheckNextState() {
		activeInteractable.UpdateState();
	}

	public void TeleportPlayerToCheckpoint() {
		Player.player.transform.position = checkpointPosition.position;
		Player.player.transform.rotation = checkpointPosition.rotation;
	}

}
