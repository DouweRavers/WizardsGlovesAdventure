using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	public SerialController serialControllerAlpha, serialControllerBeta;
	bool[] fingerData = new bool[] {
		false, false, false, false, false,
		false, false, false, false, false
	};

	bool pressed = false;
	bool[] prevCom = null;
	[HideInInspector]
	public Vector3 gyroscopeDataL, magnetometerDataL, accelometerDataL, gyroscopeDataR, magnetometerDataR, accelometerDataR;
	int sampleSize = 10;
	List<Vector3> gyroDataPointsL, magnetoDataPointsL, acceloDataPointsL, gyroDataPointsR, magnetoDataPointsR, acceloDataPointsR;
	float gestureTimer = 0f;

	public bool leftPink { get { return Input.GetKey("q") || Input.GetKey("a") || fingerData[4]; } }
	public bool leftRing { get { return Input.GetKey("w") || Input.GetKey("z") || fingerData[3]; } }
	public bool leftMiddle { get { return Input.GetKey("e") || fingerData[2]; } }
	public bool leftPoint { get { return Input.GetKey("r") || fingerData[1]; } }
	public bool leftThumb { get { return Input.GetKey("c") || fingerData[0]; } }

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
		print(fingerArray.ToString());
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

	public bool IsRigthSwingGesturePerformed(bool left = true) {
		if (Input.GetKeyDown(KeyCode.RightArrow)) return true;
		if (Time.realtimeSinceStartup - gestureTimer < 2f) return false;
		float avg = 0f;
		for (int i = 0; i < sampleSize; i++) {
			avg += left ? acceloDataPointsL[i].x : acceloDataPointsR[i].x;
		}
		avg /= sampleSize;
		if (left ? avg < -0.5f : avg > 0.5f) {
			avg = 0f;
			for (int i = 0; i < sampleSize; i++) {
				avg += left ? gyroDataPointsL[i].x : gyroDataPointsR[i].x;
			}
			avg /= sampleSize;
			if (left ? avg > 60 : avg < -60) {
				gestureTimer = Time.realtimeSinceStartup;
				serialControllerAlpha.SendSerialMessage("1");
				serialControllerBeta.SendSerialMessage("1");
				return true;
			}
		}
		return false;
	}

	public bool IsLeftSwingGesturePerformed(bool left = true) {
		if (Input.GetKeyDown(KeyCode.LeftArrow)) return true;
		if (Time.realtimeSinceStartup - gestureTimer < 2f) return false;
		float avg = 0f;
		for (int i = 0; i < sampleSize; i++) {
			avg += left ? acceloDataPointsL[i].x : acceloDataPointsR[i].x;
		}
		avg /= sampleSize;
		if (left ? avg < -0.5f : avg > 0.5f) {
			avg = 0f;
			for (int i = 0; i < sampleSize; i++) {
				avg += left ? gyroDataPointsL[i].x : gyroDataPointsR[i].x;
			}
			avg /= sampleSize;
			if (left ? avg < -60 : avg > 60) {
				gestureTimer = Time.realtimeSinceStartup;
				serialControllerAlpha.SendSerialMessage("1");
				serialControllerBeta.SendSerialMessage("1");
				return true;
			}
		}
		return false;
	}

	public bool IsForwardGesturePerformed(bool left = true) {
		if (Input.GetKeyDown(KeyCode.Return)) return true;
		if (Time.realtimeSinceStartup - gestureTimer < 2f) return false;
		float avg = 0f;
		for (int i = 0; i < sampleSize; i++) {
			avg += left ? acceloDataPointsL[i].x : acceloDataPointsR[i].x;
		}
		avg /= sampleSize;
		if (avg < (left ? -0.5f : 0.5f)) {
			avg = 0f;
			for (int i = 0; i < sampleSize; i++) {
				avg += left ? gyroDataPointsL[i].z : gyroDataPointsR[i].z;
			}
			avg /= sampleSize;
			if (left ? avg > 35 : avg < -35) {
				gestureTimer = Time.realtimeSinceStartup;
				serialControllerAlpha.SendSerialMessage("1");
				serialControllerBeta.SendSerialMessage("1");
				return true;
			}
		}
		return false;
	}

	public bool IsSpellGesturePerformed(GestureType gesture) {
		if (Time.realtimeSinceStartup - gestureTimer < 2f) return false;
		switch (gesture) {
			case GestureType.FIRE:
				return IsCombinationPressedDown(
					Finger.POINT_LEFT, Finger.POINT_RIGHT,
					Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT
					);
			case GestureType.EARTH:
				return IsCombinationPressedDown(
					Finger.PINK_LEFT, Finger.PINK_RIGHT,
					Finger.RING_LEFT, Finger.RING_RIGHT,
					Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT,
					Finger.POINT_LEFT, Finger.POINT_RIGHT,
					Finger.THUMB_LEFT, Finger.THUMB_RIGHT
					);
			case GestureType.LIGHT:
				return IsCombinationPressedDown(
					Finger.PINK_LEFT, Finger.PINK_RIGHT,
					Finger.THUMB_LEFT, Finger.THUMB_RIGHT
					);
			case GestureType.DARK:
				return IsCombinationPressedDown(
					Finger.PINK_LEFT, Finger.PINK_RIGHT,
					Finger.RING_LEFT, Finger.RING_RIGHT,
					Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT,
					Finger.POINT_LEFT, Finger.POINT_RIGHT
					);
			case GestureType.LOW:
				return IsCombinationPressedDown(
					Finger.POINT_LEFT, Finger.POINT_RIGHT,
					Finger.THUMB_LEFT, Finger.THUMB_RIGHT
					);
			case GestureType.MEDIUM:
				return IsCombinationPressedDown(
					Finger.RING_LEFT, Finger.RING_RIGHT,
					Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT,
					Finger.THUMB_LEFT, Finger.THUMB_RIGHT
					);
			case GestureType.HIGH:
				return IsCombinationPressedDown(
					Finger.PINK_LEFT, Finger.PINK_RIGHT,
					Finger.POINT_LEFT, Finger.POINT_RIGHT,
					Finger.THUMB_LEFT, Finger.THUMB_RIGHT
					);
			default:
				break;
		}
		return false;
	}

	void Start() {
		gyroDataPointsL = new List<Vector3>();
		magnetoDataPointsL = new List<Vector3>();
		acceloDataPointsL = new List<Vector3>();
		gyroDataPointsR = new List<Vector3>();
		magnetoDataPointsR = new List<Vector3>();
		acceloDataPointsR = new List<Vector3>();
		for (int i = 0; i < sampleSize; i++) {
			gyroDataPointsL.Add(Vector3.zero);
			magnetoDataPointsL.Add(Vector3.zero);
			acceloDataPointsL.Add(Vector3.zero);
			gyroDataPointsR.Add(Vector3.zero);
			magnetoDataPointsR.Add(Vector3.zero);
			acceloDataPointsR.Add(Vector3.zero);
		}
		serialControllerAlpha.portName = GameManager.game.COM1;
		serialControllerBeta.portName = GameManager.game.COM2;
	}

	void Update() {
		ParseData();
		UpdateAverageLists();
	}

	void ParseData() {
		string[] rawDataSet = new string[] { serialControllerAlpha.ReadSerialMessage(), serialControllerBeta.ReadSerialMessage() };
		string[] stringDataL = new string[0], stringDataR = new string[0];
		foreach (string rawData in rawDataSet) {
			bool leftHand = false;
			if (rawData == null || rawData.Length == 0) continue;
			if (rawData.StartsWith("Lhand:")) leftHand = true;
			if (!leftHand && !rawData.StartsWith("Rhand:")) continue;
			if (leftHand) {
				stringDataL = rawData.Replace("Lhand:", "").Trim('{', '}').Split(';');
			} else {
				stringDataR = rawData.Replace("Rhand:", "").Trim('{', '}').Split(';');
			}
		}

		float[] data = new float[28];
		if (stringDataL.Length == 0) {
			for (int i = 0; i < 14; i++) {
				data[i] = 0f;
			}
		} else {
			for (int i = 0; i < 14; i++) {
				data[i] = System.Single.Parse(stringDataL[i], System.Globalization.CultureInfo.InvariantCulture);
			}
		}
		if (stringDataR.Length == 0) {
			for (int i = 14; i < 28; i++) {
				data[i] = 0f;
			}
		} else {
			for (int i = 14; i < 28; i++) {
				data[i] = System.Single.Parse(stringDataR[i - 14], System.Globalization.CultureInfo.InvariantCulture);
			}
		}

		fingerData = new bool[] {
			data[0]> 0.5f,data[1]> 0.5f,data[2]> 0.5f,data[3]> 0.5f, data[4]> 0.5f,
			data[14]> 0.5f,data[15]> 0.5f,data[16]> 0.5f,data[17]> 0.5f, data[18]> 0.5f,
		};
		magnetometerDataL = new Vector3(data[5], data[6], data[7]);
		gyroscopeDataL = new Vector3(data[8], data[9], data[10]);
		accelometerDataL = new Vector3(data[11], data[12], data[13]);
		magnetometerDataR = new Vector3(data[19], data[20], data[21]);
		gyroscopeDataR = new Vector3(data[22], data[23], data[24]);
		accelometerDataR = new Vector3(data[25], data[26], data[27]);
	}

	void UpdateAverageLists() {
		gyroDataPointsL.RemoveAt(0);
		gyroDataPointsL.Add(gyroscopeDataL);
		magnetoDataPointsL.RemoveAt(0);
		magnetoDataPointsL.Add(magnetometerDataL);
		acceloDataPointsL.RemoveAt(0);
		acceloDataPointsL.Add(accelometerDataL);
		gyroDataPointsR.RemoveAt(0);
		gyroDataPointsR.Add(gyroscopeDataR);
		magnetoDataPointsR.RemoveAt(0);
		magnetoDataPointsR.Add(magnetometerDataR);
		acceloDataPointsR.RemoveAt(0);
		acceloDataPointsR.Add(accelometerDataR);
	}
}