using System;
using UnityEngine;
public enum Finger {
	PINK_LEFT, RING_LEFT, MIDDLE_LEFT, POINT_LEFT, THUMB_LEFT,
	THUMB_RIGHT, POINT_RIGHT, MIDDLE_RIGHT, RING_RIGHT, PINK_RIGHT,
	NONE
};

public class InputManager : MonoBehaviour {

	// A enumb containing every left and right finger


	public bool leftPink { get { return Input.GetKey("q") || Input.GetKey("a"); } }
	public bool leftRing { get { return Input.GetKey("w") || Input.GetKey("z"); } }
	public bool leftMiddle { get { return Input.GetKey("e"); } }
	public bool leftPoint { get { return Input.GetKey("r"); } }
	public bool leftThumb { get { return Input.GetKey("c"); } }

	public bool rightThumb { get { return Input.GetKey("n"); } }
	public bool rightPoint { get { return Input.GetKey("u"); } }
	public bool rightMiddle { get { return Input.GetKey("i"); } }
	public bool rightRing { get { return Input.GetKey("o"); } }
	public bool rightPink { get { return Input.GetKey("p"); } }

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
			Array.Exists(fingers, value => value == Finger.PINK_RIGHT)
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
		bool[] fingerArray = new bool[] { false, false, false, false, false, false, false, false, false, false };
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
			leftToRightFingers[9] == rightPink;
	}
}