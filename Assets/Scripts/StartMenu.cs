using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartMenu : MonoBehaviour
{
	public Slider slider;

	private AsyncOperation loader;

	public void OnPlay()
	{
		slider.gameObject.SetActive(true);
		loader = SceneManager.LoadSceneAsync(1);
	}

	public void OnQuit()
	{
		Application.Quit();
	}

	void Update()
	{
		if (loader == null || loader.isDone) slider.gameObject.SetActive(false);
		else
		{
			slider.gameObject.SetActive(true);
			slider.value = loader.progress;
		}
	}
}
