using System;
using System.Collections;
using UnityEngine;
public enum EnemyType {
	ARMORED_SKELETON, SOLDIER, DEMON, GUI
}

public class EnemyAI : MonoBehaviour {
	int healthPoints = 100;
	bool hit = false;
	public int health { get { return healthPoints; } }
	public EnemyType enemyType;

	void Start() {
		Attack();
	}

	public void Attack() {
		StartCoroutine(AttackCoroutine());
	}

	public void Hit(int points) {
		hit = true;
		healthPoints -= points;
		if (health <= 0) {
			Die();
			return;
		}
		GetComponentInChildren<Animator>().SetTrigger("Hit");
	}

	void Die() {
		GetComponentInChildren<Animator>().SetTrigger("Die");
		StartCoroutine(DieCoroutine());
	}

	IEnumerator AttackCoroutine() {
		float timeBeforeNextAttack = UnityEngine.Random.Range(2.0f, 5f);
		for (int i = 0; i < 5; i++) {
			if (hit) { // reset the attack timer when hit
				i = 0;
				hit = false;
			}
			if (healthPoints <= 0) yield return null; // end attack because death
			yield return new WaitForSeconds(timeBeforeNextAttack / 5f);
		}
		GetComponentInChildren<Animator>().SetTrigger("Attack");
		AttackingPlayer.player.Hit(10);
		Attack();
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
}
