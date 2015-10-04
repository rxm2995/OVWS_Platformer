using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour {

	int count;
	GameObject[] playerReferences;

	void Start () {
		count = 0;
		playerReferences = GameObject.FindGameObjectsWithTag ("Player");
	}

	void Update () {
		// activates new scene when all players gather on this object
		if (count == 1) {
			Debug.Log("audhfiadusfhsuiof");
			Application.LoadLevel(1);
		}

	}

	void OnTriggerEnter(Collider other) {
		count++;
	}
	void OnTriggerExit(Collider other) {
		count--;
	}
}
