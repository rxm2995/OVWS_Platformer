using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class StrongPlayer : Player
{
	public float sprintModifier = 2.0f;
	private bool isHoldingPlayer = false;

	// Update is called once per frame
	public override void Update ()
	{
		
		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			maxSpeed *= sprintModifier;
		}
		if(Input.GetKeyUp(KeyCode.LeftShift))
		{
			maxSpeed /= sprintModifier;
		}
		if(Input.GetKeyDown(KeyCode.Z))
		{
			GameObject p1 = GameObject.FindGameObjectsWithTag("Player")[0];
			if((p1.transform.position-transform.position).sqrMagnitude < 9)
			{
				isHoldingPlayer = !isHoldingPlayer;
				gameObject.GetComponent<PlayerSync>().CmdSetTestVar((isHoldingPlayer ? 1 : -1));
			}
		}
		
		base.Update();
	}
}
