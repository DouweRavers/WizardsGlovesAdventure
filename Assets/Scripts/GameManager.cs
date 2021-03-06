using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour {
	public static GameManager game;
	public StoryData storyData;
	public EnemyFightData enemyFightData;
	public PlayerFightData playerFightData;
	public GameObject LoadingScreenPrefab;
	public string COM1 = "COM1", COM2 = "COM2";
	Transform loadingScreenTransform;
	private AsyncOperation loader;


	GameObject[] gameObjects; // a list of object that can live through scene changes

	void Awake() {
		// Make unique object that lives through different scenes
		GameObject[] objects = GameObject.FindGameObjectsWithTag("GameController");
		if (objects.Length > 1) {
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
		// Easy reference to object for other classes
		game = this;
		storyData = new StoryData(0);
		enemyFightData = new EnemyFightData(0, EnemyType.KOBOLD, false);
		playerFightData = new PlayerFightData();
	}

	public void LoadFightScene(int enemyID, EnemyType enemyType, bool tutorialEnabled) {
		StoryManager.story.SaveStory();
		enemyFightData = new EnemyFightData(enemyID, enemyType, tutorialEnabled);
		SceneManager.LoadSceneAsync(4);
	}

	public void LoadFightSceneAgain() {
		SceneManager.LoadScene(4);
	}

	public void LoadNextLevel() {
		if (StoryManager.story != null) StoryManager.story.SaveStory();
		switch (storyData.level) {
			case Level.NONE:
				LoadLevel(Level.TOWN);
				break;
			case Level.TOWN:
				LoadLevel(Level.GRASSLAND);
				break;
			case Level.GRASSLAND:
				LoadLevel(Level.DUNGEON);
				break;
			default:
				break;
		}
	}
	/*
	public void LoadIntroToFightScene(string type) {
		if (type.Equals("Earth")) {
			playerFightData.element = elementType.Earth;
		} else if (type.Equals("Fire")) {
			playerFightData.element = elementType.Fire;
		} else if (type.Equals("Dark")) {
			playerFightData.element = elementType.Dark;
		} else if (type.Equals("Light")) {
			playerFightData.element = elementType.Light;
		}
		SceneManager.LoadScene(1); //modify according to build settings!!
	}
	*/


	public void LoadLevel(Level level) {
		if (StoryManager.story != null) StoryManager.story.SaveStory();
		loadingScreenTransform = Instantiate(LoadingScreenPrefab).transform;
		loadingScreenTransform.SetParent(transform);
		switch (level) {
			case Level.TOWN:
				loadingScreenTransform.GetComponent<LoadingScreen>().loader = SceneManager.LoadSceneAsync(1);
				break;
			case Level.GRASSLAND:
				loadingScreenTransform.GetComponent<LoadingScreen>().loader = SceneManager.LoadSceneAsync(2);
				break;
			case Level.DUNGEON:
				loadingScreenTransform.GetComponent<LoadingScreen>().loader = SceneManager.LoadSceneAsync(3);
				break;
			default:
				break;
		}
	}

	public void AddSpell(int spellType) { storyData.AddSpell(spellType); }
	public int GetSpell(int spellType) { return storyData.spells[spellType]; }
	public void ChangeKarmaBy(int change) { storyData.karma += change; }
	public int GetKarma(int spellType) { return storyData.karma; }

}

