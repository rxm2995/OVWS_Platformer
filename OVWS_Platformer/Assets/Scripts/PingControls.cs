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

					if (isServer)
					{
						Instantiate(ping, new Vector3(destination.x, destination.y, -1.5f), new Quaternion(0, 0, 0, 0));
						//RpcSpawnPing(destination);
						NetworkServer.Spawn (ping);
					}
					else if (isClient) {
						Instantiate(ping, new Vector3(destination.x, destination.y, -1.5f), new Quaternion(0, 0, 0, 0));
						CmdSpawnPing(destination);
					}


					clientOnlySpawnPing(destination);
					//RpcSpawnPing(destination);
					CmdSpawnPing(destination);
				}
			}

			// Activate Camera Dragging with Tab
			if (Input.GetKeyDown(KeyCode.Tab))
			{
				if (!cameraDragging)
				{	
					cameraDragging = true;
					
					//disable player movement
					GetComponent<AcrobatPlayer>().maxSpeed = 0;
					GetComponent<AcrobatPlayer>().jumpForce = 0;
					GetComponent<StrongPlayer>().maxSpeed = 0;
					GetComponent<StrongPlayer>().jumpForce = 0;
					
					//move mouse to middle of screen
					Cursor.lockState = CursorLockMode.Locked;
					Cursor.lockState = CursorLockMode.None;
					Cursor.visible = true;
				}
				else
				{
					cameraDragging = false;
					
					//enable player movement
					GetComponent<AcrobatPlayer>().maxSpeed = 6;
					GetComponent<AcrobatPlayer>().jumpForce = 1;
					GetComponent<StrongPlayer>().maxSpeed = 6;
					GetComponent<StrongPlayer>().jumpForce = 1;
				}
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
		Instantiate(ping, new Vector3(destination.x, destination.y, -1.5f), new Quaternion(0, 0, 0, 0));
		//RpcSpawnPing(destination);
		//NetworkServer.Spawn (ping);
	}

	[ClientRpc]
	void RpcSpawnPing(Vector3 destination){
		Instantiate (ping, new Vector3 (destination.x, destination.y, -1.5f), new Quaternion (0, 0, 0, 0));
	}

	[ClientCallback]
	void clientOnlySpawnPing(Vector3 destination){
		Instantiate (ping, new Vector3 (destination.x, destination.y, -1.5f), new Quaternion (0, 0, 0, 0));
	}
}
