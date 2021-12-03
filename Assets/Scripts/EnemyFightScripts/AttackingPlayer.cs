using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public enum elementType {
	Fire, Earth, Dark, Light
}

public struct PlayerFightData {
	public elementType element;
	public bool[] unlockedAttacks;
	/*
	public PlayerFightData(elementType type = elementType.Fire)
	{
		this.element = type;
	}
	*/
}

public class AttackingPlayer : MonoBehaviour {
	public static AttackingPlayer player;
	public InputManager input;

	public float health { get { return healthPoints; } }
	public int mana { get { return manaPoints; } }

	int x, y, z = 0;
	public float damageLow = 5;
	public float damageMedium = 10;
	public float damageHigh = 15;

	public float healthPoints = 100;
	public float healing = 10;

	public int manaPoints = 500;
	public int manaLow = 50;
	public int manaMedium = 100;
	public int manaHigh = 150;

	public ParticleSystem fireAttackLow, fireAttackMedium, fireAttackHigh, lightAttackLow, lightAttackMedium, lightAttackHigh, darkAttackLow, darkAttackMedium, darkAttackHigh, earthAttackLow, earthAttackMedium, earthAttackHigh;
	public ParticleSystem heal;
	public Transform enemy;
	public Image vid;
	bool toggler = true;

	public cameraShake camerashake;

	//public elementType element = GameManager.game.playerFightData.element;

