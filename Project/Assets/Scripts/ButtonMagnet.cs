using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Magnet))]
public class ButtonMagnet : MonoBehaviour, IButtonListener {

	private Magnet magnetComponent;

	// Use this for initialization
	void Start () {
		magnetComponent = gameObject.GetComponent<Magnet> ();
		magnetComponent.enabled = false;
	}
	
	public void onButtonPressed() {
		magnetComponent.enabled = true;
	}
	
	public void onButtonReleased() {
		magnetComponent.enabled = false;
	}
}
