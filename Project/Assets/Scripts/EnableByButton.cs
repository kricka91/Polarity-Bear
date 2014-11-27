using UnityEngine;
using System.Collections;

public class EnableByButton : MonoBehaviour, IButtonListener  {

	public void onButtonPressed() {
		gameObject.SetActive(true);
	}
	
	public void onButtonReleased() {
		gameObject.SetActive(false);
	}
}
