using UnityEngine;
using Cinemachine;
public abstract class Interactable : MonoBehaviour {
	public Finger[] fingers;
	public GameObject SelectIndicator = null;

	public void Select() {
		if (SelectIndicator != null) {
			SelectIndicator.SetActive(true);
		}
	}

	public void Deselect() {
		if (SelectIndicator != null) {
			SelectIndicator.SetActive(false);
		}
	}
	public abstract void PerformAction();
	public abstract void UpdateState();
}
