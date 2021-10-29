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

	public void SetCheckpointCamera(CinemachineVirtualCamera virtualCamera) {
		path.Priority = 10;
		foreach (CinemachineVirtualCamera vcam in story.GetComponentsInChildren<CinemachineVirtualCamera>()) {
			vcam.Priority = 10;
		}
		virtualCamera.Priority = 100;
		virtualCamera.MoveToTopOfPrioritySubqueue();
	}

	public void SetPathCamera() {
		foreach (CinemachineVirtualCamera vcam in story.GetComponentsInChildren<CinemachineVirtualCamera>()) {
			vcam.Priority = 10;
		}
		path.Priority = 100;
		path.MoveToTopOfPrioritySubqueue();
	}
}
