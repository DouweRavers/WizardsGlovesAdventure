using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Cinemachine;

public class Intro : MonoBehaviour
{
    public InputManager input;
    public GameObject[] GameObjectsTutorialEnabled { get { return gameObjectsDeactivate; } }
    public GameObject[] GameObjectsTutorialDisabled { get { return gameObjectsActivate; } }

    public Image imgIntro;
    public Image imgWarning;
    public Text txtIntro, txtIntro2;
    public Text txtFire, txtDark, txtLight, txtEarth;

    public int numBlinks;
    public float Pause;

    bool[] inputIntroElementLocked = new bool[] { true, true, true, true };
    bool inputIntroNewAttacksLocked = true;

    public GameObject gestureFire, gestureLight, gestureDark, gestureEarth;

    public Text element;

    public GameObject[] gameObjectsActivate;
    public GameObject[] gameObjectsDeactivate;
    public CinemachineVirtualCamera[] vCameras;

    // Start is called before the first frame update
    void Start()
    {
        txtIntro2.enabled = false;

        //DEBUG
        GameManager.game.enemyFightData.tutorialBeginnerEnabled = false;
        GameManager.game.playerFightData.unlockedAttacks = new int[] { 2, 3, 0, 0 };


        StartCoroutine(Blink());
    }

    
    void Update()
    {
        // LIGHT R+E+U+I 
        if (input.IsSpellGesturePerformed(GestureType.DARK) && !inputIntroElementLocked[0])
        {
            GameManager.game.playerFightData.element = elementType.Dark;
            disableIntroElements();
            transitionFight();
            //transitionIntroNewAttacks();
        } else if (input.IsSpellGesturePerformed(GestureType.LIGHT) && !inputIntroElementLocked[1])
        {
            GameManager.game.playerFightData.element = elementType.Light;
            disableIntroElements();
            transitionFight();
            //transitionIntroNewAttacks();
        } else if (input.IsSpellGesturePerformed(GestureType.FIRE) && !inputIntroElementLocked[2])
        {
            GameManager.game.playerFightData.element = elementType.Fire;
            disableIntroElements();
            transitionFight();
            //transitionIntroNewAttacks();
        } else if (input.IsSpellGesturePerformed(GestureType.EARTH) && !inputIntroElementLocked[3])
        {
            GameManager.game.playerFightData.element = elementType.Earth;
            disableIntroElements();
            transitionFight();
            //transitionIntroNewAttacks();
        }

        if (input.IsCombinationPressedDown(Finger.PINK_LEFT) && !inputIntroNewAttacksLocked/*input.IsForwardGesturePerformed()*/)
        {
            transitionFight();
        }
    }

    IEnumerator Blink()
    {
        txtIntro.text = "You encountered: " + GameManager.game.enemyFightData.enemyType.ToString().ToLower();
        txtIntro.enabled = true;
        imgIntro.enabled = true;

        for (int i = 0; i < numBlinks; i++)
        {
            imgWarning.enabled = !imgWarning.enabled;
            if (imgWarning.enabled == true)
            {
                FindObjectOfType<SoundManager>().Play("Alert");
            }

            yield return new WaitForSeconds(Pause);
        }
        imgWarning.enabled = true;

        yield return new WaitForSeconds(2);

        txtIntro.enabled = false;

        if(GameManager.game.enemyFightData.tutorialBeginnerEnabled)
        {
            transitionIntroElements();
        } else
        {
            transitionFight();
        }
    }

    void transitionIntroElements()
    {
        Debug.Log("yes");
        txtIntro2.text = "Choose your element:";

        displayElements(GameManager.game.playerFightData.unlockedAttacks);
        /*
        txtIntro2.enabled = true;
        txtDark.enabled = true;
        txtLight.enabled = true;
        txtFire.enabled = true;
        txtEarth.enabled = true;

        //videos afspelen
        gestureEarth.SetActive(true);
        foreach (VideoPlayer videoEarth in gestureEarth.GetComponents<VideoPlayer>())
        {
            videoEarth.Play();
        }

        gestureFire.SetActive(true);
        foreach (VideoPlayer videoFire in gestureFire.GetComponents<VideoPlayer>())
        {
            videoFire.Play();
        }

        gestureDark.SetActive(true);
        foreach (VideoPlayer videoDark in gestureDark.GetComponents<VideoPlayer>())
        {
            videoDark.Play();
        }

        gestureLight.SetActive(true);
        foreach (VideoPlayer videoLight in gestureLight.GetComponents<VideoPlayer>())
        {
            videoLight.Play();
        }
        inputIntroElementLocked = false;
        */
    }

    void displayElements(int[] unlockedElements) 
    {
        txtIntro2.enabled = true;
        if (unlockedElements[0] > 0)
        {
            gestureDark.SetActive(true);
            foreach (VideoPlayer video in gestureDark.GetComponents<VideoPlayer>())
            {
                video.Play();
            }
            txtDark.enabled = true;
            inputIntroElementLocked[0] = false;
        }
        if (unlockedElements[1] > 0)
        {
            gestureLight.SetActive(true);
            foreach (VideoPlayer video in gestureLight.GetComponents<VideoPlayer>())
            {
                video.Play();
            }
            txtLight.enabled = true;
            inputIntroElementLocked[1] = false;
        }
        if (unlockedElements[2] > 0)
        {
            gestureFire.SetActive(true);
            foreach (VideoPlayer video in gestureFire.GetComponents<VideoPlayer>())
            {
                video.Play();
            }
            txtFire.enabled = true;
            inputIntroElementLocked[2] = false;
        }
        if (unlockedElements[3] > 0)
        {
            gestureEarth.SetActive(true);
            foreach (VideoPlayer video in gestureEarth.GetComponents<VideoPlayer>())
            {
                video.Play();
            }
            txtEarth.enabled = true;
            inputIntroElementLocked[3] = false;
        }
    }

    void transitionIntroNewAttacks()
    {
        //disableUIElements();
        //txtIntro.text = "You unlocked a new attack, try it out!";
        //txtIntro.transform.position = new Vector2(600, 150);
        //yield return new WaitForSeconds(3);
        disableIntroElements();
        txtIntro2.text = "Unlocked new attacks. Try it out!";
        txtIntro2.enabled = true;
        inputIntroNewAttacksLocked = false;
    }

    void transitionFight()
    {
        activateFight();
        changeCamera();
        deactivateIntro();
    }


    public IEnumerator HideImage(GameObject gesture)
    {
        yield return new WaitForSeconds(0);
        gesture.SetActive(false);
    }

    void disableIntroElements()
    {
        txtDark.enabled = false;
        txtLight.enabled = false;
        txtFire.enabled = false;
        txtEarth.enabled = false;
        StartCoroutine(HideImage(gestureDark));
        StartCoroutine(HideImage(gestureLight));
        StartCoroutine(HideImage(gestureFire));
        StartCoroutine(HideImage(gestureEarth));
    }

    void activateFight()
    {
        for (int i = 0; i < gameObjectsActivate.Length; i++)
        {
            Debug.Log(i);
            gameObjectsActivate[i].SetActive(true);
        }
    }
    void deactivateIntro()
    {
        for(int i = 0; i < gameObjectsDeactivate.Length; i++)
        {
            gameObjectsDeactivate[i].SetActive(false);
        }
    }

    void changeCamera()
    {
        vCameras[0].Priority = 20;
        vCameras[1].Priority = 40;
    }
}

//DARK Q, LIGHT P, FIRE R, EARTH W