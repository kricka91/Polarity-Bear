using UnityEngine;
using System.Collections;

public class Magnet : MonoBehaviour {
	public float power;
	public float radius;
	private GameObject[] players;
	// Use this for initialization
	void Start () {
		//		GameObject triggerSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		//		MeshRenderer mr = triggerSphere.GetComponent (MeshRenderer);
		//		mr.enabled = false;
		//		triggerSphere.collider.isTrigger = true;
		players = GameObject.FindGameObjectsWithTag ("Polarized");
	}


	void Update () {
		foreach (GameObject player in players) {
			Vector3 playerPosition = player.transform.position;
			float distance = Vector3.Distance (gameObject.transform.position, playerPosition);
			CharacterControls character = player.GetComponent<CharacterControls>();
			if (distance < radius) {
				if (character != null) character.setAffectedByPolarity(true);
				float tmp = ((radius - distance)/radius);
				float magnitude = power*tmp*tmp;
				Vector3 dirVector = playerPosition - gameObject.transform.position;
				Vector3.Normalize(dirVector); 
				player.rigidbody.velocity += (magnitude * dirVector) * Time.deltaTime * 60;
			} else {
				if (character != null) character.setAffectedByPolarity(false);
			}
		}
	}
}
