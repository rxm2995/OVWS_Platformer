using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class StrongPlayer : Player
{
	public float sprintModifier = 2.0f;
	[SerializeField]
	private TrailRenderer trail;
	// Update is called once per frame
	public override void Update ()
	{
		
		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			trail.time = 1.5f;
			maxSpeed *= sprintModifier;
			gameObject.GetComponent<PlayerSync>().CmdToggleTrail();

		}
		if(Input.GetKeyUp(KeyCode.LeftShift))
		{
			trail.time = 0;
			maxSpeed /= sprintModifier;
			gameObject.GetComponent<PlayerSync>().CmdToggleTrail();
		}
		if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.E))
		{
			GameObject p1 = GameObject.FindGameObjectsWithTag("Player")[0];
			if((p1.transform.position-transform.position).sqrMagnitude < 9)
			{
				gameObject.GetComponent<PlayerSync>().TestFunctionPleaseIgnore();
			}
		}
		
		base.Update();
	}
}
