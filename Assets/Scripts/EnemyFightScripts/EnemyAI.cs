using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour {
	//public BaseRainScript RainScript;

	public Slider healthBar;
	public Text txtWarning;
	public Image imgBackground, imgWarning;
	public Image imgTutorial, imgTutorialWarning;
	public Text[] Tutorial;

	float timeBeforeNextAttack;
	float healthPoints = 100;
	public float health { get { return healthPoints; } }
	public int damage;
	public int defense;

	public bool boost = false;
	int boostCount = 0;
	//int randHealthBoost;

	bool hit = false;
	public bool isActive;
	bool firstHit = true;

	public EnemyType enemyType;
	public static EnemyAI AI;

	int randDefend;

	public ParticleSystem boostAnimation;

	void Start() {
		AI = this;
		healthBar.maxValue = healthPoints;

		if (GameManager.game.enemyFightData.tutorialBeginnerEnabled)
        {
			isActive = false;
        } else
        {
			isActive = true;
        }

		//txtWarning.enabled = false;
		//imgBackground.enabled = false;
		//imgWarning.enabled = false;
		//EnableUITutorial(false);

		//int randHealthBoost = UnityEngine.Random.Range(20, 50);
	}

	public void Attack() {
		Debug.Log("hit it - " + isActive);
		StartCoroutine(AttackCoroutine());
	}

	public void Hit(float points) {
		hit = true;
		randDefend = UnityEngine.Random.Range(0, defense);
		if (randDefend == 0) {
			//if (boost != true) {
			GetComponentInChildren<Animator>().SetTrigger("Defend");
			//} else {
				//GetComponentInChildren<Animator>().SetTrigger("DefendBoost");
			//}
		} else {
			healthPoints -= points;
			healthBar.value = healthPoints;
			if (health <= 0) {
				Die();
				return;
			}
			if(GameManager.game.enemyFightData.tutorialBeginnerEnabled)
            {
				if (firstHit)
                {
					firstHit = false;
					activateEnemy();
				}
            }

			GetComponentInChildren<Animator>().SetTrigger("Hit");
			FindObjectOfType<SoundManager>().Play("EnemyHit");
			if (boost == true) {
				//GetComponentInChildren<Animator>().SetTrigger("HitBoost");
				//FindObjectOfType<SoundManager>().Play("EnemyHit");

				EnemyNormal();
			}
			//else {
				//GetComponentInChildren<Animator>().SetTrigger("Hit");
				//FindObjectOfType<SoundManager>().Play("EnemyHit");
			//}

			if (20 <= health && health <= 50 && boostCount <= 0) {
				if (GameManager.game.enemyFightData.tutorialBeginnerEnabled)
                {
					StartCoroutine(EnemyBoostTutorial());
                } else
                {
					EnemyBoost(20, 2);
				}
			}
		}
	}

	void Die() {
		GetComponentInChildren<Animator>().SetTrigger("Die");
		FindObjectOfType<SoundManager>().Play("EnemyDeath");
		StartCoroutine(DieCoroutine());
	}

	IEnumerator AttackCoroutine() {

		timeBeforeNextAttack = UnityEngine.Random.Range(2.0f, 5.0f);

		for (int i = 0; i < 5; i++) {
			if (hit) { // reset the attack timer when hit
				i = 0;
				hit = false;
			}
			if (healthPoints <= 0) yield return null; // end attack because death
			yield return new WaitForSeconds(timeBeforeNextAttack / 5f);
		}
		EnemyAttack();
		//if (boost != true) {
		GetComponentInChildren<Animator>().SetTrigger("Attack");
		//} else {
			//GetComponentInChildren<Animator>().SetTrigger("AttackBoost");
		//}
		Attack();
	}

	IEnumerator EnemyBoostTutorial()
    {
		isActive = false;

		FindObjectOfType<SoundManager>().Play("Alert");
		EnemyBoost(20, 2);

		EnableUITutorial(true);

		yield return new WaitForSeconds(7);

		EnableUITutorial(false);

		activateEnemy();
	}

	public void activateEnemy()
    {
		Debug.Log("yeah");
		isActive = true;
		Attack();
	}
	public void deactivateEnemy()
    {
		isActive = false;
    }

	public void EnemyBoost(int newDamage, int newDefense) {
		damage = newDamage;
		defense = newDefense;

		float timeBeforeNextAttack = UnityEngine.Random.Range(1.0f, 3.0f);

		boost = true;
		boostCount++;

		boostAnimation.Play();
		FindObjectOfType<SoundManager>().Play("EnemyBoost");

		txtWarning.text = "enemy boost";
		txtWarning.enabled = true;
		imgBackground.enabled = true;
		imgWarning.enabled = true;
	}

	public void EnemyNormal() {
		Debug.Log("normal");

		boostAnimation.Stop();

		imgWarning.enabled = false;
		txtWarning.enabled = false;
		imgBackground.enabled = false;

		damage = 10;
		defense = 2;
		timeBeforeNextAttack = UnityEngine.Random.Range(2.0f, 5.0f);

		boost = false;
	}

	public void EnemyAttack() {
		/*
		if (boost != true) {
			FindObjectOfType<SoundManager>().Play("EnemyNormalAttack");
		} else {
			//FindObjectOfType<SoundManager>().Play("EnemyAttackBoosted");
		}
		*/
		if (isActive)
        {
			AttackingPlayer.player.Hit(damage);
		} 
	}

	public void setRandDefend(int newRandDefend) {
		randDefend = newRandDefend;
	}

	IEnumerator DieCoroutine() {
		yield return new WaitForSeconds(3.5f);
		int[] deathEnemyIDs = GameManager.game.storyData.deathEnemyIDs;
		int[] newDeathEnemyIDs = new int[deathEnemyIDs.Length + 1];
		Array.Copy(deathEnemyIDs, newDeathEnemyIDs, deathEnemyIDs.Length);
		newDeathEnemyIDs[deathEnemyIDs.Length] = GameManager.game.enemyFightData.enemyID;
		GameManager.game.storyData.deathEnemyIDs = newDeathEnemyIDs;
		GameManager.game.LoadLevel(GameManager.game.storyData.level);
	}

	void EnableUITutorial(bool isEnabled)
    {
		imgTutorial.enabled = isEnabled;
		imgTutorialWarning.enabled = isEnabled;
		for (int i = 0; i < Tutorial.Length; i++)
		{
			Tutorial[i].enabled = isEnabled;
		}
		Tutorial[0].text = "Enemy Boost!";
		Tutorial[1].text = "Once the enemy feels threatened, his defense mechanism activates:";
		Tutorial[2].text = "stronger, more frequent attacks";
		Tutorial[3].text = "more difficult to hit";
		Tutorial[4].text = "ends when you're able to hit him!";
	}
}
