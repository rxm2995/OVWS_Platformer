using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSync : NetworkBehaviour
{
	public float minDeltaBeforePosSync = 0.1f;

	private float lerpRate;

	[SyncVar]
	private Vector3 syncPos;
	// Use this for initialization

	[SyncVar]
	private int testVariablePleaseIgnore;

	void Start ()
	{
		testVariablePleaseIgnore = 0;
		lerpRate = 100;
		if (isLocalPlayer)
		{
			gameObject.GetComponentInChildren<Camera>().enabled = true;
		}
		else
		{
			gameObject.GetComponent<AcrobatPlayer>().enabled = false;
			gameObject.GetComponent<StrongPlayer>().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isLocalPlayer)
		{
			LerpPosition ();
			transform.position = syncPos;
			if(testVariablePleaseIgnore == 1)
			{
				GameObject p1 = GameObject.FindGameObjectsWithTag("Player")[0];
				p1.transform.parent = gameObject.transform;
				p1.transform.localPosition = new Vector3(0, 1.1f, 0);
				p1.GetComponent<CharacterController>().enabled = false;
				testVariablePleaseIgnore = 0;
			}
			else if(testVariablePleaseIgnore == -1)
			{
				GameObject p1 = GameObject.FindGameObjectsWithTag("Player")[0];
				p1.transform.parent = null;
				p1.GetComponent<CharacterController>().enabled = true;
				testVariablePleaseIgnore = 0;
			}
		}
	}

	void FixedUpdate()
	{
		if (isLocalPlayer)
		{
			TransmitPosition();
		}
	}

	[Client]
	void TransmitPosition()
	{
		if (isLocalPlayer && (transform.position-syncPos).sqrMagnitude >= minDeltaBeforePosSync*minDeltaBeforePosSync)
		{
			CmdSyncPosition(transform.position);
		}
	}

	[Command]
	void CmdSyncPosition(Vector3 posToTransmit)
	{
		syncPos = posToTransmit;
	}

	void LerpPosition ()
	{
		transform.position = Vector3.Lerp (transform.position, syncPos, Time.deltaTime * lerpRate);
		
	}

	[Command]
	public void CmdSetTestVar(int val)
	{
		testVariablePleaseIgnore = val;
	}
}
