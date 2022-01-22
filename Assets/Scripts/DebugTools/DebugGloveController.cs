using System.Collections.Generic;
using UnityEngine;

public class DebugGloveController : MonoBehaviour
{
    public InputManager inputManager;
    public bool left = true;

    int GraphHeight = 100, GraphWidth = 500;
    List<Vector3> gyroDataPoints, magnetoDataPoints, acceloDataPoints;

    string facing = "?";
    void Start()
    {
        gyroDataPoints = new List<Vector3>();
        for (int i = 0; i < GraphWidth; i++)
        {
            gyroDataPoints.Add(Vector3.zero);
        }

        magnetoDataPoints = new List<Vector3>();
        for (int i = 0; i < GraphWidth; i++)
        {
            magnetoDataPoints.Add(Vector3.zero);
        }

        acceloDataPoints = new List<Vector3>();
        for (int i = 0; i < GraphWidth; i++)
        {
            acceloDataPoints.Add(Vector3.zero);
        }
    }

    void Update()
    {
        GetDataFromInput();
        TestGestures();
    }

    void GetDataFromInput()
    {
        gyroDataPoints.RemoveAt(0);
        gyroDataPoints.Add(left ? inputManager.gyroscopeDataL : inputManager.gyroscopeDataR);
        magnetoDataPoints.RemoveAt(0);
        magnetoDataPoints.Add(left ? inputManager.magnetometerDataL : inputManager.magnetometerDataR);
        acceloDataPoints.RemoveAt(0);
        acceloDataPoints.Add(left ? inputManager.accelometerDataL : inputManager.accelometerDataR);
    }

    void TestGestures() {
       /* if (inputManager.IsRigthSwingGesturePerformed(left)) print("right");
        if (inputManager.IsLeftSwingGesturePerformed(left)) print("left");
        if (inputManager.IsForwardGesturePerformed()) print("forward");
        if (inputManager.IsSpellGesturePerformed(GestureType.FIRE)) print("fire");
        if (inputManager.IsSpellGesturePerformed(GestureType.EARTH)) print("earth");
        if (inputManager.IsSpellGesturePerformed(GestureType.LIGHT)) print("light");
        if (inputManager.IsSpellGesturePerformed(GestureType.DARK)) print("dark");*/
        if (inputManager.IsSpellGesturePerformed(GestureType.LOW)) print("low");
        if (inputManager.IsSpellGesturePerformed(GestureType.MEDIUM)) print("medium");
        if (inputManager.IsSpellGesturePerformed(GestureType.HIGH)) print("high");
        if (inputManager.IsAttackGesturePerformed()) print("attack");
    }

