using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartMenu : MonoBehaviour {
	public AudioSource klick;
	public VideoPlayer videoPlayer;
	public AudioSource music;
	public GameObject settings;

	void Start() {
		if (videoPlayer != null) videoPlayer.gameObject.SetActive(false);
		//GameManager.game.difficultyModifier = 1.0f;
	}
	public void OnPlay() {
		klick.Play();
		videoPlayer.gameObject.SetActive(true);
		videoPlayer.Play();
		music.Stop();
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

	public void OnDifficulty(int value)
	{
		klick.Play();
		//Debug.Log("The string: " + value);
		if (value == 0) { GameManager.game.difficultyModifier = 1.0f; }
		else if (value == 1) { GameManager.game.difficultyModifier = 1.2f; }
		else if (value == 2) { GameManager.game.difficultyModifier = 1.5f; }
		else { GameManager.game.difficultyModifier = 1.0f; }
		//Debug.Log("Difficulty is: " + GameManager.game.difficultyModifier);
	}

	public void OnComLeft(int value) {
		value += 1;
		klick.Play();
		GameManager.game.COM1 = "COM" + value;
	}
	public void OnComRight(int value) {
		value += 1;
		klick.Play();
		GameManager.game.COM2 = "COM" + value;
	}

	public void GoToCurrentLevel() {
		if (GameManager.game.storyData.level == Level.NONE) return;
		GameManager.game.LoadLevel(GameManager.game.storyData.level);
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
