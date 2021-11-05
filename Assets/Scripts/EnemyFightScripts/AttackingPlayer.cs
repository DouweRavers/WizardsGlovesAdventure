using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class AttackingPlayer : MonoBehaviour {
	public int health { get { return healthPoints; } }
	public static AttackingPlayer player;
	public InputManager input;
	int healthPoints = 100;
	public ParticleSystem fireAttack, lightAttack, darkAttack;
	public Transform enemy;
	public Image vid;
	bool toggler = true;
	void Awake() {
		player = this;
	}
	void Update() {
		if (input.IsCombinationPressed(Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT, Finger.POINT_LEFT, Finger.POINT_RIGHT)) {
			if (toggler) return;
			else toggler = true;
			fireAttack.Play();
			FightManager.fight.HitEnemy();
			vid.enabled = true;
			vid.color = Color.red;
			float time = 0;
			foreach (VideoPlayer video in vid.GetComponents<VideoPlayer>()) {
				video.Play();
				time = (float)video.length;
			}
			StartCoroutine(HideImage(time));
		} else if (input.IsCombinationPressed(Finger.RING_LEFT, Finger.RING_RIGHT, Finger.PINK_LEFT, Finger.PINK_RIGHT)) {
			if (toggler) return;
			else toggler = true;
			lightAttack.Play();
			FightManager.fight.HitEnemy();
			vid.enabled = true;
			vid.color = Color.white;
			float time = 0;
			foreach (VideoPlayer video in vid.GetComponents<VideoPlayer>()) {
				video.Play();
				time = (float)video.length;
			}
			StartCoroutine(HideImage(time));
		} else if (input.IsCombinationPressed(Finger.THUMB_LEFT, Finger.THUMB_RIGHT)) {
			if (toggler) return;
			else toggler = true;
			darkAttack.Play();
			FightManager.fight.HitEnemy();
			vid.enabled = true;
			vid.color = Color.black;
			float time = 0;
			foreach (VideoPlayer video in vid.GetComponents<VideoPlayer>()) {
				video.Play();
				time = (float)video.length;
			}
			StartCoroutine(HideImage(time));
		} else {
			toggler = false;
		}
	}

	public IEnumerator HideImage(float time) {
		yield return new WaitForSeconds(1);
		vid.enabled = false;
	}

	public void Hit(int points) {
		healthPoints -= points;
	}
}
