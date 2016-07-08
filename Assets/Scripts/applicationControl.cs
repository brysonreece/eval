using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class applicationControl : MonoBehaviour {

	Scene currentScene;
	// Use this for initialization
	public void Quit () {
		Application.Quit();
	}

	public void reloadScene () {
		currentScene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (currentScene.buildIndex);
	}
}
