using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    public Image imgIntro;
    public Image imgWarning;
    public Text txtIntro;

    public int numBlinks;
    public float Pause;

    public InputManager input;
    bool toggler = true;
    bool inputLocked = true;

    public GameObject gestureFire, gestureLight, gestureDark, gestureEarth;

    public Text element;

    // Start is called before the first frame update
    void Start()
    {
        imgIntro.enabled = false;
        imgWarning.enabled = false;
        txtIntro.enabled = false;

        StartCoroutine(Blink());
    }

    
    void Update()
    {
        // DARK Q
        if (input.IsCombinationPressed(Finger.PINK_LEFT) && !inputLocked)
        //Finger.PINK_LEFT, Finger.PINK_RIGHT, Finger.RING_LEFT, Finger.RING_RIGHT, Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT, Finger.POINT_LEFT, Finger.POINT_RIGHT
        {
            if (toggler) return;
            else toggler = true;

            GameManager.game.LoadIntroToFightScene("Dark");

            //element.text = "Your element: Dark";
            // LIGHT P
        } else if (input.IsCombinationPressed(Finger.PINK_RIGHT) && !inputLocked)
        //Finger.PINK_LEFT, Finger.PINK_RIGHT, Finger.THUMB_LEFT, Finger.THUMB_RIGHT
        {
            if (toggler) return;
            else toggler = true;

            GameManager.game.LoadIntroToFightScene("Light");
            //element.text = "Your element: Light";
            //FIRE R
        } else if (input.IsCombinationPressed(Finger.POINT_LEFT) && !inputLocked)
        //Finger.POINT_LEFT, Finger.POINT_RIGHT, Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT
        {
            if (toggler) return;
            else toggler = true;

            GameManager.game.LoadIntroToFightScene("Fire");
            //element.text = "Your element: Fire";
            // EARTH W
        } else if (input.IsCombinationPressed(Finger.RING_LEFT) && !inputLocked)
        //Finger.PINK_LEFT, Finger.PINK_RIGHT, Finger.RING_LEFT, Finger.RING_RIGHT, Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT, Finger.POINT_LEFT, Finger.POINT_RIGHT, Finger.THUMB_LEFT, Finger.THUMB_RIGHT
        {
            if (toggler) return;
            else toggler = true;

            GameManager.game.LoadIntroToFightScene("Earth");
            //element.text = "Your element: Earth";
        } else
        {
            toggler = false;
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

        txtIntro.text = "Choose your element:";

        //videos afspelen
        gestureEarth.SetActive(true);

        foreach (VideoPlayer video in gestureEarth.GetComponents<VideoPlayer>())
        {
            video.Play();
        }

        gestureFire.SetActive(true);

        foreach (VideoPlayer video in gestureFire.GetComponents<VideoPlayer>())
        {
            video.Play();
        }

        gestureDark.SetActive(true);

        foreach (VideoPlayer video in gestureDark.GetComponents<VideoPlayer>())
        {
            video.Play();
        }

        gestureLight.SetActive(true);

        foreach (VideoPlayer video in gestureLight.GetComponents<VideoPlayer>())
        {
            video.Play();
        }


        inputLocked = false;
    }

    public IEnumerator HideImage(GameObject gesture)
    {
        yield return new WaitForSeconds(2);
        gesture.SetActive(false);
    }
}

//DARK Q, LIGHT P, FIRE R, EARTH W