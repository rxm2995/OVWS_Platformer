using UnityEngine;
using System.Collections;

public class PuzzleManager2 : MonoBehaviour {

	public Vector3 doorInitialPosition;
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
			finalDoor.transform.position = new Vector3(doorInitialPosition.x, doorInitialPosition.y + 2, doorInitialPosition.z);
		}
		else
		{
			finalDoor.transform.position = doorInitialPosition;
		}
	}
}
