using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PingControls : NetworkBehaviour {

	public GameObject ping;
	public Camera playerCamera;
	private bool cameraDragging;
	private Vector3 clickPosition;
    private Vector3 playerCameraStartPos;

    [Command]
    void CmdSpawnPing(Vector3 destination)
    {
        Instantiate(ping, new Vector3(destination.x, destination.y, -1.5f), Quaternion.identity);
    }

    [ClientRpc]
    void RpcSpawnPing(Vector3 destination)
    {
        Instantiate(ping, new Vector3(destination.x, destination.y, -1.5f), Quaternion.identity);
    }

	void Start () {
		cameraDragging = false;
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
                        RpcSpawnPing(destination);
					}
					else if (isClient) {
						Instantiate(ping, new Vector3(destination.x, destination.y, -1.5f), Quaternion.identity);
						CmdSpawnPing(destination);
					}
				}
			}

			// Activate Camera Dragging with Tab
			if (Input.GetKeyDown(KeyCode.Tab))
			{
                // ENABLE
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

                    //record player camera start position
                    playerCameraStartPos = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y, playerCamera.transform.position.z);
				}
                // DISABLE
				else
				{
					cameraDragging = false;
					
					//enable player movement
					GetComponent<AcrobatPlayer>().maxSpeed = 6;
					GetComponent<AcrobatPlayer>().jumpForce = 1;
					GetComponent<StrongPlayer>().maxSpeed = 6;
					GetComponent<StrongPlayer>().jumpForce = 1;

                    //reset player camera position
                    playerCamera.transform.position = playerCameraStartPos;
				}
			}


            // Camera Dragging - move mouse to edges of screen to drag camera
            if (cameraDragging)
            {
                Vector3 destination = new Vector3(0f, 0f, 0f);
                if (Input.mousePosition.x > Screen.width - (Screen.width * 0.2))
                {
                    destination.Set(destination.x + .08f, destination.y, 0f);
                }
                else if (Input.mousePosition.x < 0 + (Screen.width * 0.2))
                {
                    destination.Set(destination.x - .08f, destination.y, 0f);
                }
                if (Input.mousePosition.y < (0 + (Screen.width * 0.2)))
                {
                    destination.Set(destination.x, destination.y - .08f, 0f);
                }
                else if (Input.mousePosition.y > (Screen.height - (Screen.height * 0.2)))
                {
                    destination.Set(destination.x, destination.y + .08f, 0f);
                }
                playerCamera.transform.Translate(destination.x, destination.y, 0f);
            }
            
		}
	}
}
