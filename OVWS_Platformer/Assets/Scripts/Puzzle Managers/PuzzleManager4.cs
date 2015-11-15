using UnityEngine;
using System.Collections;

public class PuzzleManager4 : MonoBehaviour
{
	public GameObject dropSwitch, floorSwitch;
	public GameObject togglePlatform, loweringPlatform, dropFloor;
	public Vector3 toggleUpPos, toggleDownPos, lowerEndPos, lowerEndScl;

	private SwitchBehavior dropBehavior, floorBehavior;
	private Vector3 lowerStartPos, lowerStartScl, lowerDirection, lowerScale;
	private bool toggledDown, toggleSwitchPressed;
	private float timeSinceButtonPress, secondsToFall;

	// Use this for initialization
	void Start ()
	{
		secondsToFall = 4;
		dropBehavior = dropSwitch.GetComponent<SwitchBehavior> ();
		floorBehavior = floorSwitch.GetComponent<SwitchBehavior> ();
		lowerStartPos = loweringPlatform.transform.position;
		lowerStartScl = loweringPlatform.transform.localScale;
		lowerDirection = lowerEndPos - lowerStartPos;
		lowerScale = lowerEndScl - lowerStartScl;
		lowerDirection *= 1 / secondsToFall;
		lowerScale *= 1 / secondsToFall;
		toggledDown = false;
		timeSinceButtonPress = 0;
		toggleSwitchPressed = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		dropFloor.GetComponent<BoxCollider> ().enabled = floorBehavior.activated;
		dropFloor.GetComponent<MeshRenderer> ().enabled = floorBehavior.activated;

		if(!toggleSwitchPressed && dropBehavior.activated)
		{
			//This is the first frame the switch can be pressed.
			toggledDown = !toggledDown;
			if(toggledDown)
			{
				togglePlatform.transform.position = toggleDownPos;
			}
			else
			{
				timeSinceButtonPress = 0;
				loweringPlatform.transform.position = lowerStartPos;
				loweringPlatform.transform.localScale = lowerStartScl;
				togglePlatform.transform.position = toggleUpPos;
			}
		}

		toggleSwitchPressed = dropBehavior.activated;


		timeSinceButtonPress = Mathf.Min (secondsToFall, timeSinceButtonPress + Time.deltaTime);
		loweringPlatform.transform.position = lowerStartPos + lowerDirection * timeSinceButtonPress;
		loweringPlatform.transform.localScale = lowerStartScl + lowerScale * timeSinceButtonPress;
	}
}
