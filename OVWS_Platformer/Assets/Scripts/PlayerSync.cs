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
	private bool holdStateNeedsUpdating;

	void Start ()
	{
		holdStateNeedsUpdating = false;
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
			//LerpPosition ();
			transform.position = syncPos;
			
			if(holdStateNeedsUpdating)
			{
				GameObject p1 = GameObject.FindGameObjectsWithTag("Player")[0];
				if(p1.transform.parent == null)
				{
					p1.transform.parent = gameObject.transform;
					p1.transform.localPosition = new Vector3(0, 1.1f, 0);
					p1.GetComponent<CharacterController>().enabled = false;
				}
				else
				{
					p1.transform.parent = null;
					p1.GetComponent<CharacterController>().enabled = true;
				}
				
				holdStateNeedsUpdating = false;
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

	public void TestFunctionPleaseIgnore()
	{
		
		CmdToggleHold();
		//Change this if p1 ever gets the ability to break out of a hold themselves!
		CharacterController p1Control = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<CharacterController>();
		p1Control.enabled = !p1Control.enabled;
	}
	
	[Command]
	void CmdToggleHold()
	{
		holdStateNeedsUpdating = true;
	}
}
