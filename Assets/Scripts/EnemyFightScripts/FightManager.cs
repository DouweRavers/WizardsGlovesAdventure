using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour {
	public static FightManager fight;
	public Transform enemies;

	public int damageDecrease = 1;
	int immune_count = 0;

	public Text txtWarning, txtFeedback;
	public Image imgBackground, imgWarning, imgFeedback;

	public ParticleSystem defendAnimation;

	void Start() {
		fight = this;
		foreach (EnemyAI enemyAI in enemies.GetComponentsInChildren(typeof(EnemyAI), true)) {
			if (enemyAI.enemyType == GameManager.game.enemyFightData.enemyType) enemyAI.gameObject.SetActive(true);
			else enemyAI.gameObject.SetActive(false);
		}
		txtWarning.enabled = false;
		imgBackground.enabled = false;
		imgWarning.enabled = false;
		//txtFeedback.enabled = false;
		//imgFeedback.enabled = false;
	}

	public void HitEnemy(float damageAttack, int amount_used, bool isImmune) {
		if (!isImmune) {
			enemies.GetComponentInChildren<EnemyAI>(false).Hit(damageAttack);

			defendAnimation.Stop();

			imgWarning.enabled = false;
			txtWarning.enabled = false;
			imgBackground.enabled = false;
		} else {
			/*
			if (immune_count <= 0 && GameManager.game.isTutorial)
            {
				
				EnemyAI.AI.enemyActive = false;

				txtFeedback.text = "Use another attack";
				txtFeedback.enabled = true;
				imgFeedback.enabled = true;
            }
			*/
			defendAnimation.Play();

			txtWarning.text = "enemy immune";
			txtWarning.enabled = true;
			imgBackground.enabled = true;
			imgWarning.enabled = true;

			immune_count++;
		}

	}
}
