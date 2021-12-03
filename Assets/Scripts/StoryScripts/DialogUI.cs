using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogUI : MonoBehaviour {
	public Image image;
	public TMP_Text text, titleText;
	public string title { set { titleText.text = value; } }

	public void SetSprite(Sprite sprite) {
		image.sprite = sprite;
	}

	public void SetText(string comment) {
		if (comment.Equals(text.text)) return;
		for (int i = 1; i < text.transform.parent.childCount; i++) {
			Destroy(text.transform.parent.GetChild(i).gameObject);
		}
		text.text = comment;
	}
	public void SetText(string[] comments) {
		for (int i = 1; i < text.transform.parent.childCount; i++) {
			Destroy(text.transform.parent.GetChild(i).gameObject);
		}
		text.text = comments[0];
		for (int i = 1; i < comments.Length; i++) {
			GameObject newTextObject = Instantiate(text.gameObject);
			newTextObject.transform.SetParent(text.transform.parent);
			newTextObject.GetComponent<TMP_Text>().text = comments[i];
		}
	}
}
