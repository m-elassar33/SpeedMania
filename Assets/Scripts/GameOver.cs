using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public Button restart;
	public Button quit;
	public AudioSource otherAudio;

	// Use this for initialization
	void Start () {
		otherAudio.Play ();
		restart.GetComponent<Button>().onClick.AddListener(restartFunc);
		quit.GetComponent<Button>().onClick.AddListener(quitFunc);
	}

	void restartFunc(){
		SceneManager.LoadScene("main");
	}

	void quitFunc(){
		Application.Quit();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
