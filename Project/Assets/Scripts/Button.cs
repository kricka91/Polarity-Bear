using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	int numPressers = 0; //Number of pressing objects colliding with the button
	bool pressed = false;
	IButtonListener[] myListeners;
	public GameObject[] listeners;

	void Awake() {
		myListeners = new IButtonListener[listeners.Length];
		for(int i = 0; i < listeners.Length; ++i) {
			myListeners[i] = (IButtonListener) listeners[i].GetComponent(typeof(IButtonListener));
		}
	}
	

	void OnCollisionEnter (Collision collisionInfo) {
		if(collisionInfo.gameObject.tag == "Polarized")
			++numPressers;
	}

	void OnCollisionExit (Collision collisionInfo) {
		if(collisionInfo.gameObject.tag == "Polarized")
			--numPressers;
	}

	void Update() {
		if(pressed && numPressers <= 0) {
			pressed = false;
			foreach(IButtonListener listener in myListeners)
				listener.onButtonReleased();
		} else if(!pressed && numPressers > 0) {
			pressed = true;
			foreach(IButtonListener listener in myListeners)
				listener.onButtonPressed();
		}
	} 

}
