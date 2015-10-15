using UnityEngine;
using System.Collections;

public class PuzzleManager : MonoBehaviour {

	public GameObject finalDoor;
	public GameObject switchOne;
	public GameObject switchTwo;

	private SwitchBehavior switchOneStatus, switchTwoStatus;

	// Use this for initialization
	void Start ()
	{
		switchOneStatus = switchOne.GetComponent<SwitchBehavior> ();
		switchTwoStatus = switchTwo.GetComponent<SwitchBehavior> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (switchOneStatus.activated && switchTwoStatus.activated)
		{
			switchOneStatus.solvePuzzle();
			switchTwoStatus.solvePuzzle();
			finalDoor.transform.position += new Vector3(0, 2, 0);
		}
	}
}
