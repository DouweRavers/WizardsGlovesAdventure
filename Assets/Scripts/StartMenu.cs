using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartMenu : MonoBehaviour {
	public AudioSource klick;
	public void OnPlay() {
		klick.Play();
		StartCoroutine(PlayCoroutine());
	}

	public void OnQuit() {
		klick.Play();
		StartCoroutine(QuitCoroutine());
	}

	IEnumerator PlayCoroutine() {
		while (klick.isPlaying) {
			yield return null;
		}
		GameManager.game.LoadLevel(Level.TOWN);
	}
	IEnumerator QuitCoroutine() {
		while (klick.isPlaying) {
			yield return null;
		}
		Application.Quit();
	}
}
