using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class AttackingPlayer : MonoBehaviour {
	public static AttackingPlayer player;
	public InputManager input;

	public float health { get { return healthPoints; } }
	public int mana { get { return manaPoints; } }

	int x, y, z = 0;
	public float damageLow = 15;
	public float damageMedium = 25;
	public float damageHigh = 30;
	//int[] spellsImmune = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
	bool fireAttackLowImmune, fireAttackMediumImmune, fireAttackHighImmune, lightAttackLowImmune, lightAttackMediumImmune, lightAttackHighImmune, darkAttackLowImmune, darkAttackMediumImmune, darkAttackHighImmune, earthAttackLowImmune, earthAttackMediumImmune, earthAttackHighImmune;
	bool fireImmune, lightImmune, darkImmune, earthImmune;

	public float healthPoints = 100;
	public float healing = 10;

	public int manaPoints = 2000;
	public int manaLow = 50;
	public int manaMedium = 100;
	public int manaHigh = 150;

	public ParticleSystem fireAttackLow, fireAttackMedium, fireAttackHigh, lightAttackLow, lightAttackMedium, lightAttackHigh, darkAttackLow, darkAttackMedium, darkAttackHigh, earthAttackLow, earthAttackMedium, earthAttackHigh;
	public ParticleSystem heal;
	public Transform enemy;
	public GameObject gestureLow, gestureMedium, gestureHigh;
	public GameObject gestureFire, gestureDark, gestureLight, gestureEarth;
	bool toggler = true;

	bool noMoreMana = false;

	public Text element;
	public Text txtFeedback;
	public Image imgFeedback;

	//public cameraShake camerashake;



	//public elementType element = GameManager.game.playerFightData.element;

	void Awake() {
		player = this;

		element.text = "Your element: " + GameManager.game.playerFightData.element.ToString();
		txtFeedback.text = "Start the fight";
	}

	void Update() {
		//LOW (u)
		if (input.IsCombinationPressed(Finger.POINT_RIGHT)) { //Finger.THUMB_LEFT, Finger.THUMB_RIGHT, Finger.POINT_LEFT, Finger.POINT_RIGHT
			if (toggler) return;
			else toggler = true;

			//update mana
			manaPoints -= manaLow;
			if (manaPoints <= 0) {
				noMoreMana = true;

				FindObjectOfType<SoundManager>().Play("Alert");
				txtFeedback.text = "You don't have enough mana!";

				manaPoints += manaLow;
			}

			//attack
			if (GameManager.game.playerFightData.element == elementType.Fire && !noMoreMana) {
				Debug.Log("FireLow");

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

			//MEDIUM (c)
		} else if (input.IsCombinationPressed(Finger.THUMB_LEFT)) { //Finger.THUMB_LEFT, Finger.THUMB_RIGHT, Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT, Finger.RING_LEFT, Finger.RING_RIGHT
			if (toggler) return;
			else toggler = true;

			//update mana
			manaPoints -= manaMedium;
			if (manaPoints <= 0) {
				noMoreMana = true;

				FindObjectOfType<SoundManager>().Play("Alert");
				txtFeedback.text = "You don't have enough mana!";

				manaPoints += manaMedium;
			}
			//attack
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

				//camerashake
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

			//HIGH (r)
		} else if (input.IsCombinationPressed(Finger.POINT_LEFT)) { //Finger.POINT_LEFT, Finger.POINT_RIGHT, Finger.PINK_LEFT, Finger.PINK_RIGHT, Finger.THUMB_LEFT, Finger.THUMB_RIGHT
			if (toggler) return;
			else toggler = true;

			//update mana
			manaPoints -= manaHigh;
			if (manaPoints <= 0) {
				noMoreMana = true;

				FindObjectOfType<SoundManager>().Play("Alert");
				txtFeedback.text = "You don't have enough mana!";

				manaPoints += manaHigh;
			}

			//attack
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

			//HEALING (e)
		} else if (input.IsCombinationPressed(Finger.MIDDLE_LEFT)) {
			if (toggler) return;
			else toggler = true;

			Debug.Log("healing");
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
			//change element
		} else if (input.IsCombinationPressed(Finger.PINK_LEFT)) {
			//Finger.PINK_LEFT, Finger.PINK_RIGHT, Finger.RING_LEFT, Finger.RING_RIGHT, Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT, Finger.POINT_LEFT, Finger.POINT_RIGHT
			GameManager.game.playerFightData.element = elementType.Dark;
			element.text = "Your element: Dark";
			txtFeedback.text = "changed your element";

			gestureDark.SetActive(true);

			float time = 0;
			foreach (VideoPlayer video in gestureDark.GetComponents<VideoPlayer>()) {
				video.Play();
				time = (float)video.length;
			}

			StartCoroutine(HideImage(time, gestureDark));
		} else if (input.IsCombinationPressed(Finger.PINK_RIGHT)) {
			//Finger.PINK_LEFT, Finger.PINK_RIGHT, Finger.RING_LEFT, Finger.RING_RIGHT, Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT, Finger.POINT_LEFT, Finger.POINT_RIGHT
			GameManager.game.playerFightData.element = elementType.Light;
			element.text = "Your element: Light";
			txtFeedback.text = "changed your element";

			gestureLight.SetActive(true);

			float time = 0;
			foreach (VideoPlayer video in gestureLight.GetComponents<VideoPlayer>()) {
				video.Play();
				time = (float)video.length;
			}

			StartCoroutine(HideImage(time, gestureLight));
		} else if (input.IsCombinationPressed(Finger.POINT_LEFT)) {
			//Finger.PINK_LEFT, Finger.PINK_RIGHT, Finger.RING_LEFT, Finger.RING_RIGHT, Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT, Finger.POINT_LEFT, Finger.POINT_RIGHT
			GameManager.game.playerFightData.element = elementType.Fire;
			element.text = "Your element: Fire";
			txtFeedback.text = "changed your element";

			gestureFire.SetActive(true);

			float time = 0;
			foreach (VideoPlayer video in gestureFire.GetComponents<VideoPlayer>()) {
				video.Play();
				time = (float)video.length;
			}

			StartCoroutine(HideImage(time, gestureFire));
		} else if (input.IsCombinationPressed(Finger.RING_LEFT)) {
			//Finger.PINK_LEFT, Finger.PINK_RIGHT, Finger.RING_LEFT, Finger.RING_RIGHT, Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT, Finger.POINT_LEFT, Finger.POINT_RIGHT
			GameManager.game.playerFightData.element = elementType.Earth;
			element.text = "Your element: Earth";
			txtFeedback.text = "changed your element";

			gestureEarth.SetActive(true);

			float time = 0;
			foreach (VideoPlayer video in gestureEarth.GetComponents<VideoPlayer>()) {
				video.Play();
				time = (float)video.length;
			}

			StartCoroutine(HideImage(time, gestureEarth));
		} else {
			toggler = false;
		}

		if (mana <= 100) {
			FindObjectOfType<SoundManager>().Play("Alert");
			txtFeedback.text = "don't forget about your mana!";
		}
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
}

//HEALING E, HIGH R, MEDIUM C, LOW U