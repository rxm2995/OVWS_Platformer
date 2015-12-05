using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerSync : NetworkBehaviour
{
	public Vector3 localVelocity;

	public float minDeltaBeforePosSync = 0.1f;
	private float minDistBeforeListPurge = 0.01f;

	private float lerpRate;

	private List<Vector3> syncPosList = new List<Vector3>();

	[SyncVar (hook = "clientSyncPos")]
	private Vector3 syncPos;
	// Use this for initialization

	[SyncVar]
	private Vector3 syncVel;

	[SyncVar]
	private bool holdStateNeedsUpdating;

	[SerializeField]
	private TrailRenderer tr;

	void Start ()
	{
		tr.enabled = true;
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
			//transform.position = syncPos;
			transform.position += syncVel*Time.deltaTime;
			
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
		if (isLocalPlayer)// && (transform.position-syncPos).sqrMagnitude >= minDeltaBeforePosSync*minDeltaBeforePosSync )
		{
			CmdSyncPosition(transform.position);
		}
//		else if(!isLocalPlayer && syncPosList.
	}

	[Command]
	void CmdSyncPosition(Vector3 posToTransmit)
	{
		syncPos = posToTransmit;
		syncVel = localVelocity;
	}

	[Client]
	void clientSyncPos(Vector3 posToSync)
	{
		//Rejected variable name: twentySevenEightByTenColourGlossyPicturesWithCirclesAndArrowsAndAParagraphOnTheBackOfEachOneExplainingWhatEachOneWasToBeUsedAsEvidenceAgainstUs
		syncPos = posToSync;
		if(!isLocalPlayer)
		{
			transform.position = syncPos;
		}
		syncPosList.Add(syncPos);
	}

	void LerpPosition ()
	{
		if (syncPosList.Count > 0)
		{
			transform.position = Vector3.Lerp (transform.position, syncPosList [0], 
			                                   Time.deltaTime * lerpRate);
			//if we're getting really close to that point, delete it
			if (Vector3.Distance (transform.position, syncPosList [0]) < minDistBeforeListPurge)
			{
				syncPosList.RemoveAt (0);
			}
			
			// if we don't have so many in the list, lerp faster
//			if (syncPosList.Count > 10) {
//				lerpRate = fasterLerpRate;
//			} else {
//				lerpRate = normalLerpRate;
//			}
		}
		
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

	[Command]
	public void CmdToggleTrail()
	{
		if(tr.time == 0)
		{
			tr.time = 1.5f;
		}
		else
		{
			tr.time = 0;
		}
	}
}
