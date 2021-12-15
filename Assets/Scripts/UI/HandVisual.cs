using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HandVisual : MonoBehaviour {
	public bool left = true;
	public InputManager inputManager;
	public Transform thumb, index, middle, ring, pinky;

	void Start() {

	}
	void Update() {
		if (left ? inputManager.leftThumb : inputManager.rightThumb) thumb.GetComponent<RawImage>().color = Color.red;
		else thumb.GetComponent<RawImage>().color = Color.white;

		if (left ? inputManager.leftPoint : inputManager.rightPoint) index.GetComponent<RawImage>().color = Color.red;
		else index.GetComponent<RawImage>().color = Color.white;

		if (left ? inputManager.leftMiddle : inputManager.rightMiddle) middle.GetComponent<RawImage>().color = Color.red;
		else middle.GetComponent<RawImage>().color = Color.white;

		if (left ? inputManager.leftRing : inputManager.rightRing) ring.GetComponent<RawImage>().color = Color.red;
		else ring.GetComponent<RawImage>().color = Color.white;

		if (left ? inputManager.leftPink : inputManager.rightPink) pinky.GetComponent<RawImage>().color = Color.red;
		else pinky.GetComponent<RawImage>().color = Color.white;
	}
}
