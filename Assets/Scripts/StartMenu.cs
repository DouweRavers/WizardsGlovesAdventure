using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartMenu : MonoBehaviour {
	public void OnPlay() {
		GameManager.game.LoadLevel(Level.TOWN);
	}

	public void OnQuit() {
		Application.Quit();
	}
}
