using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using Cinemachine;

public class AttackingPlayer : MonoBehaviour {
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

	//public cameraShake camerashake;



	//public elementType element = GameManager.game.playerFightData.element;

	void Awake() {
		player = this;
	}
	void Start() {
		curElement = GameManager.game.playerFightData.element;

		element.text = "Your element: " + GameManager.game.playerFightData.element.ToString();
		GameManager.game.playerFightData.attack = attackType.NONE;
		EnableUITutorial(false);

		if (GameManager.game.enemyFightData.tutorialBeginnerEnabled) {
			enableAttackVids(GameManager.game.playerFightData.element, false);
		} else {
			displayElements(GameManager.game.playerFightData.unlockedAttacks);
		}

		txtFeedback.text = "Start the fight";
	}

	void Update() {
		//LOW DAMAGE ATTACK U+N+R+C
		int i = 0;
		if (input.IsSpellGesturePerformed(GestureType.LOW)) {
			i++;
			Debug.Log("low: " + i);
			if (checkUnlocked(0)) {
				GameManager.game.playerFightData.attack = attackType.LOW;
				txtFeedback.text = "Switched to low damage attack";
				displayAttackPerformVids();
			}

			//MEDIUM DAMAGE ATTACK C
		} else if (input.IsSpellGesturePerformed(GestureType.MEDIUM)) {
			Debug.Log("Medium");
			if (checkUnlocked(1)) {
				GameManager.game.playerFightData.attack = attackType.MEDIUM;
				txtFeedback.text = "Switched to medium damage attack";
				displayAttackPerformVids();
			}
		//HIGH DAMAGE ATTACK Q+R+C+P+U+N
		} else if (input.IsSpellGesturePerformed(GestureType.HIGH)) { //Finger.POINT_LEFT, Finger.POINT_RIGHT, Finger.PINK_LEFT, Finger.PINK_RIGHT, Finger.THUMB_LEFT, Finger.THUMB_RIGHT
			Debug.Log("High");
			if (checkUnlocked(2)) {
				GameManager.game.playerFightData.attack = attackType.HIGH;
				txtFeedback.text = "Switched to High damage attack";
				displayAttackPerformVids();
			}

			//HEALING
		} else if (input.IsSpellGesturePerformed(GestureType.DARK)) {
			if (healthPoints < 100) {
				manaPoints -= manaLow;
				if (manaPoints <= 0) {
					noMoreMana = true;
					manaPoints += manaLow;
				}
				if (!noMoreMana) {
					heal.Play();
					if (healthPoints + healing > 100) {
						healthPoints = 100;
					} else {
						healthPoints += healing;
					}
				}
			}

		//SWITCH ELEMENT N
		} else if (input.IsSpellGesturePerformed(GestureType.DARK)) {
			if (checkUnlockedElement(elementType.Dark))
			{
				Debug.Log("Dark");
				GameManager.game.playerFightData.element = elementType.Dark;
				GameManager.game.playerFightData.attack = attackType.NONE;
				element.text = "Your element: Dark";
				txtFeedback.text = "Switched to Dark element";
				enableAttackVids(elementType.Dark, false);
				curElement = elementType.Dark;

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
		} else if (input.IsSpellGesturePerformed(GestureType.LIGHT)) { //Q+C+N+P
			Debug.Log("Light");
			if (checkUnlockedElement(elementType.Light))
			{
				GameManager.game.playerFightData.element = elementType.Light;
				GameManager.game.playerFightData.attack = attackType.NONE;
				element.text = "Your element: Light";
				txtFeedback.text = "Switched to Light element";
				enableAttackVids(elementType.Light, false);
				curElement = elementType.Light;

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
		} else if (input.IsSpellGesturePerformed(GestureType.FIRE)) {
			Debug.Log("Fire");
			if (checkUnlockedElement(elementType.Fire)) {
				GameManager.game.playerFightData.element = elementType.Fire;
				GameManager.game.playerFightData.attack = attackType.NONE;
				element.text = "Your element: Fire";
				txtFeedback.text = "Switched to Fire element";
				enableAttackVids(elementType.Fire, false);
				curElement = elementType.Fire;

				x = y = z = 0;

				gestureFire.SetActive(true);
				float time = 0;
				foreach (VideoPlayer video in gestureFire.GetComponents<VideoPlayer>()) {
					video.Play();
					time = (float)video.length;
				}

				StartCoroutine(HideImage(time, gestureFire));
			}
		} else if (input.IsSpellGesturePerformed(GestureType.EARTH)) {
			Debug.Log("Earth");
			if (checkUnlockedElement(elementType.Fire))
			{
				GameManager.game.playerFightData.element = elementType.Earth;
				GameManager.game.playerFightData.attack = attackType.NONE;
				element.text = "Your element: Earth";
				txtFeedback.text = "Switched to Earth element";
				enableAttackVids(elementType.Earth, false);
				curElement = elementType.Earth;

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
		//DEBUG: Q
		if (input.IsCombinationPressedDown(Finger.PINK_LEFT)/*input.IsForwardGesturePerformed()*/) {
			switch (GameManager.game.playerFightData.attack) {
				case attackType.HIGH:
					z++;
					x = y = 0;

					if (GameManager.game.playerFightData.element == elementType.Fire && checkMana(manaHigh)) {
						if (z > 3) {
							fireImmune = true;
							StartCoroutine(EnemyImmuneTutorial());
						}

						fireAttackHigh.Play();
						FindObjectOfType<SoundManager>().Play("PlayerAttackFireHigh");
						FightManager.fight.HitEnemy(damageHigh, z, fireImmune);
					} else if (GameManager.game.playerFightData.element == elementType.Earth && checkMana(manaHigh)) {
						if (z > 3) {
							earthImmune = true;
							StartCoroutine(EnemyImmuneTutorial());
						}

						earthAttackHigh.Play();
						FindObjectOfType<SoundManager>().Play("PlayerAttackEarth");
						FightManager.fight.HitEnemy(damageHigh, z, earthImmune);
					} else if (GameManager.game.playerFightData.element == elementType.Dark && checkMana(manaHigh)) {
						if (z > 3) {
							darkImmune = true;
							StartCoroutine(EnemyImmuneTutorial());
						}

						darkAttackHigh.Play();
						FindObjectOfType<SoundManager>().Play("PlayerAttackDarkHigh");
						FightManager.fight.HitEnemy(damageHigh, z, darkImmune);
					}
					if (GameManager.game.playerFightData.element == elementType.Light && checkMana(manaHigh)) {
						if (z > 3) {
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

					if (GameManager.game.playerFightData.element == elementType.Fire && checkMana(manaMedium)) {
						if (y > 3) {
							fireImmune = true;
							StartCoroutine(EnemyImmuneTutorial());
						}

						fireAttackMedium.Play();
						FindObjectOfType<SoundManager>().Play("PlayerAttackFireMedium");
						FightManager.fight.HitEnemy(damageMedium, y, fireImmune);
					} else if (GameManager.game.playerFightData.element == elementType.Earth && checkMana(manaMedium)) {
						if (y > 3) {
							earthImmune = true;
							StartCoroutine(EnemyImmuneTutorial());
						}

						earthAttackMedium.Play();
						FindObjectOfType<SoundManager>().Play("PlayerAttackEarth");
						FightManager.fight.HitEnemy(damageMedium, y, earthImmune);
					} else if (GameManager.game.playerFightData.element == elementType.Dark && checkMana(manaMedium)) {
						if (y > 3) {
							darkImmune = true;
							StartCoroutine(EnemyImmuneTutorial());
						}

						darkAttackMedium.Play();
						FindObjectOfType<SoundManager>().Play("PlayerAttackDarkMedium");
						FightManager.fight.HitEnemy(damageMedium, y, darkImmune);
					}
					if (GameManager.game.playerFightData.element == elementType.Light && checkMana(manaMedium)) {
						if (y > 3) {
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

					if (GameManager.game.playerFightData.element == elementType.Fire && checkMana(manaLow)) {
						if (x > 3) {
							fireImmune = true;
							StartCoroutine(EnemyImmuneTutorial());
						}

						fireAttackLow.Play();
						FindObjectOfType<SoundManager>().Play("PlayerAttackFireLow");
						FightManager.fight.HitEnemy(damageLow, x, fireImmune);
					} else if (GameManager.game.playerFightData.element == elementType.Earth && checkMana(manaLow)) {
						if (x > 3) {
							earthImmune = true;
							StartCoroutine(EnemyImmuneTutorial());
						}

						earthAttackLow.Play();
						FindObjectOfType<SoundManager>().Play("PlayerAttackEarth");
						FightManager.fight.HitEnemy(damageLow, x, earthImmune);
					} else if (GameManager.game.playerFightData.element == elementType.Dark && checkMana(manaLow)) {
						if (x > 3) {
							darkImmune = true;
							StartCoroutine(EnemyImmuneTutorial());
						}

						darkAttackLow.Play();
						FindObjectOfType<SoundManager>().Play("PlayerAttackDarkLow");
						FightManager.fight.HitEnemy(damageLow, x, darkImmune);
					}
					if (GameManager.game.playerFightData.element == elementType.Light && checkMana(manaLow)) {
						if (x > 3) {
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
					FindObjectOfType<SoundManager>().Play("Alert");
					break;
			}
		}
		//P
		if (input.IsCombinationPressedDown(Finger.PINK_RIGHT)/*input.IsForwardGesturePerformed()*/) {
			bool isAttackActive = false;
			bool isAttackPerformActive = false;

			for (int j = 0; j < gestureAttacks.Length; j++) {
				Debug.Log(gestureAttacks[i].activeSelf);
				if (gestureAttacks[i].activeSelf) {
					isAttackActive = true;
				}
			}
			if (gestureAttackPerform.activeSelf) {
				isAttackPerformActive = true;
			}


			if (isAttackActive) {
				displayElements(GameManager.game.playerFightData.unlockedAttacks);
			}
			if (isAttackPerformActive)
            {
				Debug.Log("treu");
				enableAttackVids(curElement, true);
			}
			//hideActivateAttack();
		}

		if (mana <= 100 && !alertM) {
			alertM = true;
			StartCoroutine(manaAlert());
		}

		if (healthPoints <= 25 && !alertHP) {
			alertHP = true;
			StartCoroutine(healthAlert());
		}
	}

	void displayAttackPerformVids() {
		hideAttacks();

		gestureAttackPerform.SetActive(true);
		foreach (VideoPlayer video in gestureAttackPerform.GetComponents<VideoPlayer>()) {
			video.Play();
		}

		AttackOrElement[0].text = "Perform attack!";
		for (int i = 1; i < AttackOrElement.Length; i++) {
			AttackOrElement[i].text = "";
		}
	}

	void enableAttackVids(elementType element, bool fromAttackPerform) {
		//hideAttacks();
		if (fromAttackPerform) {
			hideAttackPerform();
		} else {
			hideElements();
		}

		switch (element) {
			case elementType.Dark:
				displayAttacks(0);
				break;
			case elementType.Light:
				displayAttacks(1);
				break;
			case elementType.Fire:
				displayAttacks(2);
				break;
			case elementType.Earth:
				displayAttacks(3);
				break;
		}

	}
	void displayAttacks(int element) {
		AttackOrElement[0].text = "Attacks:";
		enableAttackOrElementUI(true);

		int level = GameManager.game.playerFightData.unlockedAttacks[element];
		switch (level) {
			case 0:
				AttackOrElement[1].text = "";
				AttackOrElement[2].text = "";
				AttackOrElement[3].text = "";
				break;
			case 1:
				gestureAttacks[0].SetActive(true);
				foreach (VideoPlayer video in gestureAttacks[0].GetComponents<VideoPlayer>()) {
					video.Play();
				}
				AttackOrElement[1].text = "Low";
				AttackOrElement[2].text = "";
				AttackOrElement[3].text = "";
				break;
			case 2:
				gestureAttacks[0].SetActive(true);
				foreach (VideoPlayer video in gestureAttacks[0].GetComponents<VideoPlayer>()) {
					video.Play();
				}
				gestureAttacks[1].SetActive(true);
				foreach (VideoPlayer video in gestureAttacks[1].GetComponents<VideoPlayer>()) {
					video.Play();
				}
				AttackOrElement[1].text = "Low";
				AttackOrElement[2].text = "Medium";
				AttackOrElement[3].text = "";
				break;
			case 3:
				gestureAttacks[0].SetActive(true);
				foreach (VideoPlayer video in gestureAttacks[0].GetComponents<VideoPlayer>()) {
					video.Play();
				}
				gestureAttacks[1].SetActive(true);
				foreach (VideoPlayer video in gestureAttacks[1].GetComponents<VideoPlayer>()) {
					video.Play();
				}
				gestureAttacks[2].SetActive(true);
				foreach (VideoPlayer video in gestureAttacks[2].GetComponents<VideoPlayer>()) {
					video.Play();
				}
				AttackOrElement[1].text = "Low";
				AttackOrElement[2].text = "Medium";
				AttackOrElement[3].text = "High";
				break;
		}
		AttackOrElement[4].text = "";
	}

	void displayElements(int[] unlockedElements) {
		hideAttacks();

		AttackOrElement[0].text = "Elements:";
		if (unlockedElements[0] > 0) {
			gestureDark.SetActive(true);
			foreach (VideoPlayer video in gestureDark.GetComponents<VideoPlayer>()) {
				video.Play();
			}
			AttackOrElement[1].text = "Dark";
		} else {
			AttackOrElement[1].text = "";
		}
		if (unlockedElements[1] > 0) {
			gestureLight.SetActive(true);
			foreach (VideoPlayer video in gestureLight.GetComponents<VideoPlayer>()) {
				video.Play();
			}
			AttackOrElement[2].text = "Light";
		} else {
			AttackOrElement[1].text = "";
		}
		if (unlockedElements[2] > 0) {
			gestureFire.SetActive(true);
			foreach (VideoPlayer video in gestureFire.GetComponents<VideoPlayer>()) {
				video.Play();
			}
			AttackOrElement[3].text = "Fire";
		} else {
			AttackOrElement[3].text = "";
		}
		if (unlockedElements[3] > 0) {
			gestureEarth.SetActive(true);
			foreach (VideoPlayer video in gestureEarth.GetComponents<VideoPlayer>()) {
				video.Play();
			}
			AttackOrElement[4].text = "Earth";
		} else {
			AttackOrElement[4].text = "";
		}
	}
	/*
	void hideElements()
	{
		HideImage(gestureDark);
		HideImage(gestureLight);
		HideImage(gestureFire);
		HideImage(gestureEarth);
	}
	void hideAttacks()
	{
		for (int i = 0; i < gestureAttacks.Length; i++)
		{
			HideImage(gestureAttacks[i]);
		}
	}

	public void HideImage(GameObject gesture)
	{
		//yield return new WaitForSeconds(0);
		gesture.SetActive(false);
	}
	*/
	void hideElements() {
		StartCoroutine(HideImage(gestureDark));
		StartCoroutine(HideImage(gestureLight));
		StartCoroutine(HideImage(gestureFire));
		StartCoroutine(HideImage(gestureEarth));
	}
	void hideAttacks() {
		for (int i = 0; i < gestureAttacks.Length; i++) {
			StartCoroutine(HideImage(gestureAttacks[i]));
		}
	}
	void hideAttackPerform() {
		StartCoroutine(HideImage(gestureAttackPerform));
	}

	public IEnumerator HideImage(GameObject gesture) {
		yield return new WaitForSeconds(0);
		gesture.SetActive(false);
	}

	IEnumerator EnemyImmuneTutorial() {
		if (GameManager.game.enemyFightData.tutorialBeginnerEnabled && !tutorialImmunePlayed) {
			EnemyAI.AI.deactivateEnemy();
			Debug.Log(EnemyAI.AI.isActive);

			FindObjectOfType<SoundManager>().Play("Alert");
			EnableUITutorial(true);

			yield return new WaitForSeconds(7);

			EnableUITutorial(false);
			EnemyAI.AI.activateEnemy();
			bool tutorialImmunePlayed = true;
		}
	}
	void EnableUITutorial(bool isEnabled) {
		imgTutorial.enabled = isEnabled;
		imgTutorialWarning.enabled = isEnabled;
		for (int i = 0; i < Tutorial.Length; i++) {
			Tutorial[i].enabled = isEnabled;
		}
		Tutorial[0].text = "Enemy immune!";
		Tutorial[1].text = "The attack was used too often. The enemy was able to vax himself!";
		Tutorial[2].text = "";
		Tutorial[3].text = "";
		Tutorial[4].text = "Use another element";
	}

	public IEnumerator healthAlert() {
		yield return new WaitForSeconds(3);
		FindObjectOfType<SoundManager>().Play("Alert");
		txtFeedback.text = "don't forget about your health!";
	}
	public IEnumerator manaAlert() {
		yield return new WaitForSeconds(3);
		FindObjectOfType<SoundManager>().Play("Alert");
		txtFeedback.text = "don't forget about your mana!";
	}

	public IEnumerator HideImage(float time, GameObject gesture) {
		yield return new WaitForSeconds(2);
		gesture.SetActive(false);
	}

	public void Hit(int points) {
		healthPoints -= points;
		if (AttackingPlayer.player.health <= 0) {
			Die();
		}

	}

	void Die() {
		StartCoroutine(DieCoroutine());
	}

	IEnumerator DieCoroutine() {
		yield return new WaitForSeconds(3.5f);
		GameManager.game.LoadFightSceneAgain();
	}

	bool checkUnlocked(int attackLevel) {
		switch (GameManager.game.playerFightData.element) {
			case elementType.Dark:
				if (GameManager.game.playerFightData.unlockedAttacks[0] <= attackLevel) {
					txtFeedback.text = "You haven't unlocked this attack yet";
					return false;
				} else { return true; }
			case elementType.Light:
				Debug.Log(GameManager.game.playerFightData.unlockedAttacks[1]); //2
				Debug.Log(attackLevel); //0
				if (GameManager.game.playerFightData.unlockedAttacks[1] <= attackLevel) {
					txtFeedback.text = "You haven't unlocked this attack yet";
					Debug.Log("yeah");
					return false;
				} else { return true; }
			case elementType.Fire:
				if (GameManager.game.playerFightData.unlockedAttacks[2] <= attackLevel) {
					txtFeedback.text = "You haven't unlocked this attack yet";
					return false;
				} else { return true; }
			case elementType.Earth:
				if (GameManager.game.playerFightData.unlockedAttacks[3] <= attackLevel) {
					txtFeedback.text = "You haven't unlocked this attack yet";
					return false;
				} else { return true; }
		}
		return false;
	}
	bool checkUnlockedElement(elementType element) {
		int[] unlockedAttacks = GameManager.game.playerFightData.unlockedAttacks;
		switch (element) {
			case elementType.Dark:
				if (unlockedAttacks[0] > 0) {
					return true;
				}
				break;
			case elementType.Light:
				if (unlockedAttacks[1] > 0) {
					return true;
				}
				break;
			case elementType.Fire:
				if (unlockedAttacks[2] > 0) {
					return true;
				}
				break;
			case elementType.Earth:
				if (unlockedAttacks[3] > 0) {
					return true;
				}
				break;
		}
		return false;
	}

	bool checkMana(int manaLevel) {
		manaPoints -= manaLevel;
		if (manaPoints <= 0) {
			FindObjectOfType<SoundManager>().Play("Alert");
			txtFeedback.text = "You don't have enough mana!";

			manaPoints += manaLevel;

			return false;
		}
		return true;
	}

	void enableAttackOrElementUI(bool enable) {
		for (int i = 0; i < AttackOrElement.Length; i++) {
			AttackOrElement[i].enabled = true;
		}
	}
}

//HEALING E, HIGH R, MEDIUM C, LOW U