using UnityEngine;
using System.Collections;

public class PuzzleManager7 : MonoBehaviour {

	public GameObject movingPlatform1, movingPlatform2, switchOne, switchTwo, switchThree;
	Vector3 initialPos1, initialPos2;
	Vector3 downPos1, rightPos1;
	Vector3 upPos2;
	
	private SwitchBehavior switchOneStatus, switchTwoStatus, switchThreeStatus;
	
	// Use this for initialization
	void Start () {
		initialPos1 = movingPlatform1.transform.position;
		downPos1 = new Vector3(initialPos1.x, initialPos1.y - 32.0f, initialPos1.z);
		rightPos1 = new Vector3(initialPos1.x + 20.0f, initialPos1.y, initialPos1.z);
		initialPos2 = movingPlatform2.transform.position;
		upPos2 = new Vector3(initialPos2.x, initialPos2.y + 14.0f, initialPos2.z);
		switchOneStatus = switchOne.GetComponent<SwitchBehavior> ();
		switchTwoStatus = switchTwo.GetComponent<SwitchBehavior> ();
		switchThreeStatus = switchThree.GetComponent<SwitchBehavior> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(switchOneStatus.activated)
		{
			movingPlatform1.transform.position = Vector3.Lerp(movingPlatform1.transform.position, downPos1, Time.deltaTime);
		}
		else if(switchTwoStatus.activated)
		{
			movingPlatform1.transform.position = Vector3.Lerp(movingPlatform1.transform.position, rightPos1, Time.deltaTime);
		}
		else
		{
			movingPlatform1.transform.position = Vector3.Lerp(movingPlatform1.transform.position, initialPos1, Time.deltaTime);
		}

		if(switchThreeStatus.activated)
		{
			movingPlatform2.transform.position = Vector3.Lerp(movingPlatform2.transform.position, upPos2, Time.deltaTime);
		}
		else
		{
			movingPlatform2.transform.position = Vector3.Lerp(movingPlatform2.transform.position, initialPos2, Time.deltaTime);
		}
	}
}
