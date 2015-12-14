using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ControlMenu : NetworkBehaviour {

	bool menuOn;
	private GameObject menuHolder;	

	void Start () {
		if (isLocalPlayer) {
			menuHolder = GameObject.Find("Control GO Holder");
			menuHolder.SetActive (false);
			menuOn = false;
		}
	}

	void Update () {
		if (isLocalPlayer) {
			if (menuOn && Input.GetKeyDown (KeyCode.P))
			{
				Debug.Log ("turning off");

				//disable control menu
				menuHolder.SetActive(false);
				menuOn = false;

				/**/ // THIS CAN BE REMOVED, idk if it's appropriate
				//enable player movement
				GetComponent<AcrobatPlayer>().maxSpeed = 6;
				GetComponent<AcrobatPlayer>().jumpForce = 1;
				GetComponent<StrongPlayer>().maxSpeed = 4;
				GetComponent<StrongPlayer>().jumpForce = 1.25f;
				/**/
			}
			else if (Input.GetKeyDown (KeyCode.P))
			{
				Debug.Log ("turning on");

				//enable control menu
				menuHolder.SetActive(true);
				menuOn = true;

				/**/ // THIS CAN BE REMOVED, idk if it's appropriate
				//disable player movement
				GetComponent<AcrobatPlayer>().maxSpeed = 0;
				GetComponent<AcrobatPlayer>().jumpForce = 0;
				GetComponent<StrongPlayer>().maxSpeed = 0;
				GetComponent<StrongPlayer>().jumpForce = 0;
				/**/
			}
		}
	}
}