    void OnGUI()
    {
        //ShowGestureData();
        plotAxis();
    }

    
    void plotAxis()
    {
        Texture2D gyroGraph = new Texture2D(GraphWidth, GraphHeight);
        Texture2D magnetoGraph = new Texture2D(GraphWidth, GraphHeight);
        Texture2D acceloGraph = new Texture2D(GraphWidth, GraphHeight);

        for (int x = 0; x < gyroGraph.width; x++)
        {
            int Height = gyroGraph.height;
            int thresholdX = Mathf.RoundToInt(Height / 2 * gyroDataPoints[x].x / 1000);
            int thresholdY = Mathf.RoundToInt(Height / 2 * gyroDataPoints[x].y / 1000);
            int thresholdZ = Mathf.RoundToInt(Height / 2 * gyroDataPoints[x].z / 1000);
            for (int y = 0; y < Height; y++)
            {
                if (y == Height / 2) gyroGraph.SetPixel(x, y, Color.white);
                else if (y < Height / 2)
                {
                    gyroGraph.SetPixel(x, y, new Color(
                                                Height / 2 - Mathf.Abs(thresholdX) < y && thresholdX < 0 ? 1 : 0,
                                                Height / 2 - Mathf.Abs(thresholdY) < y && thresholdY < 0 ? 1 : 0,
                                                Height / 2 - Mathf.Abs(thresholdZ) < y && thresholdZ < 0 ? 1 : 0
                                            ));
                }
                else
                {
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
        for (int i = gyroDataPoints.Count - 10; i < gyroDataPoints.Count; i++)
        {
            avgMagnitude += gyroDataPoints[i].magnitude;
            avgX += gyroDataPoints[i].x;
            avgY += gyroDataPoints[i].y;
            avgZ += gyroDataPoints[i].z;
        }
        avgMagnitude /= 10; avgX /= 10; avgY /= 10; avgZ /= 10;
        GUILayout.Label("avg 10ms:\nX=" + avgX.ToString("#.##") + "\nY=" + avgY.ToString("#.##") + "\nZ=" + avgZ.ToString("#.##") + "\nL=" + avgMagnitude.ToString("#.##"));
        GUILayout.EndHorizontal();

        for (int x = 0; x < magnetoGraph.width; x++)
        {
            int Height = magnetoGraph.height;
            int thresholdX = Mathf.RoundToInt(magnetoGraph.height / 2 * magnetoDataPoints[x].x / 400);
            int thresholdY = Mathf.RoundToInt(magnetoGraph.height / 2 * magnetoDataPoints[x].y / 400);
            int thresholdZ = Mathf.RoundToInt(magnetoGraph.height / 2 * magnetoDataPoints[x].z / 400);
            for (int y = 0; y < Height; y++)
            {
                if (y == Height / 2) magnetoGraph.SetPixel(x, y, Color.white);
                else if (y < Height / 2)
                {
                    magnetoGraph.SetPixel(x, y, new Color(
                                                Height / 2 - Mathf.Abs(thresholdX) < y && thresholdX < 0 ? 1 : 0,
                                                Height / 2 - Mathf.Abs(thresholdY) < y && thresholdY < 0 ? 1 : 0,
                                                Height / 2 - Mathf.Abs(thresholdZ) < y && thresholdZ < 0 ? 1 : 0
                                            ));
                }
                else
                {
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
        for (int i = magnetoDataPoints.Count - 10; i < magnetoDataPoints.Count; i++)
        {
            avgMagnitude += magnetoDataPoints[i].magnitude;
            avgX += magnetoDataPoints[i].x;
            avgY += magnetoDataPoints[i].y;
            avgZ += magnetoDataPoints[i].z;
        }
        avgMagnitude /= 10; avgX /= 10; avgY /= 10; avgZ /= 10;
        GUILayout.Label("avg 10ms:\nX=" + avgX.ToString("#.##") + "\nY=" + avgY.ToString("#.##") + "\nZ=" + avgZ.ToString("#.##") + "\nL=" + avgMagnitude.ToString("#.##"));
        GUILayout.EndHorizontal();

        for (int x = 0; x < acceloGraph.width; x++)
        {
            int Height = acceloGraph.height;
            int thresholdX = Mathf.RoundToInt(acceloGraph.height / 2 * acceloDataPoints[x].x / 4);
            int thresholdY = Mathf.RoundToInt(acceloGraph.height / 2 * acceloDataPoints[x].y / 4);
            int thresholdZ = Mathf.RoundToInt(acceloGraph.height / 2 * acceloDataPoints[x].z / 4);
            for (int y = 0; y < Height; y++)
            {
                if (y == Height / 2) acceloGraph.SetPixel(x, y, Color.white);
                else if (y < Height / 2)
                {
                    acceloGraph.SetPixel(x, y, new Color(
                                                Height / 2 - Mathf.Abs(thresholdX) < y && thresholdX < 0 ? 1 : 0,
                                                Height / 2 - Mathf.Abs(thresholdY) < y && thresholdY < 0 ? 1 : 0,
                                                Height / 2 - Mathf.Abs(thresholdZ) < y && thresholdZ < 0 ? 1 : 0
                                            ));
                }
                else
                {
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
        for (int i = acceloDataPoints.Count - 10; i < acceloDataPoints.Count; i++)
        {
            avgMagnitude += acceloDataPoints[i].magnitude;
            avgX += acceloDataPoints[i].x;
            avgY += acceloDataPoints[i].y;
            avgZ += acceloDataPoints[i].z;
        }
        avgMagnitude /= 10; avgX /= 10; avgY /= 10; avgZ /= 10;
        GUILayout.Label("avg 10ms:\nX=" + avgX.ToString("#.##") + "\nY=" + avgY.ToString("#.##") + "\nZ=" + avgZ.ToString("#.##") + "\nL=" + avgMagnitude.ToString("#.##"));
        GUILayout.EndHorizontal();

    }

    void ShowGestureData() {
        Vector3 avg = Vector3.zero;
        int length = acceloDataPoints.Count;
        for (int i = length - (int)(1f / Time.unscaledDeltaTime); i < length; i++)
        {
            if (acceloDataPoints[i].sqrMagnitude != 0) avg += acceloDataPoints[i];
        }
        avg /= (int)(1f / Time.unscaledDeltaTime);
        if (avg.x < -0.7) facing = "right";
        if (avg.y < -0.7) facing = "backward";
        if (avg.z < -0.7) facing = "up";
        if (avg.x > 0.7) facing = "left";
        if (avg.y > 0.7) facing = "forward";
        if (avg.z > 0.7) facing = "down";
        GUILayout.Label("AcceloVector: " + avg + " thus palm facing " + facing);
    }
}