using UnityEngine;

public struct EnemyFightData {
	public int enemyID;
	public EnemyType enemyType;
	public EnemyFightData(int id = 0, EnemyType type = EnemyType.ARMORED_SKELETON) {
		this.enemyID = id;
		this.enemyType = type;
	}

}

public class FightManager : MonoBehaviour {
	public static FightManager fight;
	public Transform enemies;

	void Start() {
		fight = this;
		foreach (EnemyAI enemyAI in enemies.GetComponentsInChildren(typeof(EnemyAI), true)) {
			if (enemyAI.enemyType == GameManager.game.enemyFightData.enemyType) enemyAI.gameObject.SetActive(true);
			else enemyAI.gameObject.SetActive(false);
		}
	}
}
