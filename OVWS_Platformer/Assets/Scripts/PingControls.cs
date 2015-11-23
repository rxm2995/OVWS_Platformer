using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PingControls : NetworkBehaviour {

	public GameObject ping;
	//public GameObject rayCastNet;
	public Camera playerCamera;

	private bool cameraDragging;
	private Vector3 clickPosition;

	void Start () {
		/*
		if (isLocalPlayer) {
			Cursor.visible = false;
		}
		*/
	}

	void Update () {
		if (isLocalPlayer) {
		
			// Add Ping with RMB
			if (Input.GetMouseButtonDown (1)) {
				if (cameraDragging)
				{
					clickPosition = Input.mousePosition;
					Vector3 destination = playerCamera.ScreenPointToRay(clickPosition).GetPoint(10);

					CmdSpawnPing(destination);
				}
			}

			// Activate Camera Dragging with Tab
			if (Input.GetKeyDown(KeyCode.Tab))
			{
				cameraDragging = true;

				//disable player movement


				//move mouse to middle of screen
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}

			/*
			// Camera Dragging - move mouse to edges of screen to drag camera
			if (Input.mousePosition.x)
			{

			}
			*/
		}
	}

	[Command]
	void CmdSpawnPing(Vector3 destination){
		Instantiate(ping, new Vector3(destination.x, destination.y, -1.5f), new Quaternion(90,0,0,0));
		NetworkServer.Spawn (ping);
	}
}
