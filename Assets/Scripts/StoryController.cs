using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour
{
	enum location { START, CHURCH, CASTLE, CAMP, GATE, TOWN, MONSTER };
	public Transform[] locations;
	public enum Phase { START, WALKING, CHURCH, OUTDOOR_CASTLE, CAMP, GATE, TOWN, MONSTER, NONE };

	Phase current_phase = Phase.START, next_phase = Phase.START;

	void Update()
	{
		UpdatePhase();
	}

	void UpdatePhase()
	{
		switch (current_phase)
		{
			case Phase.START:
				{
					if (PlayerRoot.player.input.IsCombinationPressed(InputManager.Fingers.THUMB_LEFT, InputManager.Fingers.POINT_LEFT))
					{
						PlayerRoot.player.movement.GoToLocation(locations[(int)location.CHURCH].position);
						next_phase = Phase.WALKING;
					}
					if (PlayerRoot.player.input.IsCombinationPressed(InputManager.Fingers.THUMB_RIGHT, InputManager.Fingers.MIDDLE_RIGHT))
					{
						PlayerRoot.player.movement.GoToLocation(locations[(int)location.CASTLE].position);
						next_phase = Phase.WALKING;
					}
					break;
				}
			case Phase.CHURCH:
				{
					if (PlayerRoot.player.input.IsCombinationPressed(InputManager.Fingers.THUMB_LEFT, InputManager.Fingers.POINT_LEFT))
					{
						PlayerRoot.player.movement.GoToLocation(locations[(int)location.START].position);
						next_phase = Phase.WALKING;
					}
					if (PlayerRoot.player.input.IsCombinationPressed(InputManager.Fingers.THUMB_RIGHT, InputManager.Fingers.MIDDLE_RIGHT))
					{
						PlayerRoot.player.movement.GoToLocation(locations[(int)location.CASTLE].position);
						next_phase = Phase.WALKING;
					}
					if (PlayerRoot.player.input.IsCombinationPressed(InputManager.Fingers.THUMB_LEFT, InputManager.Fingers.THUMB_RIGHT))
					{
						PlayerRoot.player.movement.GoToLocation(locations[(int)location.GATE].position);
						next_phase = Phase.WALKING;
					}
					break;
				}
			case Phase.OUTDOOR_CASTLE:
				{
					if (PlayerRoot.player.input.IsCombinationPressed(InputManager.Fingers.THUMB_LEFT, InputManager.Fingers.POINT_LEFT))
					{
						PlayerRoot.player.movement.GoToLocation(locations[(int)location.CHURCH].position);
						next_phase = Phase.WALKING;
					}
					if (PlayerRoot.player.input.IsCombinationPressed(InputManager.Fingers.THUMB_RIGHT, InputManager.Fingers.MIDDLE_RIGHT))
					{
						PlayerRoot.player.movement.GoToLocation(locations[(int)location.START].position);
						next_phase = Phase.WALKING;
					}
					if (PlayerRoot.player.input.IsCombinationPressed(InputManager.Fingers.PINK_RIGHT, InputManager.Fingers.THUMB_RIGHT))
					{
						PlayerRoot.player.movement.GoToLocation(locations[(int)location.CAMP].position);
						next_phase = Phase.WALKING;
					}
					break;
				}
			case Phase.CAMP:
				{
					if (PlayerRoot.player.input.IsCombinationPressed(
						InputManager.Fingers.MIDDLE_LEFT, InputManager.Fingers.MIDDLE_RIGHT,
						InputManager.Fingers.POINT_LEFT, InputManager.Fingers.POINT_RIGHT))
					{
						PlayerRoot.player.movement.GoToLocation(locations[(int)location.TOWN].position);
						next_phase = Phase.WALKING;
					}
					break;
				}
			case Phase.GATE:
				{
					if (PlayerRoot.player.input.IsCombinationPressed(
						InputManager.Fingers.MIDDLE_LEFT, InputManager.Fingers.MIDDLE_RIGHT,
						InputManager.Fingers.POINT_LEFT, InputManager.Fingers.POINT_RIGHT))
					{
						PlayerRoot.player.movement.GoToLocation(locations[(int)location.TOWN].position);
						next_phase = Phase.WALKING;
					}
					break;
				}
			case Phase.TOWN:
				{
					PlayerRoot.player.movement.GoToLocation(locations[(int)location.MONSTER].position);
					next_phase = Phase.WALKING;
					break;
				}
			case Phase.MONSTER:
				{
					if (PlayerRoot.player.input.IsCombinationPressed(
						InputManager.Fingers.MIDDLE_LEFT, InputManager.Fingers.MIDDLE_RIGHT,
						InputManager.Fingers.POINT_LEFT, InputManager.Fingers.POINT_RIGHT))
					{
						SceneManager.LoadSceneAsync(2);
						next_phase = Phase.NONE;
					}
					break;
				}
			case Phase.WALKING:
				{
					if (PlayerRoot.player.movement.velocity.magnitude < 0.1f)
					{
						if (Vector3.Distance(PlayerRoot.player.transform.position, locations[(int)location.START].position) < 3) next_phase = Phase.START;
						if (Vector3.Distance(PlayerRoot.player.transform.position, locations[(int)location.CHURCH].position) < 3) next_phase = Phase.CHURCH;
						if (Vector3.Distance(PlayerRoot.player.transform.position, locations[(int)location.CASTLE].position) < 3) next_phase = Phase.OUTDOOR_CASTLE;
						if (Vector3.Distance(PlayerRoot.player.transform.position, locations[(int)location.CAMP].position) < 3) next_phase = Phase.CAMP;
						if (Vector3.Distance(PlayerRoot.player.transform.position, locations[(int)location.GATE].position) < 3) next_phase = Phase.GATE;
						if (Vector3.Distance(PlayerRoot.player.transform.position, locations[(int)location.TOWN].position) < 3) next_phase = Phase.TOWN;
						if (Vector3.Distance(PlayerRoot.player.transform.position, locations[(int)location.MONSTER].position) < 3) next_phase = Phase.MONSTER;
					}
					break;
				}
			case Phase.NONE:
				{
					break;
				}
			default:
				break;
		}
		current_phase = next_phase;
	}
}
