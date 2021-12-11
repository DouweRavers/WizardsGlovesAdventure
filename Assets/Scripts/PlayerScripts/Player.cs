using UnityEngine;

public class Player : MonoBehaviour {
	public static Player player;
	public InputManager input;
	public PlayerUI UI;
	public MovementController movement;
	public SerialController leftController, rightController;

	void Awake() {
		player = this;
	}

	void Start() {
		leftController.portName = GameManager.game.COML;
		rightController.portName = GameManager.game.COMR;
	}



}
