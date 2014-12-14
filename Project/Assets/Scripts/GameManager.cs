using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject pausePanel;
	public Text timeTextField;
	public GameObject gameCompletedPanel;
	public Text highscoresTextField;


	private string timeText;
	public Timer timer;
	public Transform eventSystem;

	public MouseLocker mouseLocker;
	public MouseLook mouseLookX;
	public MouseLook mouseLookY;
	public PlaceTargetWithMouse placer;

	public int gameState;
	public const int GAME_STATE_RUNNING = 0;
	public const int GAME_STATE_PAUSED = 1;
	public const int NUM_HIGHSCORES = 5;
	public string HIGHSCORE_TAG = "highscore";

	//Singleton
	private static GameManager instance = null;
	public static GameManager Instance {
		get { return instance; }
	}
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
//		DontDestroyOnLoad(transform.gameObject);
	}

	void Start() {
		HIGHSCORE_TAG += "_lvl_" + Application.loadedLevel + "_";
		gameState = GAME_STATE_RUNNING;
		resumeGame();
		Instantiate(eventSystem, Vector3.zero, transform.rotation);
		timer.startTimer();
//		mouseLocker.setMouseLocked(true);
	}

	public static void GameOver() {
		Debug.Log("Game Over!");
		Application.LoadLevel(Application.loadedLevel);
	}

	public void pauseGame() {
		gameState = GAME_STATE_PAUSED;
		Debug.Log("PAUSE");
		Time.timeScale = 0f;
		pausePanel.SetActive(enabled);
		setControlEnabled(false);
	}

	public void resumeGame() {
		gameState = GAME_STATE_RUNNING;
		pausePanel.SetActive(false);
		Time.timeScale = 1f;
		setControlEnabled(true);
	}

	public void restartGame() {
		resumeGame();
		Application.LoadLevel(Application.loadedLevel);
	}

	public void gameCompleted() {
		Debug.Log("Game Completed Method");
		setControlEnabled(false);
		Time.timeScale = 0f;
		timer.stopTimer();
		float myScore = timer.timeElapsed;
		List<float> highscores = new List<float>();
		int myPlacement = NUM_HIGHSCORES;
		for(int i = 0; i < NUM_HIGHSCORES; ++i) {
			highscores.Add(PlayerPrefs.GetFloat(HIGHSCORE_TAG + i, 60f + i * 27));
			if(myScore < highscores[i] && myPlacement > i) {
				myPlacement = i;
			}
		}
		if(myPlacement < NUM_HIGHSCORES) {
			highscores.RemoveAt(NUM_HIGHSCORES-1); //remove the new worst score
			highscores.Insert (myPlacement, myScore);
		}

		//save the new highsore
		PlayerPrefs.SetFloat(HIGHSCORE_TAG + myPlacement, myScore);

		gameCompletedPanel.SetActive(true);
		string highscoretext = "";
		for(int i = 0; i < NUM_HIGHSCORES; ++i) {
			highscoretext += (i+1) + ".\t" + System.Math.Round(highscores[i], 2) + "s " + (myPlacement == i ? "\tNEW!" : "") + "\n";
		}
		highscoretext.Trim();
		highscoresTextField.text = highscoretext;
	}

	public void nextLevel() {
		resumeGame();
		//TODO If we gots more than 2 scenes, change this.
		Application.LoadLevel(Application.loadedLevel == 0 ? 1 : 0);
    }

	void OnGUI() {
		timeTextField.text = "Time: " + System.Math.Round(timer.timeElapsed, 2);
	}

	void setControlEnabled(bool enabled) {
		mouseLookX.setEnabled(enabled);
		mouseLookY.setEnabled(enabled);
		mouseLocker.setMouseLocked(enabled);
		placer.setEnabled(enabled);
	}

	void Update() {
		if (Input.GetKeyDown("escape")) {
			if(gameState == GAME_STATE_RUNNING)
				pauseGame();
			else {
				resumeGame();
			}
		}
		if(Input.GetKeyDown(KeyCode.L)) {
			mouseLocker.setMouseLocked(true);
		}
		
		if(Input.GetKeyDown(KeyCode.R)) {
			resumeGame();
			restartGame();
		}

		if(Input.GetKeyDown(KeyCode.I)) { //<------------------ remove in demo? IWIN btn
			gameCompleted();
		}

		if(Input.GetKeyDown(KeyCode.F1)) {
			Application.LoadLevel(0);
        }

		if(Input.GetKeyDown(KeyCode.F2)) {
			Application.LoadLevel(1);
        }
    
	}

}