	void Awake() {
		player = this;
	}
	void Update() {
		//LOW (u)
		if (input.IsCombinationPressed(Finger.POINT_RIGHT)) { //Finger.THUMB_LEFT, Finger.THUMB_RIGHT, Finger.POINT_LEFT, Finger.POINT_RIGHT
			if (toggler) return;
			else toggler = true;

			//update mana
			manaPoints -= manaLow;
			//attack
			if (GameManager.game.playerFightData.element == elementType.Fire) {
				Debug.Log("FireLow");

				fireAttackLow.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackFireLow");
				FightManager.fight.HitEnemy(damageLow, x);
				/*
				vid.enabled = true;
				vid.color = Color.red;

				float time = 0;
				foreach (VideoPlayer video in vid.GetComponents<VideoPlayer>())
				{
					video.Play();
					time = (float)video.length;
				}
				*/
				x++;
				if (x > 3) { EnemyAI.AI.enemyBoost(); }
				y = z = 0;

				//StartCoroutine(HideImage(time));
			} else if (GameManager.game.playerFightData.element == elementType.Light) {
				Debug.Log("LightLow");

				lightAttackLow.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackLightLow");
				FightManager.fight.HitEnemy(damageLow, x);
				/*
				vid.enabled = true;
				vid.color = Color.white;

				float time = 0;
				foreach (VideoPlayer video in vid.GetComponents<VideoPlayer>())
				{
					video.Play();
					time = (float)video.length;
				}
				*/
				x++;
				if (x > 3) { EnemyAI.AI.enemyBoost(); }
				y = z = 0;

				//StartCoroutine(HideImage(time));
			} else if (GameManager.game.playerFightData.element == elementType.Dark) {
				Debug.Log("DarkLow");

				darkAttackLow.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackDarkLow");
				FightManager.fight.HitEnemy(damageLow, x);
				/*
				vid.enabled = true;
				vid.color = Color.black;

				float time = 0;
				foreach (VideoPlayer video in vid.GetComponents<VideoPlayer>())
				{
					video.Play();
					time = (float)video.length;
				}
				*/
				x++;
				if (x > 3) { EnemyAI.AI.enemyBoost(); }
				y = z = 0;

				//StartCoroutine(HideImage(time));
			} else if (GameManager.game.playerFightData.element == elementType.Earth) {
				Debug.Log("EarthLow");

				earthAttackLow.Play();
				//StartCoroutine(camerashake.Shake(.15f, .4f));
				FindObjectOfType<SoundManager>().Play("PlayerAttackEarth");
				FightManager.fight.HitEnemy(damageLow, x);
				/*
				vid.enabled = true;
				vid.color = Color.red;

				float time = 0;
				foreach (VideoPlayer video in vid.GetComponents<VideoPlayer>())
				{
					video.Play();
					time = (float)video.length;
				}
				*/
				x++;
				if (x > 3) { EnemyAI.AI.enemyBoost(); }
				y = z = 0;

				//StartCoroutine(HideImage(time));
			}
			/*
			float time = 0;
			foreach (VideoPlayer video in vid.GetComponents<VideoPlayer>())
			{
				video.Play();
				time = (float)video.length;
			}
			*/
			/*
			x++;
			if (x > 3) { EnemyAI.AI.enemyBoost(); }
			y = z = 0;
			*/
			//StartCoroutine(HideImage(time));

			//MEDIUM (c)
		} else if (input.IsCombinationPressed(Finger.THUMB_LEFT)) { //Finger.THUMB_LEFT, Finger.THUMB_RIGHT, Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT, Finger.RING_LEFT, Finger.RING_RIGHT
			if (toggler) return;
			else toggler = true;

			//update mana
			manaPoints -= manaMedium;
			//attack
			if (GameManager.game.playerFightData.element == elementType.Fire) {
				Debug.Log("FireMedium");

				fireAttackMedium.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackFireMedium");
				FightManager.fight.HitEnemy(damageMedium, y);
				/*
				vid.enabled = true;
				vid.color = Color.red;

				float time = 0;
				foreach (VideoPlayer video in vid.GetComponents<VideoPlayer>())
				{
					video.Play();
					time = (float)video.length;
				}
				*/
				y++;
				if (y > 3) { EnemyAI.AI.enemyBoost(); }
				x = z = 0;

				//StartCoroutine(HideImage(time));
			} else if (GameManager.game.playerFightData.element == elementType.Light) {
				Debug.Log("LightMedium");

				lightAttackMedium.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackLightMedium");
				FightManager.fight.HitEnemy(damageMedium, y);
				/*
				vid.enabled = true;
				vid.color = Color.white;

				float time = 0;
				foreach (VideoPlayer video in vid.GetComponents<VideoPlayer>())
				{
					video.Play();
					time = (float)video.length;
				}
				*/
				y++;
				if (y > 3) { EnemyAI.AI.enemyBoost(); }
				x = z = 0;

				//StartCoroutine(HideImage(time));
			} else if (GameManager.game.playerFightData.element == elementType.Dark) {
				Debug.Log("DarkMedium");

				darkAttackMedium.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackDarkMedium");
				FightManager.fight.HitEnemy(damageMedium, y);
				/*
				vid.enabled = true;
				vid.color = Color.black;

				float time = 0;
				foreach (VideoPlayer video in vid.GetComponents<VideoPlayer>())
				{
					video.Play();
					time = (float)video.length;
				}
				*/
				y++;
				if (y > 3) { EnemyAI.AI.enemyBoost(); }
				x = z = 0;

				//StartCoroutine(HideImage(time));
			} else if (GameManager.game.playerFightData.element == elementType.Earth) {
				Debug.Log("EarthMedium");

				//camerashake
				earthAttackMedium.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackEarth");
				FightManager.fight.HitEnemy(damageMedium, y);
				/*
				vid.enabled = true;
				vid.color = Color.red;

				float time = 0;
				foreach (VideoPlayer video in vid.GetComponents<VideoPlayer>())
				{
					video.Play();
					time = (float)video.length;
				}
				*/
				y++;
				if (y > 3) { EnemyAI.AI.enemyBoost(); }
				x = z = 0;

				//StartCoroutine(HideImage(time));
			}

			//HIGH (r)
		} else if (input.IsCombinationPressed(Finger.POINT_LEFT)) { //Finger.POINT_LEFT, Finger.POINT_RIGHT, Finger.PINK_LEFT, Finger.PINK_RIGHT, Finger.THUMB_LEFT, Finger.THUMB_RIGHT
			if (toggler) return;
			else toggler = true;

			//update mana
			manaPoints -= manaHigh;
			//attack
			if (GameManager.game.playerFightData.element == elementType.Fire) {
				Debug.Log("FireHigh");

				fireAttackHigh.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackFireHigh");
				FightManager.fight.HitEnemy(damageHigh, z);
				/*
				vid.enabled = true;
				vid.color = Color.red;

				float time = 0;
				foreach (VideoPlayer video in vid.GetComponents<VideoPlayer>())
				{
					video.Play();
					time = (float)video.length;
				}
				*/
				z++;
				if (z > 3) { EnemyAI.AI.enemyBoost(); }
				x = y = 0;

				//StartCoroutine(HideImage(time));
			} else if (GameManager.game.playerFightData.element == elementType.Light) {
				Debug.Log("LightHigh");

				lightAttackHigh.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackLightHigh");
				FightManager.fight.HitEnemy(damageHigh, z);
				/*
				vid.enabled = true;
				vid.color = Color.white;

				float time = 0;
				foreach (VideoPlayer video in vid.GetComponents<VideoPlayer>())
				{
					video.Play();
					time = (float)video.length;
				}
				*/
				z++;
				if (z > 3) { EnemyAI.AI.enemyBoost(); }
				y = x = 0;

				//StartCoroutine(HideImage(time));
			} else if (GameManager.game.playerFightData.element == elementType.Dark) {
				Debug.Log("DarkHigh");

				darkAttackHigh.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackDarkHigh");
				FightManager.fight.HitEnemy(damageHigh, z);
				/*
				vid.enabled = true;
				vid.color = Color.black;

				float time = 0;
				foreach (VideoPlayer video in vid.GetComponents<VideoPlayer>())
				{
					video.Play();
					time = (float)video.length;
				}
				*/
				z++;
				if (z > 3) { EnemyAI.AI.enemyBoost(); }
				y = x = 0;

				//StartCoroutine(HideImage(time));
			} else if (GameManager.game.playerFightData.element == elementType.Earth) {
				Debug.Log("EarthHigh");


				//StartCoroutine(camerashake.Shake(.15f, .4f));
				earthAttackHigh.Play();
				FindObjectOfType<SoundManager>().Play("PlayerAttackEarth");
				FightManager.fight.HitEnemy(damageHigh, z);
				/*
				vid.enabled = true;
				vid.color = Color.red;

				float time = 0;
				foreach (VideoPlayer video in vid.GetComponents<VideoPlayer>())
				{
					video.Play();
					time = (float)video.length;
				}
				*/
				z++;
				if (z > 3) { EnemyAI.AI.enemyBoost(); }
				y = x = 0;

				//StartCoroutine(HideImage(time));
			}
			//HEALING (e)
		} else if (input.IsCombinationPressed(Finger.MIDDLE_LEFT)) {
			if (healthPoints < 100) {
				heal.Play();
				if (healthPoints + healing > 100) {
					healthPoints = 100;
				} else {
					healthPoints += healing;
				}
			}
		} else {
			toggler = false;
		}
	}

	int function(int damage, int amountUsed, Color color, string attack, ParticleSystem attackSystem) {
		attackSystem.Play();
		FindObjectOfType<SoundManager>().Play(attack);
		FightManager.fight.HitEnemy(damage, amountUsed);
		vid.enabled = true;
		vid.color = color;
		/*
		float time = 0;
		foreach (VideoPlayer video in vid.GetComponents<VideoPlayer>())
		{
			video.Play();
			time = (float)video.length;
		}
		*/
		amountUsed++;
		return amountUsed;
	}

	public IEnumerator HideImage(float time) {
		yield return new WaitForSeconds(2);
		vid.enabled = false;
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
