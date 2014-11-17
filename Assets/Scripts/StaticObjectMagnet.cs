using UnityEngine;
using System.Collections;

public class StaticObjectMagnet : MonoBehaviour {
	public float power;
	public float radius;
	public float charge;
	private GameObject[] players;
	// Use this for initialization
	void Start () {
//		GameObject triggerSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
//		MeshRenderer mr = triggerSphere.GetComponent (MeshRenderer);
//		mr.enabled = false;
//		triggerSphere.collider.isTrigger = true;
		players = GameObject.FindGameObjectsWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject player in players) {
			float distance = Vector3.Distance (gameObject.transform.position, player.transform.position);
			if (distance < radius) {
				float tmp = ((radius - distance)/radius);
				float magnitude = power*tmp*tmp;
				Vector3 dirVector = player.transform.position - gameObject.transform.position;
				Vector3.Normalize(dirVector); 
				player.rigidbody.velocity += (charge * magnitude * dirVector);
			}
		}
	}
}
