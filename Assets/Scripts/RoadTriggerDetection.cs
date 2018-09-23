using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTriggerDetection : MonoBehaviour {

	public GameObject road1;
	public GameObject road2;

	public GameObject obstacle;
	public GameObject radar;
	public GameObject coin;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		BoxCollider boxCollider1  = road1.GetComponent<BoxCollider>();
		BoxCollider boxCollider2  = road2.GetComponent<BoxCollider>();
		if (boxCollider1.isTrigger) {
			boxCollider1.isTrigger = false;
			boxCollider2.isTrigger = true;
			road2.transform.Translate(new Vector3(0,0,200));
			respawn (road2);
		} else {
			boxCollider1.isTrigger = true;
			boxCollider2.isTrigger = false;
			road1.transform.Translate(new Vector3(0,0,200));
			respawn (road1);
		}
	}

	void respawn(GameObject road){
		for(int i=2;i<road.transform.childCount;i++){
			Destroy (road.transform.GetChild (i).gameObject);
		}
		List<int> xPossibleValues = new List<int>(new int[] { -3, 0, 3});
		List<int> zPossibleValues = new List<int>(new int[] { -40,-20, 0, 20,40});
		System.Random randomX = new System.Random();
		System.Random randomZ = new System.Random();
		for (int j = 0; j < 3; j++) {
			int randomIndexForX = randomX.Next(xPossibleValues.Count);
			int randomIndexForZ = randomZ.Next(zPossibleValues.Count);
			int zChange = zPossibleValues[randomIndexForZ];
			zPossibleValues.RemoveAt(randomIndexForZ);
			GameObject newObstacle = Instantiate(obstacle, new Vector3(xPossibleValues[randomIndexForX], 2.5f, road.transform.position.z+zChange), Quaternion.identity);
			newObstacle.transform.parent = road.transform;
		}
		for (int k = 0; k < 2; k++) {
			int randomIndexForZ = randomZ.Next(zPossibleValues.Count);
			int zChange = zPossibleValues[randomIndexForZ];
			zPossibleValues.RemoveAt(randomIndexForZ);
			GameObject newRadar = Instantiate(radar, new Vector3(0, 0.5f, road.transform.position.z+zChange), Quaternion.identity);
			newRadar.transform.Rotate (new Vector3(0,0,90));
			newRadar.transform.parent = road.transform;
		}
		zPossibleValues = new List<int>(new int[] { -45,-30,-15,-5, 5, 15,30,45});
		for (int l = 0; l < 5; l++) {
			int randomIndexForX = randomX.Next(xPossibleValues.Count);
			int randomIndexForZ = randomZ.Next(zPossibleValues.Count);
			int zChange = zPossibleValues[randomIndexForZ];
			zPossibleValues.RemoveAt(randomIndexForZ);
			GameObject newCoin = Instantiate(coin, new Vector3(xPossibleValues[randomIndexForX], 1, road.transform.position.z+zChange), Quaternion.identity);
			newCoin.transform.Rotate (new Vector3(90,0,0));
			newCoin.transform.parent = road.transform;
		}
	}

}
