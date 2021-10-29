using UnityEngine;
using Cinemachine;

public struct StoryData {
	public string checkpoint_name; // name of active checkpoint
	public int[] deathEnemyIDs; // IDs of all the enemies -> used for removing dead ones on load

	public StoryData(string checkpoint, int[] enemyid) {
		checkpoint_name = checkpoint;
		deathEnemyIDs = enemyid;
	}
}

public class StoryManager : MonoBehaviour {
	public static StoryManager story;

	// Set the first checkpoint that will be loaded.
	public StoryCheckpoint startCheckpoint;
	StoryCheckpoint activeCheckpoint;
	bool movementToggle = true;

	void Awake() {
		story = this;
	}

	void Start() {
		activeCheckpoint = startCheckpoint;
		assignEnemyIds(); // configures enemyids, it counts from the hierarchy down and includes inactive ones.
		LoadStory();
	}

	void Update() {
		if (Vector3.Distance(Player.player.transform.position, activeCheckpoint.checkpointPosition.position) > 1) {
			if (!movementToggle) {
				CameraManager.cameraManager.SetPathCamera();
				movementToggle = true;
			}
			if (Player.player.movement.isMoving) return;
			Vector3[] path = new Vector3[] { // by default walks behind target to turn towards the target by the end.
				activeCheckpoint.checkpointPosition.position - activeCheckpoint.checkpointPosition.forward * 2,
				activeCheckpoint.checkpointPosition.position
				};
			Player.player.movement.GoToLocation(path);
		} else if (!Player.player.movement.isMoving) {
			if (movementToggle) {
				Player.player.transform.LookAt(activeCheckpoint.checkpointPosition.position + activeCheckpoint.checkpointPosition.forward);
				CameraManager.cameraManager.SetCheckpointCamera(activeCheckpoint.GetComponentInChildren<CinemachineVirtualCamera>());
				movementToggle = false;
			}
			activeCheckpoint.DoCurrentState();
			activeCheckpoint.CheckNextState();
		}
	}

	public void ChangeCheckpoint(StoryCheckpoint checkpoint) {
		activeCheckpoint.OnDeassignCheckpoint();
		activeCheckpoint = checkpoint;
		activeCheckpoint.OnAssignCheckpoint();
	}

	public void SaveStory() {
		// save the last played checkpoint
		GameManager.game.storyData.checkpoint_name = activeCheckpoint.name;
	}

	public void LoadStory() {
		if (GameManager.game.storyData.checkpoint_name.Equals("Dummy")) return;
		string lastCheckpointName = GameManager.game.storyData.checkpoint_name;
		foreach (StoryCheckpoint checkpoint in GetComponentsInChildren<StoryCheckpoint>()) {
			if (checkpoint.name.Equals(lastCheckpointName)) {
				activeCheckpoint = checkpoint;
				activeCheckpoint.TeleportPlayerToCheckpoint();
			}
		}
		foreach (EnemyInteractable enemy in GetComponentsInChildren<EnemyInteractable>()) {
			foreach (int id in GameManager.game.storyData.deathEnemyIDs) {
				if (enemy.enemyID == id) {
					enemy.Die();
					break;
				}
			}
		}
	}

	void assignEnemyIds() {
		EnemyInteractable[] enemyInteractables = GetComponentsInChildren<EnemyInteractable>(true);
		for (int i = 0; i < enemyInteractables.Length; i++) {
			enemyInteractables[i].enemyID = i;
		}
	}
}
