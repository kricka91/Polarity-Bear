using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	IGoalListener[] myListeners;
	public GameObject[] listeners;
	
	void Awake() {
		myListeners = new IGoalListener[listeners.Length];
		for (int i = 0; i < listeners.Length; ++i) {
				myListeners [i] = (IGoalListener)listeners[i].GetComponent(typeof(IGoalListener));
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "First Person Controller") {
			foreach(IGoalListener listener in myListeners)
				listener.onPlayerEnter();
		}
	}
}
