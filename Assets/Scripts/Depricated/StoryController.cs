using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour {
	enum location { START, CHURCH, CASTLE, CAMP, GATE, TOWN, MONSTER };
	public Transform[] locations;
	public enum Phase { START, WALKING, CHURCH, OUTDOOR_CASTLE, CAMP, GATE, TOWN, MONSTER, NONE };

	Phase current_phase = Phase.START, next_phase = Phase.START;

	void Update() {
		UpdatePhase();
	}

	void UpdatePhase() {
		switch (current_phase) {
			case Phase.START: {
					if (Player.player.input.IsCombinationPressed(Finger.THUMB_LEFT, Finger.POINT_LEFT)) {
						Player.player.movement.GoToLocation(locations[(int)location.CHURCH].position);
						next_phase = Phase.WALKING;
					}
					if (Player.player.input.IsCombinationPressed(Finger.THUMB_RIGHT, Finger.MIDDLE_RIGHT)) {
						Player.player.movement.GoToLocation(locations[(int)location.CASTLE].position);
						next_phase = Phase.WALKING;
					}
					break;
				}
			case Phase.CHURCH: {
					if (Player.player.input.IsCombinationPressed(Finger.THUMB_LEFT, Finger.POINT_LEFT)) {
						Player.player.movement.GoToLocation(locations[(int)location.START].position);
						next_phase = Phase.WALKING;
					}
					if (Player.player.input.IsCombinationPressed(Finger.THUMB_RIGHT, Finger.MIDDLE_RIGHT)) {
						Player.player.movement.GoToLocation(locations[(int)location.CASTLE].position);
						next_phase = Phase.WALKING;
					}
					if (Player.player.input.IsCombinationPressed(Finger.THUMB_LEFT, Finger.THUMB_RIGHT)) {
						Player.player.movement.GoToLocation(locations[(int)location.GATE].position);
						next_phase = Phase.WALKING;
					}
					break;
				}
			case Phase.OUTDOOR_CASTLE: {
					if (Player.player.input.IsCombinationPressed(Finger.THUMB_LEFT, Finger.POINT_LEFT)) {
						Player.player.movement.GoToLocation(locations[(int)location.CHURCH].position);
						next_phase = Phase.WALKING;
					}
					if (Player.player.input.IsCombinationPressed(Finger.THUMB_RIGHT, Finger.MIDDLE_RIGHT)) {
						Player.player.movement.GoToLocation(locations[(int)location.START].position);
						next_phase = Phase.WALKING;
					}
					if (Player.player.input.IsCombinationPressed(Finger.PINK_RIGHT, Finger.THUMB_RIGHT)) {
						Player.player.movement.GoToLocation(locations[(int)location.CAMP].position);
						next_phase = Phase.WALKING;
					}
					break;
				}
			case Phase.CAMP: {
					if (Player.player.input.IsCombinationPressed(
						Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT,
						Finger.POINT_LEFT, Finger.POINT_RIGHT)) {
						Player.player.movement.GoToLocation(locations[(int)location.TOWN].position);
						next_phase = Phase.WALKING;
					}
					break;
				}
			case Phase.GATE: {
					if (Player.player.input.IsCombinationPressed(
						Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT,
						Finger.POINT_LEFT, Finger.POINT_RIGHT)) {
						Player.player.movement.GoToLocation(locations[(int)location.TOWN].position);
						next_phase = Phase.WALKING;
					}
					break;
				}
			case Phase.TOWN: {
					Player.player.movement.GoToLocation(locations[(int)location.MONSTER].position);
					next_phase = Phase.WALKING;
					break;
				}
			case Phase.MONSTER: {
					if (Player.player.input.IsCombinationPressed(
						Finger.MIDDLE_LEFT, Finger.MIDDLE_RIGHT,
						Finger.POINT_LEFT, Finger.POINT_RIGHT)) {
						SceneManager.LoadSceneAsync(2);
						next_phase = Phase.NONE;
					}
					break;
				}
			case Phase.WALKING: {
					if (Player.player.movement.velocity.magnitude < 0.1f) {
						if (Vector3.Distance(Player.player.transform.position, locations[(int)location.START].position) < 3) next_phase = Phase.START;
						if (Vector3.Distance(Player.player.transform.position, locations[(int)location.CHURCH].position) < 3) next_phase = Phase.CHURCH;
						if (Vector3.Distance(Player.player.transform.position, locations[(int)location.CASTLE].position) < 3) next_phase = Phase.OUTDOOR_CASTLE;
						if (Vector3.Distance(Player.player.transform.position, locations[(int)location.CAMP].position) < 3) next_phase = Phase.CAMP;
						if (Vector3.Distance(Player.player.transform.position, locations[(int)location.GATE].position) < 3) next_phase = Phase.GATE;
						if (Vector3.Distance(Player.player.transform.position, locations[(int)location.TOWN].position) < 3) next_phase = Phase.TOWN;
						if (Vector3.Distance(Player.player.transform.position, locations[(int)location.MONSTER].position) < 3) next_phase = Phase.MONSTER;
					}
					break;
				}
			case Phase.NONE: {
					break;
				}
			default:
				break;
		}
		current_phase = next_phase;
	}
}
