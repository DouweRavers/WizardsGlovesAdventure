using System.Collections;
using UnityEngine;


public struct EnemyFightData {
	public int enemyID;
	public EnemyType enemyType;
	public EnemyFightData(int id = 0, EnemyType typeEn = EnemyType.ARMORED_SKELETON) {
		this.enemyID = id;
		this.enemyType = typeEn;
	}
}

public class FightManager : MonoBehaviour {
	public static FightManager fight;
	public Transform enemies;

	public int damageDecrease = 5;

	void Start() {
		fight = this;
		foreach (EnemyAI enemyAI in enemies.GetComponentsInChildren(typeof(EnemyAI), true)) {
			if (enemyAI.enemyType == GameManager.game.enemyFightData.enemyType) enemyAI.gameObject.SetActive(true);
			else enemyAI.gameObject.SetActive(false);
		}
	}

	public void HitEnemy(float damageAttack, int amount_used) {
		damageAttack = damageAttack - 1 * amount_used * ((damageAttack)/(damageAttack + damageDecrease));
		Debug.Log(damageAttack);
		Debug.Log(amount_used);
		if (damageAttack >= 0)
		{
			enemies.GetComponentInChildren<EnemyAI>(false).Hit(damageAttack);
		}
	}
}
