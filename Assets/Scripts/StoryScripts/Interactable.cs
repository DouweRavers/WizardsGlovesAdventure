using UnityEngine;

public abstract class Interactable : MonoBehaviour {
	public Finger[] fingers;

	protected void Start() {
		GetComponentInParent<StoryCheckpoint>().AddInteractable(this);
	}

	public abstract void PerformAction();
	public abstract void UpdateState();
}
