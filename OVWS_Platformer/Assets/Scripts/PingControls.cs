using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PingControls : NetworkBehaviour {

	public GameObject ping;
	private Vector3 clickPosition;
	private ArrayList pingLocations;
	private int currentPingIndex;

	void Start () {
		pingLocations = new ArrayList();
		currentPingIndex = 0;
	}

	void Update () {

		//isLocalPlayer LMB
		if (isLocalPlayer) {
			if (Input.GetMouseButtonDown (0)) {
				clickPosition = Input.mousePosition;

				Instantiate(ping, new Vector3(0, 0, 0), new Quaternion(0,0,0,0));
				Debug.Log("ping added");
			}
		}
	}
}
