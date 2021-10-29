using UnityEngine;
using UnityEngine.Events;

public enum EnemyType {
	ARMORED_SKELETON, GUI_MONSTER
}

public class EnemyInteractable : Interactable {
	public int enemyID = 0; // unique id for every enemy (gets assigned at runtime)
	public EnemyType enemyType; // defines the type of enemy
	public UnityEvent OnDeath;
	bool pressed = false;

	public override void PerformAction() {
		if (Player.player.input.IsCombinationPressed(fingers) && !pressed) {
			pressed = true;
			StoryManager.story.SaveStory();
			GameManager.game.LoadFightScene(enemyID, enemyType);
		} else pressed = false;
	}
	public override void UpdateState() {
	}

	public void Die() {
		OnDeath.Invoke();
		gameObject.SetActive(false);
	}
}
