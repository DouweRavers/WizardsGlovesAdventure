using System;
using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
	public EnemyType enemyType;
	public void Die() {
		GetComponentInChildren<Animator>().SetTrigger("Die");
		StartCoroutine(DieCoroutine());
	}

	IEnumerator DieCoroutine() {
		yield return new WaitForSeconds(3.5f);
		int[] deathEnemyIDs = GameManager.game.storyData.deathEnemyIDs;
		int[] newDeathEnemyIDs = new int[deathEnemyIDs.Length + 1];
		Array.Copy(deathEnemyIDs, newDeathEnemyIDs, deathEnemyIDs.Length);
		newDeathEnemyIDs[deathEnemyIDs.Length] = GameManager.game.enemyFightData.enemyID;
		GameManager.game.storyData.deathEnemyIDs = newDeathEnemyIDs;
		GameManager.game.LoadWorldScene();
	}
}
