using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour, IButtonListener {

	IGoalListener[] myListeners;
	private Behaviour halo;
	   
	public GameObject[] listeners;
	
	void Awake() {
		myListeners = new IGoalListener[listeners.Length];
		for (int i = 0; i < listeners.Length; ++i) {
				myListeners [i] = (IGoalListener)listeners[i].GetComponent(typeof(IGoalListener));
		}
		halo = (Behaviour)GetComponent("Halo");
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "First Person Controller") {
			foreach(IGoalListener listener in myListeners)
				listener.onPlayerEnter();
		}
	}

	// Enable the halo when something presses the button
	public void onButtonPressed(){ 
		halo.enabled = true;
	}
	public void onButtonReleased(){
		halo.enabled = false;
	}
}
