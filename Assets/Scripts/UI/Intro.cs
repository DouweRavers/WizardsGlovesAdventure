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
    public Text txtIntro;
    public Text txtFire, txtDark, txtLight, txtEarth;

    public int numBlinks;
    public float Pause;

    bool inputAttackLocked = true;
    bool inputFightLocked = true;

    public GameObject gestureFire, gestureLight, gestureDark, gestureEarth;

    public Text element;

    public GameObject[] gameObjectsActivate;
    public GameObject[] gameObjectsDeactivate;
    public CinemachineVirtualCamera[] vCameras;

    // Start is called before the first frame update
    void Start()
    {
        imgIntro.enabled = false;
        imgWarning.enabled = false;
        txtIntro.enabled = false;
        
        GameManager.game.enemyFightData.tutorialEnabled = true;
        if (!GameManager.game.enemyFightData.tutorialEnabled)
        {
            for (int i = 0; i < gameObjectsActivate.Length; i++)
            {
                gameObjectsActivate[i].SetActive(true);
            }
            for (int i = 0; i < gameObjectsDeactivate.Length; i++)
            {
                gameObjectsDeactivate[i].SetActive(false);
            }
        }

        StartCoroutine(Blink());
    }

    
    void Update()
    {
        // LIGHT R+E+U+I 
        if (input.IsSpellGesturePerformed(GestureType.DARK) && !inputAttackLocked)
        {
            GameManager.game.playerFightData.element = elementType.Dark;
            introToFight();
        } else if (input.IsSpellGesturePerformed(GestureType.LIGHT) && !inputAttackLocked)
        {
            GameManager.game.playerFightData.element = elementType.Light;
            introToFight();
        } else if (input.IsSpellGesturePerformed(GestureType.FIRE) && !inputAttackLocked)
        {
            GameManager.game.playerFightData.element = elementType.Fire;
            introToFight();
        } else if (input.IsSpellGesturePerformed(GestureType.EARTH) && !inputAttackLocked)
        {
            GameManager.game.playerFightData.element = elementType.Earth;
            introToFight();
        }

        if (input.IsCombinationPressedDown(Finger.PINK_LEFT) && inputFightLocked/*input.IsForwardGesturePerformed()*/)
        {
            activateFight();
            changeCamera();
            deactivateIntro();
        }
    }

    void introToFight()
    {
        //disableUIElements();
        //txtIntro.text = "You unlocked a new attack, try it out!";
        //txtIntro.transform.position = new Vector2(600, 150);
        //yield return new WaitForSeconds(3);
        disableUIElements();
        txtIntro.text = "Unlocked a new attack. Try it out!";
        txtIntro.transform.position = new Vector2(475, 350);

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

        txtIntro.text = "Choose your element:";
        txtIntro.transform.position = new Vector2(600, 350);

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
        inputAttackLocked = false;
    }

    public IEnumerator HideImage(GameObject gesture)
    {
        yield return new WaitForSeconds(0);
        gesture.SetActive(false);
    }

    void disableUI()
    {
        imgIntro.enabled = false;
        imgWarning.enabled = false;
        txtIntro.enabled = false;
    } 
    void disableUIElements()
    {
        /*
        imgIntro.enabled = false;
        imgWarning.enabled = false;
        txtIntro.enabled = false;
        */
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
        Debug.Log("Length: " + gameObjectsActivate.Length);
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