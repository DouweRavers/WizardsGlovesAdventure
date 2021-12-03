using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MovementController : MonoBehaviour {
	// Check if player is moving or not
	public bool isMoving { get { return velocity.sqrMagnitude > 0.1f; } }
	// Check the players speed in vector3 form
	public Vector3 velocity { get { return navMeshAgent.velocity; } }
	NavMeshAgent navMeshAgent;
	Animator animator;
	AudioSource footsteps;
	List<Vector3> path;


	void Start() {
		navMeshAgent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
		footsteps = GetComponent<AudioSource>();
	}

	// Set target for nav mesh to walk to.
	public void GoToLocation(Vector3 location) {
		path = null;
		navMeshAgent.destination = location;
	}

	/*
		nav mesh agent will walk sequentially from point to point (0 -> lenght)
		last location is the destination.
	*/
	public void GoToLocation(Vector3[] path) {
		this.path = new List<Vector3>(path);
	}

	void Update() {
		if (velocity.magnitude > 0.1f && !footsteps.isPlaying) footsteps.Play();
		else if (velocity.magnitude < 0.1f && footsteps.isPlaying) footsteps.Stop();
		animator.SetFloat("MoveX", animator.transform.InverseTransformVector(velocity).x / 3.5f);
		animator.SetFloat("MoveY", animator.transform.InverseTransformVector(velocity).z / 3.5f);
		UpdatePath();
	}

	void UpdatePath() {
		if (navMeshAgent.destination != null &&
			Vector3.Distance(transform.position, navMeshAgent.destination) > 1 || path == null) return;
		if (path.Count == 0) path = null;
		if (path.Count == 1) {
			GoToLocation(path[0]);
		} else {
			navMeshAgent.destination = path[0];
			path.RemoveAt(0);
		}
	}
}
