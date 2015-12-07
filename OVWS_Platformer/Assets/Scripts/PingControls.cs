using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PingControls : NetworkBehaviour {

	public GameObject ping;
	public Camera playerCamera;
	private ControlManager controls;
	private bool cameraDragging;
	private Vector3 clickPosition;
    private Vector3 playerCameraStartPos;
	public int pingCount;
	private int pingLimit;
	private bool pingOverload;
	private GUIStyle overloadStyle, pingHUD;
	private float overloadStartTime;
	private float overloadDuration;

    [Command]
    void CmdSpawnPing(Vector3 destination)
    {
        Instantiate(ping, new Vector3(destination.x, destination.y, 1.0f), Quaternion.identity);
    }

    [ClientRpc]
    void RpcSpawnPing(Vector3 destination)
    {
        Instantiate(ping, new Vector3(destination.x, destination.y, 1.0f), Quaternion.identity);
    }

	void Start () {
		cameraDragging = false;
		controls = GameObject.Find("Game Manager").GetComponent<ControlManager>();
		pingCount = 0;
		pingLimit = 5;
		pingOverload = false;
		overloadDuration = 6f;

		overloadStyle = new GUIStyle ();
		overloadStyle.normal.textColor = new Color (210, 0, 0);
		overloadStyle.fontStyle = FontStyle.Bold;
		overloadStyle.fontSize = 40;
		pingHUD = new GUIStyle ();
		pingHUD.normal.textColor = new Color (0, 200, 255);
		pingHUD.fontStyle = FontStyle.Bold;
		pingHUD.fontSize = 25;


	}

	void Update () {
		if (isLocalPlayer) {

			// Manage Ping Overload
			if (pingOverload) {
				if (Time.time > overloadStartTime + overloadDuration) {
					pingOverload = false;
					pingCount = 0;
				}
			}

			// Add Ping with RMB
			if (Input.GetKeyDown (controls.GetControl(PlayerActions.SpawnPing))) {
				if (cameraDragging)
				{
					if (!pingOverload) 
					{
						pingCount ++;
						if (pingCount > pingLimit)
						{
							pingOverload = true; 
							overloadStartTime = Time.time;
						}
						else
						{
							clickPosition = Input.mousePosition;
							Vector3 destination = playerCamera.ScreenPointToRay(clickPosition).GetPoint(10);
							
							if (isServer)
							{
								RpcSpawnPing(destination);
							}
							else if (isClient) {
								Instantiate(ping, new Vector3(destination.x, destination.y, 1.0f), Quaternion.identity);
								CmdSpawnPing(destination);
							}
						}
					}
				}
			}

			// Activate Camera Dragging with Tab
			if (Input.GetKeyDown(controls.GetControl(PlayerActions.ToggleCamMove)))
			{
                // ENABLE
				if (!cameraDragging)
				{
					Debug.Log("Enabling Camera Drag");
					cameraDragging = true;

                    //disable player movement
                    //GetComponent<CharacterController>().enabled = false;

					GetComponent<AcrobatPlayer>().maxSpeed = 0;
					GetComponent<AcrobatPlayer>().jumpForce = 0;
					GetComponent<StrongPlayer>().maxSpeed = 0;
					GetComponent<StrongPlayer>().jumpForce = 0;
					
					//move mouse to middle of screen
					Cursor.lockState = CursorLockMode.Locked;
					Cursor.lockState = CursorLockMode.None;
					Cursor.visible = true;

                    //record player camera start position
                   // playerCameraStartPos = new Vector3(playerCamera.transform.position.x, playerCamera.transform.position.y, playerCamera.transform.position.z);
				}
                // DISABLE
				else
				{
					Debug.Log("Disabling Camera Drag");

					cameraDragging = false;

                    //enable player movement
                    //GetComponent<CharacterController>().enabled = true;

                    GetComponent<AcrobatPlayer>().maxSpeed = 6;
					GetComponent<AcrobatPlayer>().jumpForce = 1;
					GetComponent<StrongPlayer>().maxSpeed = 4;
					GetComponent<StrongPlayer>().jumpForce = 1.25f;

                    //reset player camera position			//CONSIDER CAMERA PAN BACK UPON RESET ----------------------------------------------
                    //playerCamera.transform.position = playerCameraStartPos;
                    playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    playerCamera.transform.localPosition = new Vector3(0, 0, -10);
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

	void OnGUI() {
		if (pingOverload) {
			GUI.Label (new Rect (50, (Screen.height - 140), 50, 20), "PING OVERLOAD", overloadStyle);
		}
		if (cameraDragging) {
			GUI.Label (new Rect (50, (Screen.height - 80), 50, 20), "PING AND CAMERA DRAG ENABLED", pingHUD);
		}
	}
}
