using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class LevelEnd : NetworkBehaviour
{
	public string nextLevel;

	NetworkManager netManager;
	int playersAtEnd;
	GameObject[] playerReferences;

	void Start () {
		playersAtEnd = 0;
		playerReferences = GameObject.FindGameObjectsWithTag ("Player");
		netManager = GameObject.FindGameObjectWithTag("NetManager").GetComponent<NetworkManager>();
	}

	void Update () {
		// activates new scene when all players gather on this object
		if (playersAtEnd == 2)
		{
			Debug.Log("audhfiadusfhsuiof");
			//Application.LoadLevel(1);
			netManager.ServerChangeScene(nextLevel);
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Player"))
		{
			playersAtEnd++;
		}
	}
	void OnTriggerExit(Collider other) {
		if(other.gameObject.CompareTag("Player"))
		{
			playersAtEnd--;
		}
	}
}
