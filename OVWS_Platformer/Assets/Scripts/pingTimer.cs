using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class pingTimer : NetworkBehaviour {

	private float startTime;
	private float duration = 4.5f;

	private GameObject[] bothPlayers;
	void Start () {
		startTime = Time.time;
		bothPlayers = GameObject.FindGameObjectsWithTag ("Player");
	}

	void Update () {
		if (Time.time > startTime + duration) {
			Destroy(gameObject);
			foreach (GameObject player in bothPlayers) {
				if (player.GetComponent<AcrobatPlayer>().enabled || player.GetComponent<StrongPlayer>().enabled)
				{
					player.GetComponent<PingControls>().pingCount--;
				}
			}
		}
	}
}
