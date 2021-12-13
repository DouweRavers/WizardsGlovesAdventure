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
	bool fireImmune, lightImmune, darkImmune, earthImmune;

	public float healthPoints = 100;
	public float healing = 10;

	public int manaPoints = 2000;
	public int manaLow = 50;
	public int manaMedium = 100;
	public int manaHigh = 150;
	bool noMoreMana = false;

	public ParticleSystem fireAttackLow, fireAttackMedium, fireAttackHigh, lightAttackLow, lightAttackMedium, lightAttackHigh, darkAttackLow, darkAttackMedium, darkAttackHigh, earthAttackLow, earthAttackMedium, earthAttackHigh;
	public ParticleSystem heal;
	public Transform enemy;
	public GameObject gestureLow, gestureMedium, gestureHigh;
	public GameObject gestureFire, gestureDark, gestureLight, gestureEarth;

	public Text element;
	public Text txtFeedback;
	public Image imgFeedback;

	public GameObject introManager;

	public CinemachineVirtualCamera[] vCameras;

	//public cameraShake camerashake;



	//public elementType element = GameManager.game.playerFightData.element;

	void Awake() {
		player = this;

		element.text = "Your element: " + GameManager.game.playerFightData.element.ToString();
		GameManager.game.playerFightData.attack = attackType.NONE;

		txtFeedback.text = "Start the fight";
	}

	void Update() {
		//LOW DAMAGE ATTACK U+N+R+C
		int i = 0;
		if (input.IsSpellGesturePerformed(GestureType.LOW)) {
			i++;
			Debug.Log("low: " + i);
			if (checkUnlocked(1)) 
            {
				GameManager.game.playerFightData.attack = attackType.LOW;
				txtFeedback.text = "Switched to low damage attack";

				gestureLow.SetActive(true);
				float time = 0;
				foreach (VideoPlayer video in gestureLow.GetComponents<VideoPlayer>())
				{
					video.Play();
					time = (float)video.length;
				}
				StartCoroutine(HideImage(time, gestureLow));
			}

			//Type
			/*
			if (GameManager.game.playerFightData.element == elementType.Fire && !noMoreMana) {
				if (EnemyAI.AI.boost == false) {
					x++;
					if (x > 2) {
						fireImmune = true;
					}
				}
				y = z = 0;

				fireAttackLow.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackFireLow");
				FightManager.fight.HitEnemy(damageLow, x, fireImmune);

			} else if (GameManager.game.playerFightData.element == elementType.Light && !noMoreMana) {
				Debug.Log("LightLow");

				if (EnemyAI.AI.boost == false) {
					x++;
					if (x > 2) {
						lightImmune = true;
					}
				}
				y = z = 0;

				lightAttackLow.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackLightLow");
				FightManager.fight.HitEnemy(damageLow, x, lightImmune);
			} else if (GameManager.game.playerFightData.element == elementType.Dark && !noMoreMana) {
				Debug.Log("DarkLow");

				if (EnemyAI.AI.boost == false) {
					x++;
					if (x > 2) {
						darkImmune = true;
					}
				}
				y = z = 0;

				darkAttackLow.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackDarkLow");
				FightManager.fight.HitEnemy(damageLow, x, darkImmune);
			} else if (GameManager.game.playerFightData.element == elementType.Earth && !noMoreMana) {
				Debug.Log("EarthLow");

				if (EnemyAI.AI.boost == false) {
					x++;
					if (x > 2) {
						earthImmune = true;
					}
				}
				y = z = 0;

				earthAttackLow.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackEarth");
				FightManager.fight.HitEnemy(damageLow, x, earthImmune);
			}
			txtFeedback.text = "Used low damage attack";

			gestureLow.SetActive(true);

			float time = 0;
			foreach (VideoPlayer video in gestureLow.GetComponents<VideoPlayer>()) {
				video.Play();
				time = (float)video.length;
			}

			StartCoroutine(HideImage(time, gestureLow));
			*/
		//MEDIUM DAMAGE ATTACK W+E+C+O+I+N
		} else if (input.IsSpellGesturePerformed(GestureType.MEDIUM)) {
			Debug.Log("Medium");
			if (checkUnlocked(2))
            {
				GameManager.game.playerFightData.attack = attackType.MEDIUM;
				txtFeedback.text = "Switched to medium damage attack";

				gestureMedium.SetActive(true);
				float time = 0;
				foreach (VideoPlayer video in gestureMedium.GetComponents<VideoPlayer>())
				{
					video.Play();
					time = (float)video.length;
				}
				StartCoroutine(HideImage(time, gestureMedium));
			}

			/*
			//Element
			if (GameManager.game.playerFightData.element == elementType.Fire && !noMoreMana) {
				Debug.Log("FireMedium");

				if (EnemyAI.AI.boost == false) {
					y++;
					if (y > 2) {
						fireImmune = true;
					}
				}
				x = z = 0;

				fireAttackMedium.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackFireMedium");
				FightManager.fight.HitEnemy(damageMedium, y, fireImmune);
			} else if (GameManager.game.playerFightData.element == elementType.Light && !noMoreMana) {
				Debug.Log("LightMedium");

				if (EnemyAI.AI.boost == false) {
					y++;
					if (y > 2) {
						lightImmune = true;
					}
				}
				x = z = 0;

				lightAttackMedium.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackLightMedium");
				FightManager.fight.HitEnemy(damageMedium, y, lightImmune);
			} else if (GameManager.game.playerFightData.element == elementType.Dark && !noMoreMana) {
				Debug.Log("DarkMedium");

				if (EnemyAI.AI.boost == false) {
					y++;
					if (y > 2) {
						darkImmune = true;
					}
				}
				x = z = 0;

				darkAttackMedium.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackDarkMedium");
				FightManager.fight.HitEnemy(damageMedium, y, darkImmune);
			} else if (GameManager.game.playerFightData.element == elementType.Earth && !noMoreMana) {
				Debug.Log("EarthMedium");

				if (EnemyAI.AI.boost == false) {
					y++;
					if (y > 2) {
						earthImmune = true;
					}
				}
				x = z = 0;

				earthAttackMedium.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackEarth");
				FightManager.fight.HitEnemy(damageMedium, y, earthImmune);
			}
			txtFeedback.text = "Used medium damage attack";

			gestureMedium.SetActive(true);

			float time = 0;
			foreach (VideoPlayer video in gestureMedium.GetComponents<VideoPlayer>()) {
				video.Play();
				time = (float)video.length;
			}

			StartCoroutine(HideImage(time, gestureMedium));
			*/
		//HIGH DAMAGE ATTACK
		} else if (input.IsSpellGesturePerformed(GestureType.HIGH)) { //Finger.POINT_LEFT, Finger.POINT_RIGHT, Finger.PINK_LEFT, Finger.PINK_RIGHT, Finger.THUMB_LEFT, Finger.THUMB_RIGHT
			Debug.Log("High");
			if (checkUnlocked(3))
            {
				GameManager.game.playerFightData.attack = attackType.HIGH;
				txtFeedback.text = "Switched to High damage attack";

				gestureHigh.SetActive(true);
				float time = 0;
				foreach (VideoPlayer video in gestureHigh.GetComponents<VideoPlayer>())
				{
					video.Play();
					time = (float)video.length;
				}
				StartCoroutine(HideImage(time, gestureHigh));
			}

			/*
			//Element
			if (GameManager.game.playerFightData.element == elementType.Fire && !noMoreMana) {
				Debug.Log("FireHigh");

				if (EnemyAI.AI.boost == false) {
					z++;
					if (z > 2) {
						fireImmune = true;
					}
				}
				x = y = 0;

				fireAttackHigh.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackFireHigh");
				FightManager.fight.HitEnemy(damageHigh, z, fireImmune);
			} else if (GameManager.game.playerFightData.element == elementType.Light && !noMoreMana) {
				Debug.Log("LightHigh");

				if (EnemyAI.AI.boost == false) {
					z++;
					if (z > 2) {
						lightImmune = true;
					}
				}
				x = y = 0;

				lightAttackHigh.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackLightHigh");
				FightManager.fight.HitEnemy(damageHigh, z, lightImmune);
			} else if (GameManager.game.playerFightData.element == elementType.Dark && !noMoreMana) {
				Debug.Log("DarkHigh");

				if (EnemyAI.AI.boost == false) {
					z++;
					if (z > 2) {
						darkImmune = true;
					}
				}
				x = y = 0;

				darkAttackHigh.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackDarkHigh");
				FightManager.fight.HitEnemy(damageHigh, z, darkImmune);
			} else if (GameManager.game.playerFightData.element == elementType.Earth && !noMoreMana) {
				Debug.Log("EarthHigh");

				if (EnemyAI.AI.boost == false) {
					z++;
					if (z > 2) {
						earthImmune = true;
					}
				}
				x = y = 0;


				//StartCoroutine(camerashake.Shake(.15f, .4f));
				earthAttackHigh.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackEarth");
				FightManager.fight.HitEnemy(damageHigh, z, earthImmune);
			}
			txtFeedback.text = "Used high damage attack";

			gestureMedium.SetActive(true);

			float time = 0;
			foreach (VideoPlayer video in gestureMedium.GetComponents<VideoPlayer>()) {
				video.Play();
				time = (float)video.length;
			}

			StartCoroutine(HideImage(time, gestureMedium));
			*/
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

		//SWITCH ELEMENT
		} else if (input.IsSpellGesturePerformed(GestureType.DARK)) { //
			Debug.Log("Dark");
			GameManager.game.playerFightData.element = elementType.Dark;
			GameManager.game.playerFightData.attack = attackType.NONE;
			element.text = "Your element: Dark";
			txtFeedback.text = "Switched to Dark element";

			x = y = z = 0;

			gestureDark.SetActive(true);
			float time = 0;
			foreach (VideoPlayer video in gestureDark.GetComponents<VideoPlayer>()) {
				video.Play();
				time = (float)video.length;
			}

			StartCoroutine(HideImage(time, gestureDark));
		} else if (input.IsSpellGesturePerformed(GestureType.LIGHT)) { //Q+C+N+P
			Debug.Log("Light");
			GameManager.game.playerFightData.element = elementType.Light;
			GameManager.game.playerFightData.attack = attackType.NONE;
			element.text = "Your element: Light";
			txtFeedback.text = "Switched to Light element";

			x = y = z = 0;

			gestureLight.SetActive(true);
			float time = 0;
			foreach (VideoPlayer video in gestureLight.GetComponents<VideoPlayer>()) {
				video.Play();
				time = (float)video.length;
			}

			StartCoroutine(HideImage(time, gestureLight));
		} else if (input.IsSpellGesturePerformed(GestureType.FIRE)) {
			Debug.Log("fire");
			GameManager.game.playerFightData.element = elementType.Fire;
			GameManager.game.playerFightData.attack = attackType.NONE;
			element.text = "Your element: Fire";
			txtFeedback.text = "Switched to Fire element";

			x = y = z = 0;

			gestureFire.SetActive(true);
			float time = 0;
			foreach (VideoPlayer video in gestureFire.GetComponents<VideoPlayer>()) {
				video.Play();
				time = (float)video.length;
			}

			StartCoroutine(HideImage(time, gestureFire));
		} else if (input.IsSpellGesturePerformed(GestureType.EARTH)) {
			Debug.Log("Earth");
			GameManager.game.playerFightData.element = elementType.Earth;
			GameManager.game.playerFightData.attack = attackType.NONE;
			element.text = "Your element: Earth";
			txtFeedback.text = "Switched to Earth element";

			x = y = z = 0;

			gestureEarth.SetActive(true);
			float time = 0;
			foreach (VideoPlayer video in gestureEarth.GetComponents<VideoPlayer>()) {
				video.Play();
				time = (float)video.length;
			}

			StartCoroutine(HideImage(time, gestureEarth));
		}
		//DEBUG: Q
		if (input.IsCombinationPressedDown(Finger.PINK_LEFT)/*input.IsForwardGesturePerformed()*/)
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
						}

						fireAttackHigh.Play();
						FindObjectOfType<SoundManager>().Play("PlayerAttackFireHigh");
						FightManager.fight.HitEnemy(damageHigh, z, fireImmune);
					} else if (GameManager.game.playerFightData.element == elementType.Earth && checkMana(manaHigh))
					{
						if (z > 3)
						{
							earthImmune = true;
						}

						earthAttackHigh.Play();
						FindObjectOfType<SoundManager>().Play("PlayerAttackEarth");
						FightManager.fight.HitEnemy(damageHigh, z, earthImmune);
					} else if (GameManager.game.playerFightData.element == elementType.Dark && checkMana(manaHigh))
                    {
						if (z > 3)
						{
							darkImmune = true;
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

		if (mana <= 100) {
			StartCoroutine(manaAlert());
		}

		if (healthPoints <= 25)
        {
			StartCoroutine(healthAlert());
		}
	}

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

	bool checkUnlocked(int attackLevel)
    {
		switch (GameManager.game.playerFightData.element)
		{
			case elementType.Dark:
				if (GameManager.game.GetSpell(3) != attackLevel)
				{
					txtFeedback.text = "You haven't unlocked this attack yet";
					return false;
				}
				else { return true; }
			case elementType.Light:
				if (GameManager.game.GetSpell(2) != attackLevel)
				{
					txtFeedback.text = "You haven't unlocked this attack yet";
					return false;
				}
				else { return true; }
			case elementType.Fire:
				if (GameManager.game.GetSpell(0) != attackLevel)
				{
					txtFeedback.text = "You haven't unlocked this attack yet";
					return false;
				}
				else { return true; }
			case elementType.Earth:
				if (GameManager.game.GetSpell(1) != attackLevel)
				{
					txtFeedback.text = "You haven't unlocked this attack yet";
					return false;
				}
				else { return true; }
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
}

//HEALING E, HIGH R, MEDIUM C, LOW U