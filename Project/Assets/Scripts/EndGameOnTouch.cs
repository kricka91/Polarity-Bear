using UnityEngine;
using System.Collections;

public class EndGameOnTouch : MonoBehaviour {

	// Use this for initialization
	void OnCollisionEnter(Collision collision) {
		// layer #10 is the player layer
		if (collision.collider.gameObject.layer == 10) {
			GameManager.Instance.gameCompleted();
		}
	}
}
