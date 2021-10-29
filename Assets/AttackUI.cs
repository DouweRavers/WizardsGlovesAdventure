using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackUI : MonoBehaviour {
	public Slider healthBar;
	void Start() {
		healthBar.maxValue = AttackingPlayer.player.health;
	}

	void Update() {
		healthBar.value = AttackingPlayer.player.health;
	}


}
