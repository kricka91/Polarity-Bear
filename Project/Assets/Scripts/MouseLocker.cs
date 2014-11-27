using UnityEngine;
using System.Collections;

public class MouseLocker : MonoBehaviour {

	public Texture2D crosshairImage;
	private Vector2 crosshairDimension;
	private int yOffset;

	void Start() {
//		Screen.lockCursor = true;
		crosshairDimension.x = 40;
		crosshairDimension.y = 40;
		yOffset = 30;
	}

	void OnGUI()
	{
		float xMin = (Screen.width / 2) - (crosshairDimension.x / 2);
		float yMin = ((Screen.height / 2) - (crosshairDimension.y / 2)) - yOffset;
		GUI.DrawTexture(new Rect(xMin, yMin, crosshairDimension.x, crosshairDimension.y), crosshairImage);
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