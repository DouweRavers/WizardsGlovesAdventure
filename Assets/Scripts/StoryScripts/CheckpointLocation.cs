using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CheckpointLocation : MonoBehaviour {
	public Mesh mesh;
	void OnDrawGizmos() {
		Gizmos.DrawMesh(mesh, 0, transform.position, transform.rotation, transform.localScale);
	}
}
