using System;
using System.Collections.Generic;
using UnityEngine;

public class DebugGloveController : MonoBehaviour {
	// public Transform[] fingers;
	// public Transform hand;
	public InputManager inputManager;
	public bool left = true;
	Material selectedMaterial, unselectedMaterial;


	// Debug
	Texture2D gyroGraph, magnetoGraph, acceloGraph;
	List<Vector3> gyroDataPoints, magnetoDataPoints, acceloDataPoints;

	void Start() {
		// selectedMaterial = Instantiate(fingers[0].GetComponent<MeshRenderer>().material);
		// unselectedMaterial = Instantiate(selectedMaterial);
		// selectedMaterial.color = Color.green;
		// unselectedMaterial.color = Color.red;
		gyroGraph = new Texture2D(256, 128);
		gyroDataPoints = new List<Vector3>();
		for (int i = 0; i < gyroGraph.width; i++) {
			gyroDataPoints.Add(Vector3.zero);
		}

		magnetoGraph = new Texture2D(256, 128);
		magnetoDataPoints = new List<Vector3>();
		for (int i = 0; i < magnetoGraph.width; i++) {
			magnetoDataPoints.Add(Vector3.zero);
		}

		acceloGraph = new Texture2D(256, 128);
		acceloDataPoints = new List<Vector3>();
		for (int i = 0; i < acceloGraph.width; i++) {
			acceloDataPoints.Add(Vector3.zero);
		}
	}

	void Update() {
		if (inputManager.IsRigthSwingGesturePerformed(left)) print("right");
		if (inputManager.IsLeftSwingGesturePerformed(left)) print("left");
		if (inputManager.IsForwardGesturePerformed()) print("forward");
		if (inputManager.IsSpellGesturePerformed(GestureType.FIRE)) print("fire");
		if (inputManager.IsSpellGesturePerformed(GestureType.EARTH)) print("earth");
		if (inputManager.IsSpellGesturePerformed(GestureType.LIGHT)) print("light");
		if (inputManager.IsSpellGesturePerformed(GestureType.DARK)) print("dark");
		if (inputManager.IsSpellGesturePerformed(GestureType.LOW)) print("low");
		if (inputManager.IsSpellGesturePerformed(GestureType.MEDIUM)) print("medium");
		if (inputManager.IsSpellGesturePerformed(GestureType.HIGH)) print("high");
		// if (inputManager.IsAttackGesturePerformed()) print("attack");



		// if (left ? inputManager.leftThumb : inputManager.rightThumb) fingers[0].GetComponent<MeshRenderer>().sharedMaterial = selectedMaterial;
		// else fingers[0].GetComponent<MeshRenderer>().sharedMaterial = unselectedMaterial;

		// if (left ? inputManager.leftPoint : inputManager.rightRing) fingers[1].GetComponent<MeshRenderer>().sharedMaterial = selectedMaterial;
		// else fingers[1].GetComponent<MeshRenderer>().sharedMaterial = unselectedMaterial;

		// if (left ? inputManager.leftMiddle : inputManager.rightMiddle) fingers[2].GetComponent<MeshRenderer>().sharedMaterial = selectedMaterial;
		// else fingers[2].GetComponent<MeshRenderer>().sharedMaterial = unselectedMaterial;

		// if (left ? inputManager.leftRing : inputManager.rightPoint) fingers[3].GetComponent<MeshRenderer>().sharedMaterial = selectedMaterial;
		// else fingers[3].GetComponent<MeshRenderer>().sharedMaterial = unselectedMaterial;

		// if (left ? inputManager.leftPink : inputManager.rightPink) fingers[4].GetComponent<MeshRenderer>().sharedMaterial = selectedMaterial;
		// else fingers[4].GetComponent<MeshRenderer>().sharedMaterial = unselectedMaterial;

		gyroDataPoints.RemoveAt(0);
		gyroDataPoints.Add(left ? inputManager.gyroscopeDataL : inputManager.gyroscopeDataR);
		magnetoDataPoints.RemoveAt(0);
		magnetoDataPoints.Add(left ? inputManager.magnetometerDataL : inputManager.magnetometerDataR);
		acceloDataPoints.RemoveAt(0);
		acceloDataPoints.Add(left ? inputManager.accelometerDataL : inputManager.accelometerDataR);
	}

