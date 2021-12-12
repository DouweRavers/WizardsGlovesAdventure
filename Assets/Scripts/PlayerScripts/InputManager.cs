using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	public SerialController serialController1;
	bool[] fingerData = new bool[] {
		false, false, false, false, false,
		false, false, false, false, false
	};

	bool pressed = false;
	bool[] prevCom = null;
	[HideInInspector]
	public Vector3 gyroscopeData, magnetometerData, accelometerData;
	List<Vector3> gyroDataPoints, magnetoDataPoints, acceloDataPoints;
	float gestureTimer = 0f;

	public bool leftPink { get { return Input.GetKey("q") || Input.GetKey("a") || fingerData[0]; } }
	public bool leftRing { get { return Input.GetKey("w") || Input.GetKey("z") || fingerData[1]; } }
	public bool leftMiddle { get { return Input.GetKey("e") || fingerData[2]; } }
	public bool leftPoint { get { return Input.GetKey("r") || fingerData[3]; } }
	public bool leftThumb { get { return Input.GetKey("c") || fingerData[4]; } }

	public bool rightThumb { get { return Input.GetKey("n") || fingerData[5]; } }
	public bool rightPoint { get { return Input.GetKey("u") || fingerData[6]; } }
	public bool rightMiddle { get { return Input.GetKey("i") || fingerData[7]; } }
	public bool rightRing { get { return Input.GetKey("o") || fingerData[8]; } }
	public bool rightPink { get { return Input.GetKey("p") || fingerData[9]; } }

	public bool IsCombinationPressed(Finger[] fingers) {
		bool[] fingerArray = new bool[] {
			Array.Exists(fingers, value => value == Finger.PINK_LEFT),
			Array.Exists(fingers, value => value == Finger.RING_LEFT),
			Array.Exists(fingers, value => value == Finger.MIDDLE_LEFT),
			Array.Exists(fingers, value => value == Finger.POINT_LEFT),
			Array.Exists(fingers, value => value == Finger.THUMB_LEFT),
			Array.Exists(fingers, value => value == Finger.THUMB_RIGHT),
			Array.Exists(fingers, value => value == Finger.POINT_RIGHT),
			Array.Exists(fingers, value => value == Finger.MIDDLE_RIGHT),
			Array.Exists(fingers, value => value == Finger.RING_RIGHT),
			Array.Exists(fingers, value => value == Finger.PINK_RIGHT),
			Array.Exists(fingers, value => value == Finger.BLOCK),
			};
		return IsCombinationPressed(fingerArray);
	}
	// Check if the exact specified combination is pressed/touched using enum values
	public bool IsCombinationPressed(
		Finger firstFinger = Finger.NONE, Finger secondFinger = Finger.NONE,
		Finger thirdthFinger = Finger.NONE, Finger forthFinger = Finger.NONE,
		Finger fifthFinger = Finger.NONE, Finger sixthFinger = Finger.NONE,
		Finger seventhFinger = Finger.NONE, Finger eigthFinger = Finger.NONE,
		Finger ninethFinger = Finger.NONE, Finger tenthFinger = Finger.NONE) {
		bool[] fingerArray = new bool[] { false, false, false, false, false, false, false, false, false, false, false };
		if (firstFinger != Finger.NONE) fingerArray[(int)firstFinger] = true;
		if (secondFinger != Finger.NONE) fingerArray[(int)secondFinger] = true;
		if (thirdthFinger != Finger.NONE) fingerArray[(int)thirdthFinger] = true;
		if (forthFinger != Finger.NONE) fingerArray[(int)forthFinger] = true;
		if (fifthFinger != Finger.NONE) fingerArray[(int)fifthFinger] = true;
		if (sixthFinger != Finger.NONE) fingerArray[(int)sixthFinger] = true;
		if (seventhFinger != Finger.NONE) fingerArray[(int)seventhFinger] = true;
		if (eigthFinger != Finger.NONE) fingerArray[(int)eigthFinger] = true;
		if (ninethFinger != Finger.NONE) fingerArray[(int)ninethFinger] = true;
		if (tenthFinger != Finger.NONE) fingerArray[(int)tenthFinger] = true;
		return IsCombinationPressed(fingerArray);
	}

	/*
		Check if the exact specified combination is pressed/touched using a boolean array
		The array start from the left pink at 0 to the right pink at 9. 
	*/
	public bool IsCombinationPressed(bool[] leftToRightFingers) {
		return
			leftToRightFingers[0] == leftPink &&
			leftToRightFingers[1] == leftRing &&
			leftToRightFingers[2] == leftMiddle &&
			leftToRightFingers[3] == leftPoint &&
			leftToRightFingers[4] == leftThumb &&

			leftToRightFingers[5] == rightThumb &&
			leftToRightFingers[6] == rightPoint &&
			leftToRightFingers[7] == rightMiddle &&
			leftToRightFingers[8] == rightRing &&
			leftToRightFingers[9] == rightPink &&
			!leftToRightFingers[10];
	}

	public bool IsCombinationPressedDown(Finger[] fingers) {
		bool[] fingerArray = new bool[] {
			Array.Exists(fingers, value => value == Finger.PINK_LEFT),
			Array.Exists(fingers, value => value == Finger.RING_LEFT),
			Array.Exists(fingers, value => value == Finger.MIDDLE_LEFT),
			Array.Exists(fingers, value => value == Finger.POINT_LEFT),
			Array.Exists(fingers, value => value == Finger.THUMB_LEFT),
			Array.Exists(fingers, value => value == Finger.THUMB_RIGHT),
			Array.Exists(fingers, value => value == Finger.POINT_RIGHT),
			Array.Exists(fingers, value => value == Finger.MIDDLE_RIGHT),
			Array.Exists(fingers, value => value == Finger.RING_RIGHT),
			Array.Exists(fingers, value => value == Finger.PINK_RIGHT),
			Array.Exists(fingers, value => value == Finger.BLOCK),
			};
		return IsCombinationPressedDown(fingerArray);
	}

	public bool IsCombinationPressedDown(Finger firstFinger = Finger.NONE, Finger secondFinger = Finger.NONE,
		Finger thirdthFinger = Finger.NONE, Finger forthFinger = Finger.NONE,
		Finger fifthFinger = Finger.NONE, Finger sixthFinger = Finger.NONE,
		Finger seventhFinger = Finger.NONE, Finger eigthFinger = Finger.NONE,
		Finger ninethFinger = Finger.NONE, Finger tenthFinger = Finger.NONE) {
		bool[] fingerArray = new bool[] { false, false, false, false, false, false, false, false, false, false, false };
		if (firstFinger != Finger.NONE) fingerArray[(int)firstFinger] = true;
		if (secondFinger != Finger.NONE) fingerArray[(int)secondFinger] = true;
		if (thirdthFinger != Finger.NONE) fingerArray[(int)thirdthFinger] = true;
		if (forthFinger != Finger.NONE) fingerArray[(int)forthFinger] = true;
		if (fifthFinger != Finger.NONE) fingerArray[(int)fifthFinger] = true;
		if (sixthFinger != Finger.NONE) fingerArray[(int)sixthFinger] = true;
		if (seventhFinger != Finger.NONE) fingerArray[(int)seventhFinger] = true;
		if (eigthFinger != Finger.NONE) fingerArray[(int)eigthFinger] = true;
		if (ninethFinger != Finger.NONE) fingerArray[(int)ninethFinger] = true;
		if (tenthFinger != Finger.NONE) fingerArray[(int)tenthFinger] = true;
		return IsCombinationPressedDown(fingerArray);
	}

	public bool IsCombinationPressedDown(bool[] leftToRightFingers) {
		bool result = IsCombinationPressed(leftToRightFingers);
		if (pressed && Enumerable.SequenceEqual(leftToRightFingers, prevCom)) {
			if (!result) {
				prevCom = null;
				pressed = false;
			}
			return false;
		} else {
			if (result) {
				prevCom = leftToRightFingers;
				pressed = true;
			}
			return result;
		}
	}

	public bool IsRigthSwingGesturePerformed() {
		if (Input.GetKeyDown(KeyCode.RightArrow)) return true;
		if (Time.realtimeSinceStartup - gestureTimer < 1) return false;
		float avg = 0f;
		for (int i = 0; i < acceloDataPoints.Count; i++) {
			avg += acceloDataPoints[i].magnitude;
		}
		avg /= acceloDataPoints.Count;
		if (avg < 0.65f) {
			avg = 0f;
			for (int i = 0; i < magnetoDataPoints.Count; i++) {
				avg += magnetoDataPoints[i].z;
			}
			avg /= magnetoDataPoints.Count;
			if (avg > 60) {
				gestureTimer = Time.realtimeSinceStartup;
				serialController1.SendSerialMessage("1");
				return true;
			}
		}
		return false;
	}

	public bool IsLeftSwingGesturePerformed() {
		if (Input.GetKeyDown(KeyCode.LeftArrow)) return true;
		if (Time.realtimeSinceStartup - gestureTimer < 1) return false;
		float avg = 0f;
		for (int i = 0; i < acceloDataPoints.Count; i++) {
			avg += acceloDataPoints[i].magnitude;
		}
		avg /= acceloDataPoints.Count;
		if (avg < 0.65f) {
			avg = 0f;
			for (int i = 0; i < magnetoDataPoints.Count; i++) {
				avg += magnetoDataPoints[i].z;
			}
			avg /= magnetoDataPoints.Count;
			if (avg < -60) {
				gestureTimer = Time.realtimeSinceStartup;
				serialController1.SendSerialMessage("1");
				return true;
			}
		}
		return false;
	}

	public bool IsForwardGesturePerformed() {
		if (Input.GetKeyDown(KeyCode.Return)) return true;
		if (Time.realtimeSinceStartup - gestureTimer < 1) return false;
		float avg = 0f;
		for (int i = 0; i < magnetoDataPoints.Count; i++) {
			avg += magnetoDataPoints[i].x;
		}
		avg /= magnetoDataPoints.Count;
		if (avg < 30) {
			if (accelometerData.x < -1.5f) {
				gestureTimer = Time.realtimeSinceStartup;
				serialController1.SendSerialMessage("1");
				return true;
			}
		}
		return false;
	}

	public bool IsSpellGesturePerformed(GestureType gesture) {
		if (IsCombinationPressedDown((Finger)gesture)) return true; // dirty debugging trick
		if (Time.realtimeSinceStartup - gestureTimer < 1) return false;
		switch (gesture) {
			case GestureType.FIRE:
				IsCombinationPressedDown(
					Finger.POINT_LEFT, Finger.POINT_RIGHT,
					Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT
					);
				break;
			case GestureType.EARTH:
				IsCombinationPressedDown(
					Finger.PINK_LEFT, Finger.PINK_RIGHT,
					Finger.RING_LEFT, Finger.RING_RIGHT,
					Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT,
					Finger.POINT_LEFT, Finger.POINT_RIGHT,
					Finger.THUMB_LEFT, Finger.THUMB_RIGHT
					);
				break;
			case GestureType.LIGHT:
				IsCombinationPressedDown(
					Finger.PINK_LEFT, Finger.PINK_RIGHT,
					Finger.THUMB_LEFT, Finger.THUMB_RIGHT
					);
				break;
			case GestureType.DARK:
				IsCombinationPressedDown(
					Finger.PINK_LEFT, Finger.PINK_RIGHT,
					Finger.RING_LEFT, Finger.RING_RIGHT,
					Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT,
					Finger.POINT_LEFT, Finger.POINT_RIGHT
					);
				break;
			case GestureType.LOW:
				IsCombinationPressedDown(
					Finger.POINT_LEFT, Finger.POINT_RIGHT,
					Finger.THUMB_LEFT, Finger.THUMB_RIGHT
					);
				break;
			case GestureType.MEDIUM:
				IsCombinationPressedDown(
					Finger.RING_LEFT, Finger.RING_RIGHT,
					Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT,
					Finger.THUMB_LEFT, Finger.THUMB_RIGHT
					);
				break;
			case GestureType.HIGH:
				IsCombinationPressedDown(
					Finger.PINK_LEFT, Finger.PINK_RIGHT,
					Finger.POINT_LEFT, Finger.POINT_RIGHT,
					Finger.THUMB_LEFT, Finger.THUMB_RIGHT
					);
				break;
		}
		return false;
	}

	void Start() {
		int sampleSize = 10;
		gyroDataPoints = new List<Vector3>();
		for (int i = 0; i < sampleSize; i++) {
			gyroDataPoints.Add(Vector3.zero);
		}

		magnetoDataPoints = new List<Vector3>();
		for (int i = 0; i < sampleSize; i++) {
			magnetoDataPoints.Add(Vector3.zero);
		}

		acceloDataPoints = new List<Vector3>();
		for (int i = 0; i < sampleSize; i++) {
			acceloDataPoints.Add(Vector3.zero);
		}
	}

	void Update() {
		ParseData();
		UpdateAverageLists();
		// IsRigthSwingGesturePerformed();
		// IsLeftSwingGesturePerformed();
		// IsForwardGesturePerformed();
	}

	void ParseData() {
		bool leftHand = false;
		string rawData = serialController1.ReadSerialMessage();
		if (rawData == null || rawData.Length == 0) return;
		if (rawData.StartsWith("Lhand:")) leftHand = true;
		if (!leftHand && !rawData.StartsWith("Rhand:")) return;
		rawData = rawData.Replace("Lhand:", "");
		rawData = rawData.Replace("Rhand:", "");
		rawData = rawData.Trim('{', '}');
		string[] stringData = rawData.Split(';');
		float[] data = new float[stringData.Length];
		for (int i = 0; i < stringData.Length; i++) {
			data[i] = System.Single.Parse(stringData[i], System.Globalization.CultureInfo.InvariantCulture);
		}
		fingerData = new bool[] {
			data[0]> 0.5f,data[1]> 0.5f,data[2]> 0.5f,data[3]> 0.5f, data[4]> 0.5f,data[5]> 0.5f,
			// false, false, false, false, false
			true,true,true,true,true
		};
		magnetometerData = new Vector3(data[6], data[7], data[8]);
		gyroscopeData = new Vector3(data[9], data[10], data[11]);
		accelometerData = new Vector3(data[12], data[13], data[13]);
	}

	void UpdateAverageLists() {
		gyroDataPoints.RemoveAt(0);
		gyroDataPoints.Add(gyroscopeData);
		magnetoDataPoints.RemoveAt(0);
		magnetoDataPoints.Add(magnetometerData);
		acceloDataPoints.RemoveAt(0);
		acceloDataPoints.Add(accelometerData);
	}
}