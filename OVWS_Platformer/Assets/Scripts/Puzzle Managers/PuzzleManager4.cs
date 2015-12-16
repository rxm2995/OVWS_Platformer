using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PuzzleManager4 : NetworkBehaviour
{
	[SerializeField]
	TextMesh tutorialText;
	ControlManager controlManager;

	public GameObject dropSwitch, floorSwitch;
	public GameObject togglePlatform, loweringPlatform, dropFloor;
	public Vector3 toggleUpPos, toggleDownPos, lowerEndPos, lowerEndScl;

	private SwitchBehavior dropBehavior, floorBehavior;
	private Vector3 lowerStartPos, lowerStartScl, lowerDirection, lowerScale;
	private bool toggleSwitchPressed;
	private float timeSinceButtonPress, secondsToFall;

	[SyncVar (hook="ToggleHook")]
	private bool toggledDown;

	// Use this for initialization
	void Start ()
	{
		controlManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ControlManager>();
		controlManager.GetControl(PlayerActions.Jump);
		tutorialText.text = "Feeling Stuck? \n" +
			"Try hitting " + controlManager.GetControl (PlayerActions.Ragequit);

		secondsToFall = 4;
		dropBehavior = dropSwitch.GetComponent<SwitchBehavior> ();
		floorBehavior = floorSwitch.GetComponent<SwitchBehavior> ();
		lowerStartPos = loweringPlatform.transform.position;
		lowerStartScl = loweringPlatform.transform.localScale;
		lowerDirection = lowerEndPos - lowerStartPos;
		lowerScale = lowerEndScl - lowerStartScl;
		lowerDirection *= 1 / secondsToFall;
		lowerScale *= 1 / secondsToFall;
		timeSinceButtonPress = 0;
		toggleSwitchPressed = false;

		ToggleHook(toggledDown);
	}
	
	// Update is called once per frame
	void Update ()
	{
		dropFloor.GetComponent<BoxCollider> ().enabled = floorBehavior.activated;
		dropFloor.GetComponent<MeshRenderer> ().enabled = floorBehavior.activated;

		if(!toggleSwitchPressed && dropBehavior.activated)
		{
			//This is the first frame the switch can be pressed.
			if(isServer)
			{
				setToggleDown(!toggledDown);
			}
		}

		toggleSwitchPressed = dropBehavior.activated;


		timeSinceButtonPress = Mathf.Min (secondsToFall, timeSinceButtonPress + Time.deltaTime);
		loweringPlatform.transform.position = lowerStartPos + lowerDirection * timeSinceButtonPress;
		loweringPlatform.transform.localScale = lowerStartScl + lowerScale * timeSinceButtonPress;
	}

	[Server]
	void setToggleDown(bool val)
	{
		toggledDown = val;
	}

	[Client]
	void ToggleHook(bool val)
	{
		toggledDown = val;
		
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
}
