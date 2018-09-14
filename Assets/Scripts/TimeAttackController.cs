using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TimeAttackController : MonoBehaviour {

	// Text
	public Text scoreText;
	public Text highScoreText;
	public Text gameoverText;
	public Text highText;
	//Slider
	public Slider timeSlider;
	// Shape
	public Sprite[] shapes;
	public Image shape;
	// Button
	public GameObject restart;
	public GameObject yes;
	public GameObject no;
	public GameObject help;
	public GameObject back;
	// Audio
	public AudioSource soundRight;
	public AudioSource soundWrong;
	public AudioSource soundMove;
	public AudioSource soundRestart;
	// Score
	private int score;
	private int highScore;
	// 
	private int oldShape;
	private int newShape;
	// Timer
	private float timer;
	private float timeCount = 0;
	private int decTimer;
	int state = 0;
	//0: main menu, 1: playing, 2: game over

	void Start() {
		StartGame();
	}

	public void StartGame () { 
		highScore = PlayerPrefs.GetInt ("HighScoreTimeAttack", 0);
		highText.text = "HIGH SCORE\n";
		highScoreText.text = highScore.ToString ();
		timer = 30;
		timeCount = 0;
		score = 0;
		gameoverText.text = "";
		scoreText.text = "";
		timeSlider.gameObject.SetActive (true);
		back.SetActive (false);
		help.SetActive (false);
		restart.SetActive (false);
		WaveOne ();
	}

	void Update(){
		if (state != 1)
			return;
		timeCount += Time.deltaTime;
		timeSlider.value = 1 - timeCount / timer;
		if (timeCount > timer)
			GameOver ();
	}

	void WaveOne() {
		newShape = Random.Range (0, shapes.Length);
		shape.sprite = shapes [newShape];
		shape.gameObject.SetActive (true);
		UpdateScore ();
		score++;
		Invoke ("WaveTwo", 1);
	}

	void WaveTwo() {
		oldShape = newShape;
		NewWave ();
		state = 1;
		soundRight.Play ();
		UpdateScore();
		yes.SetActive (true);
		no.SetActive (true);	
	}

	void NewWave() {
		int[] randomShape = new int[] {oldShape, Random.Range (0, shapes.Length)};
		newShape = randomShape [Random.Range (0,2)];
		shape.sprite = shapes [newShape];
	}

	public void AddScoreYes () {
		if (oldShape == newShape) {
			soundRight.Play ();
			score++;
			NewWave ();
		} else
			GameOver ();
		UpdateScore ();
	}

	public void AddScoreNo () {
		if (oldShape != newShape) {
			soundRight.Play ();
			score++;
			oldShape = newShape;
			NewWave ();
		} else
			GameOver ();
		UpdateScore ();
	}

	public void UpdateScore () {
		scoreText.text = score.ToString ();
		if (score > highScore) {
			highScore = score;
			PlayerPrefs.SetInt ("HighScoreTimeAttack", score);
			PlayerPrefs.Save ();
		}
	}

	public void GameOver() {
		state = 2;
		soundWrong.Play ();
		shape.gameObject.SetActive (false);
		gameoverText.text = "G A M E O V E R";
		timeSlider.value = 0.0f;
		timeSlider.gameObject.SetActive (false);
		back.SetActive (true);	
		help.SetActive (true);
		restart.SetActive (true);
		yes.SetActive (false);
		no.SetActive (false);
	}

	public void Restart() {
		soundRestart.Play ();
		StartGame ();
	}

	public void Help() {
		soundMove.Play ();
		SceneManager.LoadScene ("Help");
	}

	public void Back() {
		soundMove.Play ();
		SceneManager.LoadScene ("MainMenu");
	}
}