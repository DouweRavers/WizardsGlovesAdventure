using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour {
	public static GameManager game;
	public StoryData storyData;
	public EnemyFightData enemyFightData;
	public PlayerFightData playerFightData;

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
		storyData = new StoryData("Dummy", new int[0]);
		enemyFightData = new EnemyFightData();
		enemyFightData.enemyType = EnemyType.SOLDIER;
		playerFightData = new PlayerFightData();
	}



	public void LoadFightScene(int enemyID, EnemyType enemyType) {
		StoryManager.story.SaveStory();
		enemyFightData = new EnemyFightData();
		enemyFightData.enemyID = enemyID;
		enemyFightData.enemyType = enemyType;
		SceneManager.LoadSceneAsync(4);
	}
	public void LoadFightSceneAgain()
	{
		SceneManager.LoadScene(4);
	}

	public void LoadWorldScene() {
		SceneManager.LoadScene(3);
	}

	public void LoadIntroToFightScene(string type)
    {
		if (type.Equals("Earth"))
        {
			playerFightData.element = elementType.Earth;
        } else if (type.Equals("Fire"))
		{
			playerFightData.element = elementType.Fire;
		} else if (type.Equals("Dark"))
		{
			playerFightData.element = elementType.Dark;
		} else if (type.Equals("Light"))
		{
			playerFightData.element = elementType.Light;
		}
		SceneManager.LoadScene(1);
	}
}
