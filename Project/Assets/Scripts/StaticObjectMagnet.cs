using UnityEngine;
using System.Collections;

public class StaticObjectMagnet : MonoBehaviour {
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
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject player in players) {
			Vector3 playerPosition = player.transform.position;
			Vector3 closestPointOnMesh = collider.ClosestPointOnBounds(playerPosition);
			float distance = Vector3.Distance (closestPointOnMesh, playerPosition);
			CharacterControls character = player.GetComponent<CharacterControls>();
			if (distance < radius) {
				if (character != null) character.setAffectedByPolarity(true);
				float tmp = ((radius - distance)/radius);
				float magnitude = power*tmp*tmp;
				Vector3 dirVector = playerPosition - closestPointOnMesh;
				Vector3.Normalize(dirVector); 
				player.rigidbody.velocity += (magnitude * dirVector) * Time.deltaTime * 60;
			} else {
				if (character != null) character.setAffectedByPolarity(false);
			}
		}
	}
}
