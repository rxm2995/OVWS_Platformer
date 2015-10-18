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
	void Start ()
	{
		lerpRate = 100;
		if (isLocalPlayer)
		{
			gameObject.GetComponentInChildren<Camera>().enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isLocalPlayer)
		{
			LerpPosition ();
			transform.position = syncPos;
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
}
