using UnityEngine;
using System.Collections;

public class ButtonMovement : MonoBehaviour, IButtonListener {
	private Vector3 buttonUpPos, buttonDownPos, targetPos, oldPos;
	private float timer;
	public Vector3 buttonDisplacement;
	public float animationLength;

	// Use this for initialization
	void Start () {
		buttonUpPos = gameObject.transform.position;
		buttonDownPos = buttonUpPos + buttonDisplacement;
		timer = animationLength;
	}

	void Update () {

		if (timer < animationLength) {
			timer += Time.deltaTime;
			gameObject.transform.position = Vector3.Lerp (oldPos, targetPos, timer / animationLength);
		}
	}
	
	// Update is called once per frame
	public void onButtonPressed() {
		targetPos = buttonDownPos;
		oldPos = buttonUpPos;
		timer = 0;
	}
	
	public void onButtonReleased() {
		targetPos = buttonUpPos;
		oldPos = buttonDownPos;
		timer = 0;
	}
}
