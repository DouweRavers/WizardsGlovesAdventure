using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Level {
	TOWN, GRASSLAND, DUNGEON, NONE
}
public enum SpellType {
	ROCK, FIRE, LIGHT, DARK
}

public enum EnemyType {
	GOBLIN, KOBOLD, TROLL, DEMON, DRAGON, NONE
}

public enum Finger {
	PINK_LEFT = 0, RING_LEFT = 1, MIDDLE_LEFT = 2, POINT_LEFT = 3, THUMB_LEFT = 4,
	THUMB_RIGHT = 5, POINT_RIGHT = 6, MIDDLE_RIGHT = 7, RING_RIGHT = 8, PINK_RIGHT = 9,
	NONE = 10, BLOCK = 11
};

public enum elementType {
	Fire, Earth, Dark, Light
}

public enum attackType
{
	NONE, LOW, MEDIUM, HIGH
}

public enum GestureType {
	FIRE, EARTH, LIGHT, DARK, LOW, MEDIUM, HIGH
}

public struct StoryData {
	public Level level;
	public string checkpoint_name; // name of active checkpoint
	public int[] deathEnemyIDs; // IDs of all the enemies -> used for removing dead ones on load
	public int karma;
	public bool isDogDeath;
	public int[] spells;

	public StoryData(int dummy = 0) {
		level = Level.NONE;
		checkpoint_name = "Dummy";
		deathEnemyIDs = new int[0];
		karma = 0;
		isDogDeath = false;
		spells = new int[] { 1, 1, 1, 1 };
	}

	public void AddSpell(int spellType) {
		if (spells[spellType] < 3) spells[spellType]++;
	}

	public void addEnemyDeath(int enemyId) {
		int[] newList = new int[deathEnemyIDs.Length + 1];
		for (int i = 0; i < deathEnemyIDs.Length; i++) { newList[i] = deathEnemyIDs[i]; }
		newList[deathEnemyIDs.Length] = enemyId;
		deathEnemyIDs = newList;
	}
}

public struct EnemyFightData {
	public int enemyID;
	public EnemyType enemyType;
	public bool tutorialEnabled;
	public EnemyFightData(int id = 0, EnemyType typeEn = EnemyType.NONE, bool tutorial = false) {
		this.enemyID = id;
		this.enemyType = typeEn;
		this.tutorialEnabled = tutorial;
	}
}

public struct PlayerFightData {
	public elementType element;
	public attackType attack;
	public bool[] unlockedAttacks; //fire, earth, light, dark
}