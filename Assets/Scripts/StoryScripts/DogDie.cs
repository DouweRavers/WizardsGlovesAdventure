using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class DogDie : MonoBehaviour {
	public Transform happyGuy, dog, sadGuy;
	void Start() {
		if (StoryManager.story.level == Level.GRASSLAND) {
			//GameManager.game.storyData.isDogDeath = true;
			//Debug.Log("Is dog dead?: " + GameManager.game.storyData.isDogDeath);
			if (GameManager.game.storyData.isDogDeath) {
				GetComponent<VIDE_Assign>().overrideStartNode = 1;
				happyGuy.gameObject.SetActive(false);
				dog.gameObject.SetActive(false);
				sadGuy.gameObject.SetActive(true);
			}
		}
	}

	public void Die() {
		GetComponent<Animator>().SetTrigger("Die");
	}

	public void KillDog() {
		GameManager.game.storyData.isDogDeath = true;
	}
}
