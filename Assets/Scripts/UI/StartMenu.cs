using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartMenu : MonoBehaviour {
	public AudioSource klick;
	public VideoPlayer videoPlayer;
	public GameObject settings;

	void Start() {
		videoPlayer.gameObject.SetActive(false);
	}
	public void OnPlay() {
		klick.Play();
		videoPlayer.gameObject.SetActive(true);
		videoPlayer.Play();
		StartCoroutine(PlayCoroutine());
	}

	public void OnQuit() {
		klick.Play();
		StartCoroutine(QuitCoroutine());
	}
	public void OnSettings(bool setView) {
		klick.Play();
		settings.SetActive(setView);
	}
	public void OnComLeft(int value) {
		klick.Play();
		GameManager.game.COM1 = "COM" + value;
	}
	public void OnComRight(int value) {
		klick.Play();
		GameManager.game.COM2 = "COM" + value;
	}
	IEnumerator PlayCoroutine() {
		yield return new WaitForSeconds((float)videoPlayer.length);
		GameManager.game.LoadLevel(Level.TOWN);
	}
	IEnumerator QuitCoroutine() {
		while (klick.isPlaying) {
			yield return null;
		}
		Application.Quit();
	}
}
