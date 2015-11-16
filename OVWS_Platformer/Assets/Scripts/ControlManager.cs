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
	private KeyCode[] controlCodes;

	// Use this for initialization
	void Start ()
	{
		KeyCode[] tempCodes = {KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.LeftShift, KeyCode.Z, KeyCode.K};
		controlCodes = tempCodes;
		DontDestroyOnLoad(gameObject);
	}

	public KeyCode GetControl(PlayerActions index)
	{
		return controlCodes[(int)index];
	}

	public void SetControl(PlayerActions index, KeyCode newVal)
	{
		controlCodes[(int)index] = newVal;
	}

	public void SetWASDControls()
	{
		KeyCode[] tempCodes = {KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.LeftShift, KeyCode.F, KeyCode.K};
		controlCodes = tempCodes;
	}

	public void SetArrowControls()
	{
		KeyCode[] tempCodes = {KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.LeftShift, KeyCode.Z, KeyCode.K};
		controlCodes = tempCodes;
	}
}
