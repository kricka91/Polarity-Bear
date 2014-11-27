using UnityEngine;
using System.Collections;

public class ButtonLight : MonoBehaviour, IButtonListener {

	public Light myLight;

	public void onButtonPressed() {
		myLight.intensity = 8f;
	}

	public void onButtonReleased() {
		myLight.intensity = 0;
	}
}
