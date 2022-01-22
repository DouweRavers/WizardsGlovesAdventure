using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AttackingPlayer : MonoBehaviour
{
    public static AttackingPlayer player;
    public InputManager input;

    public float health { get { return healthPoints; } }
    public int mana { get { return manaPoints; } }

    int x, y, z = 0;
    public float damageLow = 15;
    public float damageMedium = 25;
    public float damageHigh = 30;
    bool fireImmune = false, lightImmune = false, darkImmune = false, earthImmune = false;
    bool tutorialImmunePlayed = false;

    public float healthPoints = 100;
    public float healing = 10;
    bool alertHP = false;

    public int manaPoints = 2000;
    public int manaLow = 50;
    public int manaMedium = 100;
    public int manaHigh = 150;
    bool noMoreMana = false;
    bool alertM = false;
    bool performAttack = false;
    bool selectAttacks = false;
    bool selectElements = true;

    public ParticleSystem fireAttackLow, fireAttackMedium, fireAttackHigh, lightAttackLow, lightAttackMedium, lightAttackHigh, darkAttackLow, darkAttackMedium, darkAttackHigh, earthAttackLow, earthAttackMedium, earthAttackHigh;
    public ParticleSystem heal;
    public Transform enemy;
    public GameObject[] gestureAttacks;
    public GameObject gestureFire, gestureDark, gestureLight, gestureEarth;
    public GameObject gestureAttackPerform;
    elementType curElement;

    public Text element;
    public Text txtFeedback;
    public Image imgFeedback;
    public Text[] Tutorial;
    public Text[] AttackOrElement;
    public Image imgTutorial, imgTutorialWarning;

    public GameObject introManager;

    public CinemachineVirtualCamera[] vCameras;

    void Awake()
    {
        player = this;
    }

    void Start()
    {
        GameManager.game.playerFightData.attack = attackType.NONE;
        EnableUITutorial(false);
        if (GameManager.game.enemyFightData.tutorialBeginnerEnabled)
        {
            enableAttackVids(GameManager.game.playerFightData.element, false);
        }
        else
        {
            displayElements(GameManager.game.storyData.spells);
        }
        txtFeedback.text = "Start the fight";
    }

    void Update()
    {
        if (GameManager.game.playerFightData.element == elementType.None) SelectElement();
        else if (GameManager.game.playerFightData.attack == attackType.NONE) SelectAttackStrenght();
        else CheckAndPerformAttack();
        if (mana <= 100 && !alertM)
        {
            alertM = true;
            StartCoroutine(manaAlert());
        }
        if (healthPoints <= 25 && !alertHP)
        {
            alertHP = true;
            StartCoroutine(healthAlert());
        }
    }
    
    //FUNCTIONS TO CHECK AND ACT EVERY FRAME

    void SelectElement()
    {
        if (input.IsSpellGesturePerformed(GestureType.DARK))
        {
            Debug.Log("Dark selected");
            if (checkUnlockedElement(elementType.Dark) && selectElements == true)
            {
                GameManager.game.playerFightData.element = elementType.Dark;
                GameManager.game.playerFightData.attack = attackType.NONE;
                curElement = elementType.Dark;

                element.text = "Your element: Dark";
                txtFeedback.text = "Switched to Dark element";

                enableAttackVids(elementType.Dark, false);

                x = y = z = 0;

                gestureDark.SetActive(true);
                float time = 0;
                foreach (VideoPlayer video in gestureDark.GetComponents<VideoPlayer>())
                {
                    video.Play();
                    time = (float)video.length;
                }
                StartCoroutine(HideImage(time, gestureDark));
            }
        }
        else if (input.IsSpellGesturePerformed(GestureType.LIGHT))
        {
            Debug.Log("Light selected");
            if (checkUnlockedElement(elementType.Light) && selectElements == true)
            {
                GameManager.game.playerFightData.element = elementType.Light;
                GameManager.game.playerFightData.attack = attackType.NONE;
                curElement = elementType.Light;

                element.text = "Your element: Light";
                txtFeedback.text = "Switched to Light element";

                enableAttackVids(elementType.Light, false);

                x = y = z = 0;

                gestureLight.SetActive(true);
                float time = 0;
                foreach (VideoPlayer video in gestureLight.GetComponents<VideoPlayer>())
                {
                    video.Play();
                    time = (float)video.length;
                }
                StartCoroutine(HideImage(time, gestureLight));
            }
        }
        else if (input.IsSpellGesturePerformed(GestureType.FIRE))
        {
            Debug.Log("Fire selected");
            if (checkUnlockedElement(elementType.Fire) && selectElements == true)
            {
                GameManager.game.playerFightData.element = elementType.Fire;
                GameManager.game.playerFightData.attack = attackType.NONE;
                curElement = elementType.Fire;

                element.text = "Your element: Fire";
                txtFeedback.text = "Switched to Fire element";

                enableAttackVids(elementType.Fire, false);

                x = y = z = 0;

                gestureFire.SetActive(true);
                float time = 0;
                foreach (VideoPlayer video in gestureFire.GetComponents<VideoPlayer>())
                {
                    video.Play();
                    time = (float)video.length;
                }
                StartCoroutine(HideImage(time, gestureFire));
            }
        }
        else if (input.IsSpellGesturePerformed(GestureType.EARTH))
        {
            Debug.Log("Earth selected");
            if (checkUnlockedElement(elementType.Fire) && selectElements == true)
            {
                GameManager.game.playerFightData.element = elementType.Earth;
                GameManager.game.playerFightData.attack = attackType.NONE;
                curElement = elementType.Earth;

                element.text = "Your element: Earth";
                txtFeedback.text = "Switched to Earth element";

                enableAttackVids(elementType.Earth, false);

                x = y = z = 0;

                gestureEarth.SetActive(true);
                float time = 0;
                foreach (VideoPlayer video in gestureEarth.GetComponents<VideoPlayer>())
                {
                    video.Play();
                    time = (float)video.length;
                }
                StartCoroutine(HideImage(time, gestureEarth));
            }
        }
    }

    void SelectAttackStrenght()
    {
        if (input.IsSpellGesturePerformed(GestureType.LOW))
        {
            Debug.Log("low Selected");
            if (checkUnlocked(0) && selectAttacks)
            {
                GameManager.game.playerFightData.attack = attackType.LOW;
                txtFeedback.text = "Switched to low damage attack";
                displayAttackPerformVids();
            }
        }
        else if (input.IsSpellGesturePerformed(GestureType.MEDIUM))
        {
            if (checkUnlocked(1) && selectAttacks == true)
            {
                Debug.Log("Medium selected");
                GameManager.game.playerFightData.attack = attackType.MEDIUM;
                txtFeedback.text = "Switched to medium damage attack";
                displayAttackPerformVids();
            }
        }
        else if (input.IsSpellGesturePerformed(GestureType.HIGH))
        {
            if (checkUnlocked(2) && selectAttacks == true)
            {
                Debug.Log("High selected");
                GameManager.game.playerFightData.attack = attackType.HIGH;
                txtFeedback.text = "Switched to High damage attack";
                displayAttackPerformVids();
            }
        }
    }

    void CheckAndPerformAttack()
    {
        if (input.IsAttackGesturePerformed() && performAttack == true)
        {
            switch (GameManager.game.playerFightData.attack)
            {
                case attackType.HIGH:
                    z++;
                    x = y = 0;

                    if (GameManager.game.playerFightData.element == elementType.Fire && checkMana(manaHigh))
                    {
                        if (z > 3)
                        {
                            fireImmune = true;
                            //probably why the tutorial always showed
                            StartCoroutine(EnemyImmuneTutorial());
                        }

                        fireAttackHigh.Play();
                        FindObjectOfType<SoundManager>().Play("PlayerAttackFireHigh");
                        FightManager.fight.HitEnemy(damageHigh, z, fireImmune);
                    }
                    else if (GameManager.game.playerFightData.element == elementType.Earth && checkMana(manaHigh))
                    {
                        if (z > 3)
                        {
                            earthImmune = true;
                            StartCoroutine(EnemyImmuneTutorial());
                        }

                        earthAttackHigh.Play();
                        FindObjectOfType<SoundManager>().Play("PlayerAttackEarth");
                        FightManager.fight.HitEnemy(damageHigh, z, earthImmune);
                    }
                    else if (GameManager.game.playerFightData.element == elementType.Dark && checkMana(manaHigh))
                    {
                        if (z > 3)
                        {
                            darkImmune = true;
                            StartCoroutine(EnemyImmuneTutorial());
                        }

                        darkAttackHigh.Play();
                        FindObjectOfType<SoundManager>().Play("PlayerAttackDarkHigh");
                        FightManager.fight.HitEnemy(damageHigh, z, darkImmune);
                    }
                    if (GameManager.game.playerFightData.element == elementType.Light && checkMana(manaHigh))
                    {
                        if (z > 3)
                        {
                            lightImmune = true;
                            StartCoroutine(EnemyImmuneTutorial());
                        }

                        lightAttackHigh.Play();
                        FindObjectOfType<SoundManager>().Play("PlayerAttackLightHigh");
                        FightManager.fight.HitEnemy(damageHigh, z, lightImmune);
                    }
                    break;
                case attackType.MEDIUM:
                    y++;
                    x = z = 0;

                    if (GameManager.game.playerFightData.element == elementType.Fire && checkMana(manaMedium))
                    {
                        if (y > 3)
                        {
                            fireImmune = true;
                            StartCoroutine(EnemyImmuneTutorial());
                        }

                        fireAttackMedium.Play();
                        FindObjectOfType<SoundManager>().Play("PlayerAttackFireMedium");
                        FightManager.fight.HitEnemy(damageMedium, y, fireImmune);
                    }
                    else if (GameManager.game.playerFightData.element == elementType.Earth && checkMana(manaMedium))
                    {
                        if (y > 3)
                        {
                            earthImmune = true;
                            StartCoroutine(EnemyImmuneTutorial());
                        }

                        earthAttackMedium.Play();
                        FindObjectOfType<SoundManager>().Play("PlayerAttackEarth");
                        FightManager.fight.HitEnemy(damageMedium, y, earthImmune);
                    }
                    else if (GameManager.game.playerFightData.element == elementType.Dark && checkMana(manaMedium))
                    {
                        if (y > 3)
                        {
                            darkImmune = true;
                            StartCoroutine(EnemyImmuneTutorial());
                        }

                        darkAttackMedium.Play();
                        FindObjectOfType<SoundManager>().Play("PlayerAttackDarkMedium");
                        FightManager.fight.HitEnemy(damageMedium, y, darkImmune);
                    }
                    if (GameManager.game.playerFightData.element == elementType.Light && checkMana(manaMedium))
                    {
                        if (y > 3)
                        {
                            lightImmune = true;
                            StartCoroutine(EnemyImmuneTutorial());
                        }

                        lightAttackMedium.Play();
                        FindObjectOfType<SoundManager>().Play("PlayerAttackLightMedium");
                        FightManager.fight.HitEnemy(damageMedium, y, lightImmune);
                    }
                    break;
                case attackType.LOW:
                    x++;
                    y = z = 0;

                    if (GameManager.game.playerFightData.element == elementType.Fire && checkMana(manaLow))
                    {
                        if (x > 3)
                        {
                            fireImmune = true;
                            StartCoroutine(EnemyImmuneTutorial());
                        }

                        fireAttackLow.Play();
                        FindObjectOfType<SoundManager>().Play("PlayerAttackFireLow");
                        FightManager.fight.HitEnemy(damageLow, x, fireImmune);
                    }
                    else if (GameManager.game.playerFightData.element == elementType.Earth && checkMana(manaLow))
                    {
                        if (x > 3)
                        {
                            earthImmune = true;
                            StartCoroutine(EnemyImmuneTutorial());
                        }

                        earthAttackLow.Play();
                        FindObjectOfType<SoundManager>().Play("PlayerAttackEarth");
                        FightManager.fight.HitEnemy(damageLow, x, earthImmune);
                    }
                    else if (GameManager.game.playerFightData.element == elementType.Dark && checkMana(manaLow))
                    {
                        if (x > 3)
                        {
                            darkImmune = true;
                            StartCoroutine(EnemyImmuneTutorial());
                        }

                        darkAttackLow.Play();
                        FindObjectOfType<SoundManager>().Play("PlayerAttackDarkLow");
                        FightManager.fight.HitEnemy(damageLow, x, darkImmune);
                    }
                    if (GameManager.game.playerFightData.element == elementType.Light && checkMana(manaLow))
                    {
                        if (x > 3)
                        {
                            lightImmune = true;
                            StartCoroutine(EnemyImmuneTutorial());
                        }

                        lightAttackLow.Play();
                        FindObjectOfType<SoundManager>().Play("PlayerAttackLightLow");
                        FightManager.fight.HitEnemy(damageLow, x, lightImmune);
                    }
                    break;
                case attackType.NONE:
                    txtFeedback.text = "No attack selected";
                    break;
            }
            GameManager.game.playerFightData.element = elementType.None;
            GameManager.game.playerFightData.attack = attackType.NONE;
            //Go back to showing elements
            displayElements(GameManager.game.storyData.spells);
        }
    }

    //FUNCTIONS TO DISPLAY VIDEOS
    void displayAttackPerformVids()
    {
        hideAttacks();
        selectAttacks = false;
        performAttack = true;

        gestureAttackPerform.SetActive(true);
        foreach (VideoPlayer video in gestureAttackPerform.GetComponents<VideoPlayer>())
        {
            video.Play();
        }

        AttackOrElement[0].text = "Perform attack!";
        for (int i = 1; i < AttackOrElement.Length; i++)
        {
            AttackOrElement[i].text = "";
        }
    }
    void enableAttackVids(elementType element, bool fromAttackPerform)
    {
        //hideAttacks();
        if (fromAttackPerform)
        {
            hideAttackPerform();
        }
        else
        {
            hideElements();
            selectElements = false;
            selectAttacks = true;
        }


        switch (element)
        {
            case elementType.Dark:
                displayAttacks(3);
                break;
            case elementType.Light:
                displayAttacks(2);
                break;
            case elementType.Fire:
                displayAttacks(0);
                break;
            case elementType.Earth:
                displayAttacks(1);
                break;
        }

    }
    void displayAttacks(int element)
    {
        AttackOrElement[0].text = "Attacks:";
        enableAttackOrElementUI(true);

        int level = GameManager.game.storyData.spells[element];
        switch (level)
        {
            case 0:
                gestureAttacks[0].SetActive(true);
                foreach (VideoPlayer video in gestureAttacks[0].GetComponents<VideoPlayer>())
                {
                    video.Play();
                }
                AttackOrElement[1].text = "Low";
                AttackOrElement[2].text = "";
                AttackOrElement[3].text = "";
                break;
            case 1:
                gestureAttacks[0].SetActive(true);
                foreach (VideoPlayer video in gestureAttacks[0].GetComponents<VideoPlayer>())
                {
                    video.Play();
                }
                gestureAttacks[1].SetActive(true);
                foreach (VideoPlayer video in gestureAttacks[1].GetComponents<VideoPlayer>())
                {
                    video.Play();
                }
                AttackOrElement[1].text = "Low";
                AttackOrElement[2].text = "Medium";
                AttackOrElement[3].text = "";
                break;
            case 2:
                gestureAttacks[0].SetActive(true);
                foreach (VideoPlayer video in gestureAttacks[0].GetComponents<VideoPlayer>())
                {
                    video.Play();
                }
                gestureAttacks[1].SetActive(true);
                foreach (VideoPlayer video in gestureAttacks[1].GetComponents<VideoPlayer>())
                {
                    video.Play();
                }
                gestureAttacks[2].SetActive(true);
                foreach (VideoPlayer video in gestureAttacks[2].GetComponents<VideoPlayer>())
                {
                    video.Play();
                }
                AttackOrElement[1].text = "Low";
                AttackOrElement[2].text = "Medium";
                AttackOrElement[3].text = "High";
                break;
        }
        AttackOrElement[4].text = "";
    }
    void displayElements(int[] unlockedElements)
    {
        hideAttackPerform();
        selectElements = true;
        performAttack = false;
        //hideGoBack();

        AttackOrElement[0].text = "Elements:";
        if (unlockedElements[3] >= 0)
        {
            gestureDark.SetActive(true);
            foreach (VideoPlayer video in gestureDark.GetComponents<VideoPlayer>())
            {
                video.Play();
            }
            AttackOrElement[1].text = "Dark";
        }
        else
        {
            AttackOrElement[1].text = "";
        }
        if (unlockedElements[2] >= 0)
        {
            gestureLight.SetActive(true);
            foreach (VideoPlayer video in gestureLight.GetComponents<VideoPlayer>())
            {
                video.Play();
            }
            AttackOrElement[2].text = "Light";
        }
        else
        {
            AttackOrElement[1].text = "";
        }
        if (unlockedElements[0] >= 0)
        {
            gestureFire.SetActive(true);
            foreach (VideoPlayer video in gestureFire.GetComponents<VideoPlayer>())
            {
                video.Play();
            }
            AttackOrElement[3].text = "Fire";
        }
        else
        {
            AttackOrElement[3].text = "";
        }
        if (unlockedElements[1] >= 0)
        {
            gestureEarth.SetActive(true);
            foreach (VideoPlayer video in gestureEarth.GetComponents<VideoPlayer>())
            {
                video.Play();
            }
            AttackOrElement[4].text = "Earth";
        }
        else
        {
            AttackOrElement[4].text = "";
        }
    }

    //FUNCTIONS TO HIDE VIDEOS
    void hideElements()
    {
        StartCoroutine(HideImage(gestureDark));
        StartCoroutine(HideImage(gestureLight));
        StartCoroutine(HideImage(gestureFire));
        StartCoroutine(HideImage(gestureEarth));
    }
    void hideAttacks()
    {
        for (int i = 0; i < gestureAttacks.Length; i++)
        {
            StartCoroutine(HideImage(gestureAttacks[i]));
        }
    }
    void hideAttackPerform()
    {
        StartCoroutine(HideImage(gestureAttackPerform));
    }
    public IEnumerator HideImage(GameObject gesture)
    {
        yield return new WaitForSeconds(0);
        gesture.SetActive(false);
    }

    //FUNCTIONS FOR TUTORIALS
    IEnumerator EnemyImmuneTutorial()
    {
        if (GameManager.game.enemyFightData.tutorialBeginnerEnabled && !tutorialImmunePlayed)
        {
            EnemyAI.AI.deactivateEnemy();
            Debug.Log(EnemyAI.AI.isActive);

            FindObjectOfType<SoundManager>().Play("Alert");
            EnableUITutorial(true);

            yield return new WaitForSeconds(4);

            EnableUITutorial(false);
            EnemyAI.AI.activateEnemy();
            tutorialImmunePlayed = true;
        }
    }
    void EnableUITutorial(bool isEnabled)
    {
        imgTutorial.enabled = isEnabled;
        imgTutorialWarning.enabled = isEnabled;
        for (int i = 0; i < Tutorial.Length; i++)
        {
            Tutorial[i].enabled = isEnabled;
        }
        Tutorial[0].text = "Enemy immune!";
        Tutorial[1].text = "The attack was used too often. The enemy was able to vax himself!";
        Tutorial[2].text = "";
        Tutorial[3].text = "";
        Tutorial[4].text = "Use another element";
    }

    public IEnumerator HideImage(float time, GameObject gesture)
    {
        yield return new WaitForSeconds(2);
        gesture.SetActive(false);
    }

    //FUNCTIONS TO CHECK CERTAIN PARAMS
    bool checkUnlocked(int attackLevel)
    {
        switch (GameManager.game.playerFightData.element)
        {
            case elementType.Dark:
                if (GameManager.game.storyData.spells[3] < attackLevel)
                {
                    txtFeedback.text = "You haven't unlocked this attack yet";
                    return false;
                }
                else { return true; }
            case elementType.Light:
                if (GameManager.game.storyData.spells[2] < attackLevel)
                {
                    txtFeedback.text = "You haven't unlocked this attack yet";
                    Debug.Log("yeah");
                    return false;
                }
                else { return true; }
            case elementType.Fire:
                if (GameManager.game.storyData.spells[0] < attackLevel)
                {
                    txtFeedback.text = "You haven't unlocked this attack yet";
                    return false;
                }
                else { return true; }
            case elementType.Earth:
                if (GameManager.game.storyData.spells[1] < attackLevel)
                {
                    txtFeedback.text = "You haven't unlocked this attack yet";
                    return false;
                }
                else { return true; }
        }
        return false;
    }
    bool checkUnlockedElement(elementType element)
    {
        int[] unlockedAttacks = GameManager.game.storyData.spells;
        switch (element)
        {
            case elementType.Dark:
                if (unlockedAttacks[3] >= 0)
                {
                    return true;
                }
                break;
            case elementType.Light:
                if (unlockedAttacks[2] >= 0)
                {
                    return true;
                }
                break;
            case elementType.Fire:
                if (unlockedAttacks[0] >= 0)
                {
                    return true;
                }
                break;
            case elementType.Earth:
                if (unlockedAttacks[1] >= 0)
                {
                    return true;
                }
                break;
        }
        return false;
    }
    bool checkMana(int manaLevel)
    {
        manaPoints -= manaLevel;
        if (manaPoints <= 0)
        {
            FindObjectOfType<SoundManager>().Play("Alert");
            txtFeedback.text = "You don't have enough mana!";

            manaPoints += manaLevel;

            return false;
        }
        return true;
    }

    //FUNCTIONS TO ALERT FOR CERTAIN PARAMS
    public IEnumerator healthAlert()
    {
        yield return new WaitForSeconds(3);
        FindObjectOfType<SoundManager>().Play("Alert");
        txtFeedback.text = "don't forget about your health!";
    }
    public IEnumerator manaAlert()
    {
        yield return new WaitForSeconds(3);
        FindObjectOfType<SoundManager>().Play("Alert");
        txtFeedback.text = "don't forget about your mana!";
    }

    //FUNCTIONS FOR LIFE PLAYER
    public void Hit(int points)
    {
        healthPoints -= points;
        if (AttackingPlayer.player.health <= 0)
        {
            Die();
        }

    }
    void Die()
    {
        StartCoroutine(DieCoroutine());
    }
    IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(3.5f);
        GameManager.game.LoadFightSceneAgain();
    }

    void enableAttackOrElementUI(bool enable)
    {
        for (int i = 0; i < AttackOrElement.Length; i++)
        {
            AttackOrElement[i].enabled = true;
        }
    }
}

//HEALING E, HIGH R, MEDIUM C, LOW U