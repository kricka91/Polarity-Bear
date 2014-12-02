using UnityEngine;
using System.Collections;

/**
 * Keeps track of when player dies
 */
public class PlayerDeath : MonoBehaviour {
	private GameObject player;

	 /* At which point the player should be considered out of the map (starts falling to its death */
	public float fallStart;
	/* At which point the fall ends and the player dies */
	public float fallEnd;
	/* Start color of ambient light */
	private Color startColor;

	// Use this for initialization
	void Start () {
		player = (GameObject.Find("First Person Controller"));
		startColor = RenderSettings.ambientLight;
	}
	
	// Update is called once per frame
	void Update () {
		// Make world fade to black as player falls //TODO: Make whole scren black?
		if(player.transform.position.y < fallStart){
			float c = Mathf.Abs((player.transform.position.y-fallEnd)/(fallEnd-fallStart));
			RenderSettings.ambientLight = new Color(c*startColor.r,c*startColor.g,c*startColor.b);
		}
		
		if(player.transform.position.y < fallEnd){
			Debug.Log("Player has fallen into the void");
			Application.LoadLevel (Application.loadedLevelName);
		}
	}
}
