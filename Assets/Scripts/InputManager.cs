using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	public enum Fingers
	{
		PINK_LEFT, RING_LEFT, MIDDLE_LEFT, POINT_LEFT, THUMB_LEFT,
		THUMB_RIGHT, POINT_RIGHT, MIDDLE_RIGHT, RING_RIGHT, PINK_RIGHT,
		NONE
	};
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

	public bool IsCombinationPressed(
		Fingers firstFinger = Fingers.NONE, Fingers secondFinger = Fingers.NONE,
		Fingers thirdthFinger = Fingers.NONE, Fingers forthFinger = Fingers.NONE)
	{
		bool[] fingerArray = new bool[] { false, false, false, false, false, false, false, false, false, false };
		if (firstFinger != Fingers.NONE) fingerArray[(int)firstFinger] = true;
		if (secondFinger != Fingers.NONE) fingerArray[(int)secondFinger] = true;
		if (thirdthFinger != Fingers.NONE) fingerArray[(int)thirdthFinger] = true;
		if (forthFinger != Fingers.NONE) fingerArray[(int)forthFinger] = true;
		return IsCombinationPressed(fingerArray);
	}

	public bool IsCombinationPressed(bool[] leftToRightFingers)
	{
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