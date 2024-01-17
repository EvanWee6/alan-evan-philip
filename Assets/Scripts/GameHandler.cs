using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour {


	private LevelGrid levelGrid;
	
	private void Start() {
		Debug.Log("Hello");
		levelGrid = new LevelGrid(20,20);
	}

}
