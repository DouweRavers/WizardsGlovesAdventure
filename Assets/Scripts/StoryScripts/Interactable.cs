using UnityEngine;

public abstract class Interactable : MonoBehaviour {
	public Finger[] fingers;

	void Start() {
		GetComponentInParent<StoryCheckpoint>().AddInteractable(this);
	}

	public abstract void PerformAction();
	public abstract void UpdateState();
}
