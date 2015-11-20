using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Ping : NetworkBehaviour {

	public Texture pingSprite;
	private Vector3 clickPosition;
	//change to data structure with push pull functionality for ping kill timer
	private Vector3[] pingLocations;
	private int currentPingIndex;

	void Start () {
		//pingSprite.enabled = false;
		pingLocations = new Vector3[10];
		currentPingIndex = 0;
	}

	void Update () {

		//isLocalPlayer LMB
		if (Input.GetMouseButtonDown (0)) {
			clickPosition = Input.mousePosition;

			// Check if clicked within minimap border
			if ((Input.mousePosition.x > (Screen.width * 0.75)) && (Input.mousePosition.x < Screen.width)) {
				if ((Input.mousePosition.y < (Screen.height * 0.25)) && (Input.mousePosition.y > 0)) {
					Debug.Log("CLICKED IN MINIMAP");
					//addPing();
				}
			}

			// Else, handle real world position conversion
			// x
		}

		displayPings ();
	}

	void addPing()
	{
		// Minimap Ping
		pingLocations [currentPingIndex] = Input.mousePosition;

		// In-World Ping
		// x

		currentPingIndex += 1;
	}

	void displayPings()
	{
		/*
		foreach (int pingPosition in pingLocations) {
			//Minimap Pings
			//GUI.DrawTexture(new Rect(pingPosition.x, pingPosition.y, 50, 50), pingSprite);
				//50 is placeholder for texture width and height

			//In-World Pings
				//Instantiate?
			//x
		}*/
	}
}
