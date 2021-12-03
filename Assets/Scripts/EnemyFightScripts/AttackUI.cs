using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackUI : MonoBehaviour {
	public Slider healthBar;
	public Text mana;
	void Start() {
		healthBar.maxValue = AttackingPlayer.player.health;
		mana.text = "Mana: " + AttackingPlayer.player.mana;
	}

	void Update() {
		healthBar.value = AttackingPlayer.player.health;
		mana.text = "Mana: " + AttackingPlayer.player.mana;
	}
}
