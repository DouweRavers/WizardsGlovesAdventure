using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public SerialController serialControllerAlpha, serialControllerBeta;
    bool alphaIsLeft = false;
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
    public bool[] inputFingers
    {
        get
        {
            return new bool[] {
                leftPink, leftRing, leftMiddle, leftPoint, leftThumb,
                rightThumb, rightPoint, rightMiddle, rightRing, rightPink
            };
        }
    }

    public bool IsCombinationPressed(Finger[] fingers, int margin = 0)
    {
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
        return IsCombinationPressed(fingerArray, margin);
    }

    // Check if the exact specified combination is pressed/touched using enum values
    public bool IsCombinationPressed(
        Finger firstFinger = Finger.NONE, Finger secondFinger = Finger.NONE,
        Finger thirdthFinger = Finger.NONE, Finger forthFinger = Finger.NONE,
        Finger fifthFinger = Finger.NONE, Finger sixthFinger = Finger.NONE,
        Finger seventhFinger = Finger.NONE, Finger eigthFinger = Finger.NONE,
        Finger ninethFinger = Finger.NONE, Finger tenthFinger = Finger.NONE, int margin = 0)
    {
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
        return IsCombinationPressed(fingerArray, margin);
    }

    /*
		Check if the exact specified combination is pressed/touched using a boolean array
		The array start from the left pink at 0 to the right pink at 9. 

        left to right = array with expected input which the actual input is tested against.
	    margin = the amount of expected inputs that are allowed to missed. 0 means exact match the input 10 means all can miss and still be valid.
     */
    public bool IsCombinationPressed(bool[] leftToRightFingers, int margin = 0)
    {
        // skip value is enable
        if (leftToRightFingers[10]) return false;
        int hits = 0;
        int requiredHits = 0;
        for (int i = 0; i < 10; i++)
        {
            // a input value is true while it is expected false
            if (!leftToRightFingers[i] && inputFingers[i]) return false;
            if (leftToRightFingers[i]) requiredHits++;
            if (leftToRightFingers[i] && inputFingers[i]) hits++;
        }
        if (requiredHits <= hits + margin) return true;
        return false;
    }

    public bool IsCombinationPressedDown(Finger[] fingers, int margin = 0)
    {
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
        return IsCombinationPressedDown(fingerArray, margin);
    }

    public bool IsCombinationPressedDown(Finger firstFinger = Finger.NONE, Finger secondFinger = Finger.NONE,
        Finger thirdthFinger = Finger.NONE, Finger forthFinger = Finger.NONE,
        Finger fifthFinger = Finger.NONE, Finger sixthFinger = Finger.NONE,
        Finger seventhFinger = Finger.NONE, Finger eigthFinger = Finger.NONE,
        Finger ninethFinger = Finger.NONE, Finger tenthFinger = Finger.NONE, int margin = 0)
    {
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
        return IsCombinationPressedDown(fingerArray, margin);
    }

    public bool IsCombinationPressedDown(bool[] leftToRightFingers, int margin = 0)
    {
        bool result = IsCombinationPressed(leftToRightFingers, margin);
        if (pressed && Enumerable.SequenceEqual(leftToRightFingers, prevCom))
        {
            if (!result)
            {
                prevCom = null;
                pressed = false;
            }
            return false;
        }
        else
        {
            if (result)
            {
                prevCom = leftToRightFingers;
                pressed = true;
            }
            return result;
        }
    }

    public bool IsRigthSwingGesturePerformed(bool left = true)
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) return true;
        if (Time.realtimeSinceStartup - gestureTimer < 1f) return false;

        float frames = (0.1f / Time.unscaledDeltaTime) + 1;
        frames = frames >= sampleSize ? sampleSize - 1 : frames;
        float avgRotationChange = 0f;
        for (int i = sampleSize - (int)frames; i < sampleSize; i++)
        {
            avgRotationChange += Mathf.Abs(gyroDataPointsR[i].x);
        }
        avgRotationChange /= frames;
        if (avgRotationChange > 200f)
        {
            gestureTimer = Time.realtimeSinceStartup;
            if (!alphaIsLeft)
                serialControllerAlpha.SendSerialMessage("1");
            else
                serialControllerBeta.SendSerialMessage("1");
            return true;
        }
        return false;
    }

    public bool IsLeftSwingGesturePerformed(bool left = true)
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) return true;
        if (Time.realtimeSinceStartup - gestureTimer < 1f) return false;

        float frames = (0.1f / Time.unscaledDeltaTime) + 1;
        frames = frames >= sampleSize ? sampleSize - 1 : frames;
        float avgRotationChange = 0f;
        for (int i = sampleSize - (int)frames; i < sampleSize; i++)
        {
            avgRotationChange += Mathf.Abs(gyroDataPointsL[i].x);
        }
        avgRotationChange /= frames;
        if (avgRotationChange > 200f)
        {
            gestureTimer = Time.realtimeSinceStartup;
            if (alphaIsLeft)
                serialControllerAlpha.SendSerialMessage("1");
            else
                serialControllerBeta.SendSerialMessage("1");
            return true;
        }
        return false;
    }

    public bool IsForwardGesturePerformed()
    {
        if (Input.GetKeyDown(KeyCode.Return)) return true;
        if (Time.realtimeSinceStartup - gestureTimer < 1f) return false;

        float frames = (0.1f / Time.unscaledDeltaTime) + 1;
        frames = frames >= sampleSize ? sampleSize - 1 : frames;
        float avgRotationChangeL = 0f, avgRotationChangeR = 0f;
        for (int i = sampleSize - (int)frames; i < sampleSize; i++)
        {
            avgRotationChangeL += Mathf.Abs(gyroDataPointsL[i].z);
            avgRotationChangeR += Mathf.Abs(gyroDataPointsR[i].z);
        }
        avgRotationChangeL /= frames;
        avgRotationChangeR /= frames;
        if (avgRotationChangeL > 200f || avgRotationChangeR > 200f)
        {
            gestureTimer = Time.realtimeSinceStartup;
            if (avgRotationChangeL > 200f && alphaIsLeft)
                serialControllerAlpha.SendSerialMessage("1");
            else
                serialControllerBeta.SendSerialMessage("1");
            return true;
        }
        return false;
    }

    public bool IsAttackGesturePerformed()
    {
        if (Input.GetKeyDown(KeyCode.Space)) return true;
        if (Time.realtimeSinceStartup - gestureTimer < 1f) return false;

        float frames = (0.1f / Time.unscaledDeltaTime) + 1;
        frames = frames >= sampleSize ? sampleSize - 1 : frames;
        float avgAccelValueL = 0f, avgAccelValueR = 0f;
        for (int i = sampleSize - (int)frames; i < sampleSize; i++)
        {
            avgAccelValueL += Mathf.Abs(acceloDataPointsL[i].y);
            avgAccelValueR += Mathf.Abs(acceloDataPointsR[i].y);
        }
        avgAccelValueL /= frames;
        avgAccelValueR /= frames;
        if (avgAccelValueL > 1f || avgAccelValueR > 1f)
        {
            gestureTimer = Time.realtimeSinceStartup;
            if (avgAccelValueL > 1f && alphaIsLeft)
                serialControllerAlpha.SendSerialMessage("1");
            else
                serialControllerBeta.SendSerialMessage("1");
            return true;
        }
        return false;
    }

    public bool IsSpellGesturePerformed(GestureType gesture)
    {
        if (gesture == GestureType.FIRE && Input.GetKeyDown(KeyCode.F)) return true;
        if (gesture == GestureType.EARTH && Input.GetKeyDown(KeyCode.E)) return true;
        if (gesture == GestureType.LIGHT && Input.GetKeyDown(KeyCode.L)) return true;
        if (gesture == GestureType.DARK && Input.GetKeyDown(KeyCode.D)) return true;
        if (gesture == GestureType.LOW && Input.GetKeyDown(KeyCode.Alpha1)) return true;
        if (gesture == GestureType.MEDIUM && Input.GetKeyDown(KeyCode.Alpha2)) return true;
        if (gesture == GestureType.HIGH && Input.GetKeyDown(KeyCode.Alpha3)) return true;
        if (Time.realtimeSinceStartup - gestureTimer < 1f) return false;

        float frames = (0.1f / Time.unscaledDeltaTime) + 1;
        frames = frames >= sampleSize ? sampleSize - 1 : frames;
        Vector3 avgAccelValueL = Vector3.zero, avgAccelValueR = Vector3.zero;
        for (int i = sampleSize - (int)frames; i < sampleSize; i++)
        {
            avgAccelValueL += acceloDataPointsL[i];
            avgAccelValueR += acceloDataPointsR[i];
        }
        avgAccelValueL /= frames;
        avgAccelValueR /= frames;
        //if (avgAccelValueL.z > 0.5f)
        //{
            //if (avgAccelValueR.z > 0.5f && avgAccelValueR.y < 0.5f)
           // {
                if (gesture == GestureType.FIRE && IsCombinationPressedDown(
                    Finger.POINT_LEFT, Finger.POINT_RIGHT,
                    Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT,
                    margin: 3))
                {
                    gestureTimer = Time.realtimeSinceStartup;
                    serialControllerAlpha.SendSerialMessage("1");
                    serialControllerBeta.SendSerialMessage("1");
                    return true;
                }
            //}
           // if (avgAccelValueR.z < -0.5f)
            //{
                if (gesture == GestureType.DARK && IsCombinationPressedDown(
                    Finger.PINK_LEFT, Finger.PINK_RIGHT,
                    Finger.RING_LEFT, Finger.RING_RIGHT,
                    Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT,
                    Finger.POINT_LEFT, Finger.POINT_RIGHT,
                    margin: 5))
                {
                    gestureTimer = Time.realtimeSinceStartup;
                    serialControllerAlpha.SendSerialMessage("1");
                    serialControllerBeta.SendSerialMessage("1");
                    return true;
                }
            //}
        //}
        //if (avgAccelValueL.z < -0.5f && avgAccelValueR.z > 0.5f)
        //{
            if (gesture == GestureType.DARK && IsCombinationPressedDown(
                    Finger.PINK_LEFT, Finger.PINK_RIGHT,
                    Finger.RING_LEFT, Finger.RING_RIGHT,
                    Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT,
                    Finger.POINT_LEFT, Finger.POINT_RIGHT,
                    margin: 5))
            {
                gestureTimer = Time.realtimeSinceStartup;
                serialControllerAlpha.SendSerialMessage("1");
                serialControllerBeta.SendSerialMessage("1");
                return true;
            }
        //}
        //if (avgAccelValueL.z > 0.5f && avgAccelValueR.z > 0.5f)
        //{
            if (gesture == GestureType.LOW && IsCombinationPressedDown(
                Finger.POINT_LEFT, Finger.POINT_RIGHT,
                Finger.THUMB_LEFT, Finger.THUMB_RIGHT,
                margin: 3))
            {
                gestureTimer = Time.realtimeSinceStartup;
                serialControllerAlpha.SendSerialMessage("1");
                serialControllerBeta.SendSerialMessage("1");
                return true;
            }
        //}
        //else if (avgAccelValueL.y + avgAccelValueL.z > 0.7f && avgAccelValueR.y + avgAccelValueR.z > 0.7f)
        //{
            if (gesture == GestureType.EARTH && IsCombinationPressedDown(
                Finger.PINK_LEFT, Finger.PINK_RIGHT,
                Finger.RING_LEFT, Finger.RING_RIGHT,
                Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT,
                Finger.POINT_LEFT, Finger.POINT_RIGHT,
                Finger.THUMB_LEFT, Finger.THUMB_RIGHT,
                margin: 7))
            {
                gestureTimer = Time.realtimeSinceStartup;
                serialControllerAlpha.SendSerialMessage("1");
                serialControllerBeta.SendSerialMessage("1");
                return true;
            }
            if (gesture == GestureType.LIGHT && IsCombinationPressedDown(
                Finger.PINK_LEFT, Finger.PINK_RIGHT,
                Finger.THUMB_LEFT, Finger.THUMB_RIGHT,
                margin: 3))
            {
                gestureTimer = Time.realtimeSinceStartup;
                serialControllerAlpha.SendSerialMessage("1");
                serialControllerBeta.SendSerialMessage("1");
                return true;
            }
            if (gesture == GestureType.HIGH && IsCombinationPressedDown(
                Finger.PINK_LEFT, Finger.PINK_RIGHT,
                Finger.POINT_LEFT, Finger.POINT_RIGHT,
                Finger.THUMB_LEFT, Finger.THUMB_RIGHT,
                margin: 4))
            {
                gestureTimer = Time.realtimeSinceStartup;
                serialControllerAlpha.SendSerialMessage("1");
                serialControllerBeta.SendSerialMessage("1");
                return true;
            }
            if (gesture == GestureType.MEDIUM && IsCombinationPressedDown(
                Finger.RING_LEFT, Finger.RING_RIGHT,
                Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT,
                Finger.THUMB_LEFT, Finger.THUMB_RIGHT,
                margin: 4))
            {
                gestureTimer = Time.realtimeSinceStartup;
                serialControllerAlpha.SendSerialMessage("1");
                serialControllerBeta.SendSerialMessage("1");
                return true;
            }
            
        //}
            return false;
    }

    void Start()
    {
        gyroDataPointsL = new List<Vector3>();
        magnetoDataPointsL = new List<Vector3>();
        acceloDataPointsL = new List<Vector3>();
        gyroDataPointsR = new List<Vector3>();
        magnetoDataPointsR = new List<Vector3>();
        acceloDataPointsR = new List<Vector3>();
        for (int i = 0; i < sampleSize; i++)
        {
            gyroDataPointsL.Add(Vector3.zero);
            magnetoDataPointsL.Add(Vector3.zero);
            acceloDataPointsL.Add(Vector3.zero);
            gyroDataPointsR.Add(Vector3.zero);
            magnetoDataPointsR.Add(Vector3.zero);
            acceloDataPointsR.Add(Vector3.zero);
        }
        if (GameManager.game != null) serialControllerAlpha.portName = GameManager.game.COM1;
        if (GameManager.game != null) serialControllerBeta.portName = GameManager.game.COM2;
    }

    void Update()
    {
        ParseData();
        UpdateAverageLists();
    }

    void ParseData()
    {
        string[] rawDataSet = new string[] { serialControllerAlpha.ReadSerialMessage(), serialControllerBeta.ReadSerialMessage() };
        string[] stringDataL = new string[0], stringDataR = new string[0];
        for (int i = 0; i < 2; i++)
        {
            string rawData = rawDataSet[i];
            bool leftHand = false;
            if (rawData == null || rawData.Length == 0) continue;
            if (rawData.StartsWith("Lhand:")) leftHand = true;
            if (!leftHand && !rawData.StartsWith("Rhand:")) continue;
            if (leftHand)
            {
                stringDataL = rawData.Replace("Lhand:", "").Trim('{', '}').Split(';');
                alphaIsLeft = i == 0;
            }
            else
            {
                stringDataR = rawData.Replace("Rhand:", "").Trim('{', '}').Split(';');
            }
        }
        bool discardLeft = false, discardRight = false;
        float[] data = new float[28];
        if (stringDataL.Length == 0)
        {
            discardLeft = true;
            for (int i = 0; i < 14; i++)
            {
                data[i] = 0f;
            }
        }
        else
        {
            for (int i = 0; i < 14; i++)
            {
                data[i] = System.Single.Parse(stringDataL[i], System.Globalization.CultureInfo.InvariantCulture);
            }
        }
        if (stringDataR.Length == 0)
        {
            discardRight = true;
            for (int i = 14; i < 28; i++)
            {
                data[i] = 0f;
            }
        }
        else
        {
            for (int i = 14; i < 28; i++)
            {
                data[i] = System.Single.Parse(stringDataR[i - 14], System.Globalization.CultureInfo.InvariantCulture);
            }
        }
        if (!discardLeft)
        {
            fingerData[0] = data[0] > 0.5f;
            fingerData[1] = data[1] > 0.5f;
            fingerData[2] = data[2] > 0.5f;
            fingerData[3] = data[3] > 0.5f;
            fingerData[4] = data[4] > 0.5f;
        }
        if (!discardRight)
        {
            fingerData[5] = data[14] > 0.5f;
            fingerData[6] = data[15] > 0.5f;
            fingerData[7] = data[16] > 0.5f;
            fingerData[8] = data[17] > 0.5f;
            fingerData[9] = data[18] > 0.5f;
        }

        magnetometerDataL = new Vector3(data[5], data[6], data[7]);
        gyroscopeDataL = new Vector3(data[8], data[9], data[10]);
        accelometerDataL = new Vector3(data[11], data[12], data[13]);
        magnetometerDataR = new Vector3(data[19], data[20], data[21]);
        gyroscopeDataR = new Vector3(data[22], data[23], data[24]);
        accelometerDataR = new Vector3(data[25], data[26], data[27]);
    }

    void UpdateAverageLists()
    {
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