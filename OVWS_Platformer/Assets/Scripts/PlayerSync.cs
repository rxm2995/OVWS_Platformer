using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSync : NetworkBehaviour
{
	public float minDeltaBeforePosSync = 0.1f;

	[SyncVar]
	private Vector3 syncPos;

	// Use this for initialization
	void Start ()
	{
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
		if (isLocalPlayer && (transform.position-syncPos).magnitude >= minDeltaBeforePosSync)
		{
			CmdSyncPosition(transform.position);
		}
	}

	[Command]
	void CmdSyncPosition(Vector3 posToTransmit)
	{
		syncPos = posToTransmit;
	}
}
