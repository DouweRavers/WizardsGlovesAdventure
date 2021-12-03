using System;
using UnityEngine;
public enum Finger {
	PINK_LEFT = 0, RING_LEFT = 1, MIDDLE_LEFT = 2, POINT_LEFT = 3, THUMB_LEFT = 4,
	THUMB_RIGHT = 5, POINT_RIGHT = 6, MIDDLE_RIGHT = 7, RING_RIGHT = 8, PINK_RIGHT = 9,
	NONE = 10, BLOCK = 11
};

public class InputManager : MonoBehaviour {
	public SerialController serialController1;
	bool[] fingerData = new bool[] {
		false, false, false, false, false,
		false, false, false, false, false
	};
	Vector3 gyroData, magnoData, acceloData;

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

	void Update() {
		ParseData();
	}

	void ParseData() {
		string rawData = serialController1.ReadSerialMessage();
		if (rawData == null || rawData.Length == 0 || rawData[0] != '{') return;
		rawData = rawData.Trim('{', '}');
		string[] stringData = rawData.Split(';');
		float[] data = new float[stringData.Length];
		for (int i = 0; i < stringData.Length; i++) {
			data[i] = System.Single.Parse(stringData[i], System.Globalization.CultureInfo.InvariantCulture);
		}
		fingerData = new bool[] {
			false, false, false, false, false,
			data[0]> 0.5f,data[1]> 0.5f,data[2]> 0.5f,data[3]> 0.5f, data[4]> 0.5f,data[5]> 0.5f
		};
	}
}