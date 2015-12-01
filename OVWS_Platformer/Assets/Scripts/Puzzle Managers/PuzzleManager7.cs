using UnityEngine;
using System.Collections;

public class PuzzleManager7 : MonoBehaviour {

	public GameObject movingPlatform1, movingPlatform2, switchOne, switchTwo, switchThree;
	Vector3 initialPos1, initialPos2;
	Vector3 downPos1, rightPos1;
	Vector3 upPos2;
	Vector3 targetPos1, currentPos1;
	Vector3 targetPos2, currentPos2;
	
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
		currentPos1 = movingPlatform1.transform.position;
		currentPos2 = movingPlatform2.transform.position;
//		if(switchOneStatus.activated)
//		{
//			movingPlatform1.transform.position = Vector3.Lerp(movingPlatform1.transform.position, downPos1, Time.deltaTime);
//		}
//		else if(switchTwoStatus.activated)
//		{
//			movingPlatform1.transform.position = Vector3.Lerp(movingPlatform1.transform.position, rightPos1, Time.deltaTime);
//		}
//		else
//		{
//			movingPlatform1.transform.position = Vector3.Lerp(movingPlatform1.transform.position, initialPos1, Time.deltaTime);
//		}
//
//		if(switchThreeStatus.activated)
//		{
//			movingPlatform2.transform.position = Vector3.Lerp(movingPlatform2.transform.position, upPos2, Time.deltaTime);
//		}
//		else
//		{
//			movingPlatform2.transform.position = Vector3.Lerp(movingPlatform2.transform.position, initialPos2, Time.deltaTime);
//		}

		if(switchOneStatus.activated)
		{
			targetPos1 = downPos1;
		}
		else if(switchTwoStatus.activated)
		{
			targetPos1 = rightPos1;
		}
		else
		{
			targetPos1 = initialPos1;
		}

		if(switchThreeStatus.activated)
		{
			targetPos2 = upPos2;
		}
		else
		{
			targetPos2 = initialPos2;
		}


		float lerpRate1 = 1.0f - (Mathf.Abs(targetPos1.magnitude - currentPos1.magnitude)/50.0f);
		movingPlatform1.transform.position = Vector3.Lerp(currentPos1, targetPos1, Time.deltaTime * lerpRate1 * 2.0f);

		float lerpRate2 = 1.0f - (Mathf.Abs(targetPos2.magnitude - currentPos2.magnitude)/50.0f);
		movingPlatform2.transform.position = Vector3.Lerp(currentPos2, targetPos2, Time.deltaTime * lerpRate2 * 2.0f);
	}
}
