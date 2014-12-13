using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {


	public float timeElapsed;
	private bool running = false;

	public void startTimer() {
		running = true;
	}

	public void stopTimer() {
		running = false;
	}
	
	void Update()
	{
		if(running) {
			timeElapsed += Time.deltaTime;
		}
	}
}
