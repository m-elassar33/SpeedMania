using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {

	public GameObject cam1;
	public GameObject cam2;
	bool pause;
	public Text t;
	public Button jump;
	public Button cameraBtn;
	public Button pauseBtn;
	int score;
	public int level;

	public GameObject pauseMenu;

	public Button restart;
	public Button resume;
	public Button quit;

	public AudioSource coinAudio;
	public AudioSource radarAudio;
	public AudioSource obstacleAudio;
	public AudioSource jumpAudio;

	public AudioSource mainAudio;
	public AudioSource otherAudio;

	// Use this for initialization
	void Start () {
		pauseMenu.SetActive (false);
		pause = false;
		mainAudio.Play();
		score = 0;
		level = 0;
		jump.GetComponent<Button>().onClick.AddListener(jumpFunc);
		pauseBtn.GetComponent<Button>().onClick.AddListener(pauseFunc);
		cameraBtn.GetComponent<Button>().onClick.AddListener(cameraFunc);
		restart.GetComponent<Button>().onClick.AddListener(restartFunc);
		resume.GetComponent<Button>().onClick.AddListener(resumeFunc);
		quit.GetComponent<Button>().onClick.AddListener(quitFunc);
	}

	void resumeFunc(){
		pause = false;
		Time.timeScale = 1;
		pauseMenu.SetActive (false);
		otherAudio.Stop ();
		mainAudio.Play();
	}

	void restartFunc(){
		SceneManager.LoadScene("main");
	}

	void quitFunc(){
		Application.Quit();
	}

	void pauseFunc(){
		Time.timeScale = 0;
		pause = true;
		pauseMenu.SetActive (true);
		mainAudio.Pause ();
		otherAudio.Play ();
	}

	void cameraFunc(){
		if (cam1.GetComponent<Camera> ().depth == 0) {
			cam1.GetComponent<Camera> ().depth = 1;
			cam2.GetComponent<Camera> ().depth = 0;
			cam2.GetComponent<AudioListener>().enabled = false;
			cam1.GetComponent<AudioListener>().enabled = true;
		}else{
			cam1.GetComponent<Camera> ().depth = 0;
			cam2.GetComponent<Camera> ().depth = 1;
			cam1.GetComponent<AudioListener>().enabled = false;
			cam2.GetComponent<AudioListener>().enabled = true;
		}
	}

	void jumpFunc(){
		if (!pause) {
			gameObject.transform.position = new Vector3(gameObject.transform.position.x,3,gameObject.transform.position.z);
			jumpAudio.Play ();
		}
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp(KeyCode.Escape))
		{
			if (Time.timeScale == 0) {
				pause = false;
				Time.timeScale = 1;
				pauseMenu.SetActive (false);
				otherAudio.Stop ();
				mainAudio.Play();
			} else {
				Time.timeScale = 0;
				pause = true;
				pauseMenu.SetActive (true);
				mainAudio.Pause ();
				otherAudio.Play ();
			}
		}

		if (Input.GetKeyUp(KeyCode.C))
		{
			if (cam1.GetComponent<Camera> ().depth == 0) {
				cam1.GetComponent<Camera> ().depth = 1;
				cam2.GetComponent<Camera> ().depth = 0;
			}else{
				cam1.GetComponent<Camera> ().depth = 0;
				cam2.GetComponent<Camera> ().depth = 1;
			}
		}

		float horTran = Input.GetAxis("Horizontal");
		horTran = horTran * 0.2f;
		if (!pause) {
			gameObject.transform.Translate (new Vector3 (horTran, 0, 0));
		}
		if (!pause) {
			transform.Translate ((Input.acceleration.x * 0.2f), 0, 0);
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			if (!pause) {
				jumpAudio.Play ();
				gameObject.transform.position = new Vector3(gameObject.transform.position.x,3,gameObject.transform.position.z);
			}
		}

		float verTran = (level+1)*0.25f;
		if (!pause) {
			gameObject.transform.Translate (new Vector3 (0, 0, verTran));
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("Coin")) {
			GameObject.Destroy(other.gameObject);
			score=score+10;
			t.text = "Score: " + (score);
			level = score / 50;
			coinAudio.Play();
		}
		if (other.gameObject.CompareTag("Radar")) {
			if (score > 50) {
				score = score - 50;
			} else {
				score = 0;
			}
			level = score / 50;
			t.text = "Score: " + (score);
			radarAudio.Play ();
		}
	}
	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.CompareTag("Obstacle")) {
			pause = true;
			StartCoroutine(DelayedGameOver());
		}
	}

	IEnumerator DelayedGameOver()
	{
		obstacleAudio.Play ();
		yield return new WaitForSeconds (obstacleAudio.clip.length);    
		SceneManager.LoadScene("GameOver");
	}

}
