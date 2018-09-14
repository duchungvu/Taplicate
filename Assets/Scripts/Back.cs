using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour {

	public AudioSource sound;

	public void Help() {
		sound.Play ();
		SceneManager.LoadScene ("MainMenu");
	}
}
