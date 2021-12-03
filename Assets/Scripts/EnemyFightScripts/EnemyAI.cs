using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public enum EnemyType {
	ARMORED_SKELETON, SOLDIER, DEMON, GUI
}

public class EnemyAI : MonoBehaviour
{
	//public BaseRainScript RainScript;

	public Slider healthBar;

	float timeBeforeNextAttack;
	float healthPoints = 100;
	public float health { get { return healthPoints; } }
	public int damage;
	public int defense;

	bool boost = false;
	int boostCount = 0;
	int randHealthBoost = UnityEngine.Random.Range(20, 50);
	bool hit = false;

	public EnemyType enemyType;
	public static EnemyAI AI;

	int randDefend;

	void Start()
	{
		Attack();
		AI = this;

		healthBar.maxValue = healthPoints;
	}

	public void Attack()
	{
		StartCoroutine(AttackCoroutine());
	}

	public void Hit(float points)
	{
		hit = true;
		randDefend = UnityEngine.Random.Range(0, defense);
		if (randDefend == 0)
		{
			if (boost != true)
			{
				GetComponentInChildren<Animator>().SetTrigger("Defend");
			}
			else
			{
				GetComponentInChildren<Animator>().SetTrigger("DefendBoost");
			}
		}
		else
		{
			healthPoints -= points;
			healthBar.value = healthPoints;
			if (health <= 0)
			{
				Die();
				return;
			}

			if (randHealthBoost <= health && health <= 50 && boostCount <= 0)
			{
				enemyBoost();
			}
			if (boost == true)
			{
				GetComponentInChildren<Animator>().SetTrigger("HitBoost");
				FindObjectOfType<SoundManager>().Play("EnemyHit");

				enemyNormal();
			}
			else
			{
				GetComponentInChildren<Animator>().SetTrigger("Hit");
				FindObjectOfType<SoundManager>().Play("EnemyHit");
			}
		}
	}

	void Die()
	{
		GetComponentInChildren<Animator>().SetTrigger("Die");
		FindObjectOfType<SoundManager>().Play("EnemyDeath");
		StartCoroutine(DieCoroutine());
	}

	IEnumerator AttackCoroutine()
	{
		timeBeforeNextAttack = UnityEngine.Random.Range(2.0f, 5.0f);
		for (int i = 0; i < 5; i++)
		{
			if (hit)
			{ // reset the attack timer when hit
				i = 0;
				hit = false;
			}
			if (healthPoints <= 0) yield return null; // end attack because death
			yield return new WaitForSeconds(timeBeforeNextAttack / 5f);
		}
		if (boost != true)
		{
			GetComponentInChildren<Animator>().SetTrigger("Attack");
		}
		else
		{
			GetComponentInChildren<Animator>().SetTrigger("AttackBoost");
		}
		Attack();
	}
	/*
	IEnumerator DieCoroutine()
	{
		yield return new WaitForSeconds(3.5f);
		int[] deathEnemyIDs = GameManager.game.storyData.deathEnemyIDs;
		int[] newDeathEnemyIDs = new int[deathEnemyIDs.Length + 1];
		Array.Copy(deathEnemyIDs, newDeathEnemyIDs, deathEnemyIDs.Length);
		newDeathEnemyIDs[deathEnemyIDs.Length] = GameManager.game.enemyFightData.enemyID;
		GameManager.game.storyData.deathEnemyIDs = newDeathEnemyIDs;
		GameManager.game.LoadWorldScene();
	}
	*/
	public void enemyBoost()
	{
		damage = 20;
		defense = 2;
		float timeBeforeNextAttack = UnityEngine.Random.Range(1.0f, 3.0f);

		boost = true;
		boostCount++;
	}
	public void enemyNormal()
	{
		damage = 10;
		defense = 2;
		timeBeforeNextAttack = UnityEngine.Random.Range(2.0f, 5.0f);

		boost = false;
	}

	public void EnemyAttack()
	{
		if (boost != true)
		{
			FindObjectOfType<SoundManager>().Play("EnemyNormalAttack");
		}
		else
		{
			FindObjectOfType<SoundManager>().Play("EnemyAttackBoosted");
		}
		AttackingPlayer.player.Hit(damage);
	}

	public void setRandDefend(int newRandDefend)
	{
		randDefend = newRandDefend;
	}

	IEnumerator DieCoroutine()
	{
		yield return new WaitForSeconds(3.5f);
		int[] deathEnemyIDs = GameManager.game.storyData.deathEnemyIDs;
		int[] newDeathEnemyIDs = new int[deathEnemyIDs.Length + 1];
		Array.Copy(deathEnemyIDs, newDeathEnemyIDs, deathEnemyIDs.Length);
		newDeathEnemyIDs[deathEnemyIDs.Length] = GameManager.game.enemyFightData.enemyID;
		GameManager.game.storyData.deathEnemyIDs = newDeathEnemyIDs;
		GameManager.game.LoadLevel(GameManager.game.storyData.level);
	}
}
