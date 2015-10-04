using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class LevelEnd : NetworkBehaviour
{
	NetworkManager netManager;
	int count;
	GameObject[] playerReferences;

	void Start () {
		count = 0;
		playerReferences = GameObject.FindGameObjectsWithTag ("Player");
		netManager = GameObject.FindGameObjectWithTag("NetManager").GetComponent<NetworkManager>();
	}

	void Update () {
		// activates new scene when all players gather on this object
		if (count == 1) {
			Debug.Log("audhfiadusfhsuiof");
			//Application.LoadLevel(1);
			netManager.ServerChangeScene("testScene2");
		}
	}

	void OnTriggerEnter(Collider other) {
		count++;
	}
	void OnTriggerExit(Collider other) {
		count--;
	}
}
