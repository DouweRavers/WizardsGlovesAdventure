using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;

public class StoryUI : MonoBehaviour {
	public Slider karmaSlider;
	public Volume volume;
	public TMPro.TMP_Text fire, earth, light, dark;

	void Update() {
		karmaSlider.value = GameManager.game.storyData.karma;
		if (volume.profile.TryGet<Bloom>(out var bloom)) {
			bloom.intensity.overrideState = true;
			bloom.intensity.value = 0.5f + (GameManager.game.storyData.karma) / 46f;

		}
		if (volume.profile.TryGet<Vignette>(out var vignette)) {
			vignette.intensity.overrideState = true;
			vignette.intensity.value = 0.5f - (0.25f + (GameManager.game.storyData.karma) / 92f);
		}
		fire.text = (GameManager.game.storyData.spells[0] + 1).ToString();
		earth.text = (GameManager.game.storyData.spells[1] + 1).ToString();
		light.text = (GameManager.game.storyData.spells[2] + 1).ToString();
		dark.text = (GameManager.game.storyData.spells[3] + 1).ToString();
	}
}
