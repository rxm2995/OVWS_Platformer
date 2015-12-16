using UnityEngine;
using System.Collections;

public class PuzzleManager2 : MonoBehaviour {

	public Vector3 doorInitialPosition;
	public Vector3 doorFinalPosition;
	public GameObject finalDoor;
	public GameObject switchOne;
	public GameObject switchTwo;
	
	private SwitchBehavior switchOneStatus, switchTwoStatus;
	
	// Use this for initialization
	void Start ()
	{
		doorInitialPosition = finalDoor.transform.position;
		switchOneStatus = switchOne.GetComponent<SwitchBehavior> ();
		switchTwoStatus = switchTwo.GetComponent<SwitchBehavior> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(switchTwoStatus.activated || switchOneStatus.activated)
		{
			finalDoor.transform.position = Vector3.MoveTowards(finalDoor.transform.position, doorFinalPosition, Time.deltaTime);
		}
		else
		{
			finalDoor.transform.position = Vector3.MoveTowards(finalDoor.transform.position, doorInitialPosition, Time.deltaTime);

		}

		if (finalDoor.transform.position == doorFinalPosition || finalDoor.transform.position == doorInitialPosition) {

		}
	}
}
