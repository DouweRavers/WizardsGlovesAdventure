using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingScreen : MonoBehaviour {
	public Slider slider;
	public AsyncOperation loader;
	void Update() {
		if (loader == null) return;
		slider.value = loader.progress * slider.maxValue;
		if (loader.isDone) Destroy(gameObject);
	}
}
