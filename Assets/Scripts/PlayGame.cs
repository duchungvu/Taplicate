using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour {

	public AudioSource sound;

	public void Back() {
		sound.Play ();
		SceneManager.LoadScene ("MainMenu");
	}

	public void Help() {
		sound.Play ();
		SceneManager.LoadScene ("Help");
	}

	public void Classic() {
		sound.Play ();
		SceneManager.LoadScene ("Classic");
	}

	public void TimeAttack() {
		sound.Play ();
		SceneManager.LoadScene ("TimeAttack");
	}
}
