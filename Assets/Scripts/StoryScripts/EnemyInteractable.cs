using UnityEngine;
using UnityEngine.Events;


public class EnemyInteractable : Interactable {
	public int enemyID = 0; // unique id for every enemy (gets assigned at runtime)
	public EnemyType enemyType; // defines the type of enemy
	public UnityEvent OnDeath;

	public override void PerformAction() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			Fight();
		}
	}
	public override void UpdateState() {
	}

	public void Fight() {
		StoryManager.story.SaveStory();
		//GameManager.game.LoadFightScene(enemyID, enemyType);
		Die();
	}

	public void Die() {
		OnDeath.Invoke();
	}
}
