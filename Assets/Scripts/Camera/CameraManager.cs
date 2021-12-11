using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour {
	public static CameraManager cameraManager;

	public Transform story;
	public CinemachineClearShot path;
	void Awake() {
		cameraManager = this;
	}

	public void SetCheckpointCamera(StoryCheckpoint checkpoint) {
		path.Priority = 0;
		foreach (CinemachineClearShot vcam in story.GetComponentsInChildren<CinemachineClearShot>()) {
			vcam.Priority = 0;
		}
		foreach (CinemachineClearShot vcam in checkpoint.GetComponentsInChildren<CinemachineClearShot>()) {
			vcam.Priority = 10;
			vcam.MoveToTopOfPrioritySubqueue();
		}
	}

	public void SetPathCamera() {
		foreach (CinemachineClearShot vcam in story.GetComponentsInChildren<CinemachineClearShot>()) {
			vcam.Priority = 0;
		}
		path.Priority = 10;
		path.MoveToTopOfPrioritySubqueue();
	}
}
