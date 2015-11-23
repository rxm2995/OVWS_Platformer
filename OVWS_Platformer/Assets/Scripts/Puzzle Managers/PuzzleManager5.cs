using UnityEngine;
using System.Collections;

public class PuzzleManager5 : MonoBehaviour {

	public GameObject movingPlatform, switchOne, switchTwo, switchThree, switchFour, switchFive, switchSix;
	public Vector3 finalPlatformPos;
	
	private SwitchBehavior switchOneStatus, switchTwoStatus, switchThreeStatus, switchFourStatus, switchFiveStatus, switchSixStatus;
	
	// Use this for initialization
	void Start ()
	{
		switchOneStatus = switchOne.GetComponent<SwitchBehavior> ();
		switchTwoStatus = switchTwo.GetComponent<SwitchBehavior> ();
		switchThreeStatus = switchThree.GetComponent<SwitchBehavior> ();
		switchFourStatus = switchFour.GetComponent<SwitchBehavior> ();
		switchFiveStatus = switchFive.GetComponent<SwitchBehavior> ();
		switchSixStatus = switchSix.GetComponent<SwitchBehavior> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!switchOneStatus.puzzleSolved && switchOneStatus.activated && switchTwoStatus.activated
		    && switchThreeStatus.activated && switchFourStatus.activated
		    && switchFiveStatus.activated && switchSixStatus.activated)
		{
			switchOneStatus.solvePuzzle();
			switchTwoStatus.solvePuzzle();
			switchThreeStatus.solvePuzzle();
			switchFourStatus.solvePuzzle();
			switchFiveStatus.solvePuzzle();
			switchSixStatus.solvePuzzle();
			movingPlatform.transform.position = finalPlatformPos;
		}
	}
}
