using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MovementController : MonoBehaviour
{
	public NavMeshAgent navMeshAgent;
	public Animator animator;
	public Vector3 velocity { get { return navMeshAgent.velocity; } }
	public void GoToLocation(Vector3 location)
	{
		navMeshAgent.destination = location;
	}

	void Update()
	{
		animator.SetFloat("velocity", navMeshAgent.velocity.magnitude / 3.5f);
	}
}
