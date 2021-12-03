using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if (input.IsCombinationPressed(Finger.PINK_LEFT, Finger.PINK_RIGHT, Finger.RING_LEFT, Finger.RING_RIGHT, Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT, Finger.POINT_LEFT, Finger.POINT_RIGHT) && !inputLocked)
        {
            if (toggler) return;
            else toggler = true;

            GameManager.game.LoadIntroToFightScene("Dark");
        } else if (input.IsCombinationPressed(Finger.PINK_LEFT, Finger.PINK_RIGHT, Finger.THUMB_LEFT, Finger.THUMB_RIGHT) && !inputLocked)
        {
            if (toggler) return;
            else toggler = true;

            GameManager.game.LoadIntroToFightScene("Light");
        //e+r+u+i
        } else if (input.IsCombinationPressed(Finger.POINT_LEFT, Finger.POINT_RIGHT, Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT) && !inputLocked)
        {
            if (toggler) return;
            else toggler = true;

            GameManager.game.LoadIntroToFightScene("Fire");
        } else if (input.IsCombinationPressed(Finger.PINK_LEFT, Finger.PINK_RIGHT, Finger.RING_LEFT, Finger.RING_RIGHT, Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT, Finger.POINT_LEFT, Finger.POINT_RIGHT, Finger.THUMB_LEFT, Finger.THUMB_RIGHT) && !inputLocked)
        {
            if (toggler) return;
            else toggler = true;

            GameManager.game.LoadIntroToFightScene("Earth");
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
        inputLocked = false;
    }
}
