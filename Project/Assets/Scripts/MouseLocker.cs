using UnityEngine;
using System.Collections;

public class MouseLocker : MonoBehaviour {

	void Start() {
//		Screen.lockCursor = true;
	}


	void Update() {

		if (Input.GetKeyDown("escape"))
			Screen.lockCursor = false;
		if(Input.GetKeyDown(KeyCode.L)) {
			Screen.lockCursor = true;
		}

		if(Input.GetKeyDown(KeyCode.R)) {
			Application.LoadLevel(Application.loadedLevel);
		}
		/*
		if (!Screen.lockCursor && wasLocked) {
			wasLocked = false;
			DidUnlockCursor();
		} else
		if (Screen.lockCursor && !wasLocked) {
			wasLocked = true;
			DidLockCursor();
		}*/
	}
}