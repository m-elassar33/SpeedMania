using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

	public Button start;
	public Button mute;
	public Button howToPlay;
	public Button credits;
	public AudioSource otherAudio;
	public Text t;

	// Use this for initialization
	void Start () {
		otherAudio.Play ();
		start.GetComponent<Button>().onClick.AddListener(startFunc);
		mute.GetComponent<Button>().onClick.AddListener(muteFunc);
		howToPlay.GetComponent<Button>().onClick.AddListener(howToPlayFunc);
		credits.GetComponent<Button>().onClick.AddListener(creditsFunc);
	}

	void startFunc(){
		SceneManager.LoadScene("main");
	}

	void muteFunc(){
		AudioListener.pause = !AudioListener.pause;
	}

	void howToPlayFunc(){
		t.text = "Move with Arrows Jump with Space";
	}

	void creditsFunc(){
		t.text = "Road texture was shared by Omar Tag & All sounds from youtube";
	}

	// Update is called once per frame
	void Update () {
		
	}
}
