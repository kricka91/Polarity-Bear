using UnityEngine;
using System.Collections;

/**
 * Keeps track of when player dies
 */
public class PlayerDeath : MonoBehaviour {
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = (GameObject.Find("First Person Controller"));
	}
	
	// Update is called once per frame
	void Update () {
		if(player.transform.position.y < -20){
			Debug.Log("Player has fallen into the void");
			Application.LoadLevel (Application.loadedLevelName);
		}
	}
}
