using UnityEngine;

public class Player : MonoBehaviour {
	public static Player player;
	public InputManager input;
	public PlayerUI UI;
	public MovementController movement;

	void Awake() {
		player = this;
	}



}