	void OnGUI() {
		for (int x = 0; x < gyroGraph.width; x++) {
			int Height = gyroGraph.height;
			int thresholdX = Mathf.RoundToInt(Height / 2 * gyroDataPoints[x].x / 200);
			int thresholdY = Mathf.RoundToInt(Height / 2 * gyroDataPoints[x].y / 200);
			int thresholdZ = Mathf.RoundToInt(Height / 2 * gyroDataPoints[x].z / 200);
			for (int y = 0; y < Height; y++) {
				if (y == Height / 2) gyroGraph.SetPixel(x, y, Color.white);
				else if (y < Height / 2) {
					gyroGraph.SetPixel(x, y, new Color(
												Height / 2 - Mathf.Abs(thresholdX) < y && thresholdX < 0 ? 1 : 0,
												Height / 2 - Mathf.Abs(thresholdY) < y && thresholdY < 0 ? 1 : 0,
												Height / 2 - Mathf.Abs(thresholdZ) < y && thresholdZ < 0 ? 1 : 0
											));
				} else {
					gyroGraph.SetPixel(x, y, new Color(
												Height / 2 + thresholdX > y && thresholdX > 0 ? 1 : 0,
												Height / 2 + thresholdY > y && thresholdY > 0 ? 1 : 0,
												Height / 2 + thresholdZ > y && thresholdZ > 0 ? 1 : 0
											));
				}

			}
		}
		gyroGraph.Apply();
		GUILayout.BeginHorizontal();
		GUILayout.Label("Gyroscope");
		GUILayout.Box(gyroGraph);
		float avgMagnitude = 0, avgX = 0, avgY = 0, avgZ = 0;
		for (int i = gyroDataPoints.Count - 10; i < gyroDataPoints.Count; i++) {
			avgMagnitude += gyroDataPoints[i].magnitude;
			avgX += gyroDataPoints[i].x;
			avgY += gyroDataPoints[i].y;
			avgZ += gyroDataPoints[i].z;
		}
		avgMagnitude /= 10; avgX /= 10; avgY /= 10; avgZ /= 10;
		GUILayout.Label("avg 10ms:\nX=" + avgX.ToString("#.##") + "\nY=" + avgY.ToString("#.##") + "\nZ=" + avgZ.ToString("#.##") + "\nL=" + avgMagnitude.ToString("#.##"));
		GUILayout.EndHorizontal();

		for (int x = 0; x < magnetoGraph.width; x++) {
			int Height = magnetoGraph.height;
			int thresholdX = Mathf.RoundToInt(magnetoGraph.height * magnetoDataPoints[x].x / 200);
			int thresholdY = Mathf.RoundToInt(magnetoGraph.height * magnetoDataPoints[x].y / 200);
			int thresholdZ = Mathf.RoundToInt(magnetoGraph.height * magnetoDataPoints[x].z / 200);
			for (int y = 0; y < Height; y++) {
				if (y == Height / 2) magnetoGraph.SetPixel(x, y, Color.white);
				else if (y < Height / 2) {
					magnetoGraph.SetPixel(x, y, new Color(
												Height / 2 - Mathf.Abs(thresholdX) < y && thresholdX < 0 ? 1 : 0,
												Height / 2 - Mathf.Abs(thresholdY) < y && thresholdY < 0 ? 1 : 0,
												Height / 2 - Mathf.Abs(thresholdZ) < y && thresholdZ < 0 ? 1 : 0
											));
				} else {
					magnetoGraph.SetPixel(x, y, new Color(
												Height / 2 + thresholdX > y && thresholdX > 0 ? 1 : 0,
												Height / 2 + thresholdY > y && thresholdY > 0 ? 1 : 0,
												Height / 2 + thresholdZ > y && thresholdZ > 0 ? 1 : 0
											));
				}

			}
		}
		magnetoGraph.Apply();
		GUILayout.BeginHorizontal();
		GUILayout.Label("Magneto sensor");
		GUILayout.Box(magnetoGraph);
		avgMagnitude = 0; avgX = 0; avgY = 0; avgZ = 0;
		for (int i = magnetoDataPoints.Count - 10; i < magnetoDataPoints.Count; i++) {
			avgMagnitude += magnetoDataPoints[i].magnitude;
			avgX += magnetoDataPoints[i].x;
			avgY += magnetoDataPoints[i].y;
			avgZ += magnetoDataPoints[i].z;
		}
		avgMagnitude /= 10; avgX /= 10; avgY /= 10; avgZ /= 10;
		GUILayout.Label("avg 10ms:\nX=" + avgX.ToString("#.##") + "\nY=" + avgY.ToString("#.##") + "\nZ=" + avgZ.ToString("#.##") + "\nL=" + avgMagnitude.ToString("#.##"));
		GUILayout.EndHorizontal();

		for (int x = 0; x < acceloGraph.width; x++) {
			int Height = acceloGraph.height;
			int thresholdX = Mathf.RoundToInt(acceloGraph.height * acceloDataPoints[x].x / 4);
			int thresholdY = Mathf.RoundToInt(acceloGraph.height * acceloDataPoints[x].y / 4);
			int thresholdZ = Mathf.RoundToInt(acceloGraph.height * acceloDataPoints[x].z / 4);
			for (int y = 0; y < Height; y++) {
				if (y == Height / 2) acceloGraph.SetPixel(x, y, Color.white);
				else if (y < Height / 2) {
					acceloGraph.SetPixel(x, y, new Color(
												Height / 2 - Mathf.Abs(thresholdX) < y && thresholdX < 0 ? 1 : 0,
												Height / 2 - Mathf.Abs(thresholdY) < y && thresholdY < 0 ? 1 : 0,
												Height / 2 - Mathf.Abs(thresholdZ) < y && thresholdZ < 0 ? 1 : 0
											));
				} else {
					acceloGraph.SetPixel(x, y, new Color(
												Height / 2 + thresholdX > y && thresholdX > 0 ? 1 : 0,
												Height / 2 + thresholdY > y && thresholdY > 0 ? 1 : 0,
												Height / 2 + thresholdZ > y && thresholdZ > 0 ? 1 : 0
											));
				}

			}
		}
		acceloGraph.Apply();
		GUILayout.BeginHorizontal();
		GUILayout.Label("Accelometer");
		GUILayout.Box(acceloGraph);
		avgMagnitude = 0; avgX = 0; avgY = 0; avgZ = 0;
		for (int i = acceloDataPoints.Count - 10; i < acceloDataPoints.Count; i++) {
			avgMagnitude += acceloDataPoints[i].magnitude;
			avgX += acceloDataPoints[i].x;
			avgY += acceloDataPoints[i].y;
			avgZ += acceloDataPoints[i].z;
		}
		avgMagnitude /= 10; avgX /= 10; avgY /= 10; avgZ /= 10;
		GUILayout.Label("avg 10ms:\nX=" + avgX.ToString("#.##") + "\nY=" + avgY.ToString("#.##") + "\nZ=" + avgZ.ToString("#.##") + "\nL=" + avgMagnitude.ToString("#.##"));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		GUILayout.Label("L pinkt: ");
		GUILayout.Label(inputManager.leftPink ? "+" : "-", GUILayout.Width(15));
		GUILayout.Label("L ring: ");
		GUILayout.Label(inputManager.leftRing ? "+" : "-", GUILayout.Width(15));
		GUILayout.Label("L middle: ");
		GUILayout.Label(inputManager.leftMiddle ? "+" : "-", GUILayout.Width(15));
		GUILayout.Label("L point: ");
		GUILayout.Label(inputManager.leftPoint ? "+" : "-", GUILayout.Width(15));
		GUILayout.Label("L thumb: ");
		GUILayout.Label(inputManager.leftThumb ? "+" : "-", GUILayout.Width(15));
		GUILayout.Label("R pinkt: ");
		GUILayout.Label(inputManager.rightPink ? "+" : "-", GUILayout.Width(15));
		GUILayout.Label("R ring: ");
		GUILayout.Label(inputManager.rightRing ? "+" : "-", GUILayout.Width(15));
		GUILayout.Label("R middle: ");
		GUILayout.Label(inputManager.rightMiddle ? "+" : "-", GUILayout.Width(15));
		GUILayout.Label("R point: ");
		GUILayout.Label(inputManager.rightPoint ? "+" : "-", GUILayout.Width(15));
		GUILayout.Label("R thumb: ");
		GUILayout.Label(inputManager.rightThumb ? "+" : "-", GUILayout.Width(15));

		GUILayout.EndHorizontal();
	}
}
