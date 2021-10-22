using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerRoot : MonoBehaviour
{
	public static PlayerRoot player;
	public InputManager input;
	public PlayerUI UI;
	public MovementController movement;

	void Start()
	{
		player = this;
	}



}
