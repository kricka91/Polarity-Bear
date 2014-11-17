using UnityEngine;
using System.Collections;

public class MouseLocker : MonoBehaviour {
	


	void Update() {

		if (Input.GetKeyDown("escape"))
			Screen.lockCursor = false;
		if(Input.GetMouseButtonDown(1)) {
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