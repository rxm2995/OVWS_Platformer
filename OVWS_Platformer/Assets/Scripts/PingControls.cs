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
		if (Input.GetMouseButtonDown (0)) {
			clickPosition = Input.mousePosition;
			if (Input.GetKey(KeyCode.Space))
			{
				addPing();
			}

			/*
			// Check if clicked within minimap border
			if ((Input.mousePosition.x > (Screen.width * 0.75)) && (Input.mousePosition.x < Screen.width)) {
				if ((Input.mousePosition.y < (Screen.height * 0.25)) && (Input.mousePosition.y > 0)) {
					Debug.Log("CLICKED IN MINIMAP");
					//addPing();
				}
			}
			*/
		}

		displayPings ();
	}

	void addPing()
	{
		pingLocations.Add (new Vector3(clickPosition.x, clickPosition.y, 0));
		currentPingIndex += 1;
	}

	void displayPings()
	{
		foreach (Vector3 pingPosition in pingLocations) {
			Instantiate(ping, pingPosition, new Quaternion(0,0,0,0));
			//x
		}
	}
}
