using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum PlayerActions
{
	MoveLeft,
	MoveRight,
	Jump,
	Sprint,
	Hold,
	Ragequit
};

public class ControlManager : MonoBehaviour
{
	private KeyCode[] controlCodes = {KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.LeftShift, KeyCode.Z, KeyCode.K};

	// Use this for initialization
	void Start ()
	{
		if(GameObject.FindGameObjectsWithTag(gameObject.tag).Length > 1)
		{
			//Duplicate prevention
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
	}

	public KeyCode GetControl(PlayerActions actionToGet)
	{
		return controlCodes[(int)actionToGet];
	}

	public void SetControl(PlayerActions actionToSet, KeyCode newVal)
	{
		controlCodes[(int)actionToSet] = newVal;
	}

	public void SetWASDControls()
	{
		KeyCode[] tempCodes = {KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.LeftShift, KeyCode.F, KeyCode.K};
		controlCodes = tempCodes;

		ButtonThing[] buttonsToUpdate = GameObject.Find("Button Holder").GetComponentsInChildren<ButtonThing>();
		foreach(ButtonThing bt in buttonsToUpdate)
		{
			bt.UpdateText();
		}
	}

	public void SetArrowControls()
	{
		KeyCode[] tempCodes = {KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.LeftShift, KeyCode.Z, KeyCode.K};
		controlCodes = tempCodes;

		ButtonThing[] buttonsToUpdate = GameObject.Find("Button Holder").GetComponentsInChildren<ButtonThing>();
		foreach(ButtonThing bt in buttonsToUpdate)
		{
			bt.UpdateText();
		}
	}
}
