using UnityEngine;
using UnityEngine.Events;


public class EnemyInteractable : Interactable {
	public int enemyID = 0; // unique id for every enemy (gets assigned at runtime)
	public EnemyType enemyType; // defines the type of enemy
	public UnityEvent OnDeath;

	public override void PerformAction() {
		if (Player.player.input.IsCombinationPressed(Finger.THUMB_RIGHT, Finger.POINT_RIGHT)) {
			StoryManager.story.select.Play();
			Fight();
		}
	}
	public override void UpdateState() {
	}

	public void Fight() {
		StoryManager.story.SaveStory();
		GameManager.game.LoadFightScene(enemyID, enemyType);
	}

	public void Die() {
		OnDeath.Invoke();
	}
}
