using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class DogDie : MonoBehaviour {
	public Transform happyGuy, dog, sadGuy;
	void Start() {
		if (StoryManager.story.level == Level.GRASSLAND) {
			GameManager.game.storyData.isDogDeath = true;
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
}
