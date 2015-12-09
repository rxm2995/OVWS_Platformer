using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class StrongPlayer : Player
{
	public float speedModifier = 1.0f;
	public float maxSpeedModifier = 5.0f;
	public float minSpeedModifier = 1.0f;
	//public float sprintModifier = 2.0f;
	[SerializeField]
	private TrailRenderer trail;
	// Update is called once per frame
	public override void Update ()
	{
		
		if(Input.GetKeyDown(controls.GetControl(PlayerActions.Sprint)))
		{
			trail.time = 1.5f;
			//maxSpeed *= sprintModifier;

			gameObject.GetComponent<PlayerSync>().CmdToggleTrail();

		}
		else if(Input.GetKey(controls.GetControl(PlayerActions.Sprint)))
		{
			speedModifier += Time.deltaTime * 2;
			if(speedModifier > maxSpeedModifier)
				speedModifier = maxSpeedModifier;
		}
		else if(Input.GetKeyUp(controls.GetControl(PlayerActions.Sprint)))
		{
			trail.time = 0;
			//maxSpeed /= sprintModifier;

			gameObject.GetComponent<PlayerSync>().CmdToggleTrail();
		}
		else
		{
			speedModifier -= Time.deltaTime * 2;
			if(speedModifier < minSpeedModifier)
				speedModifier = minSpeedModifier;
		}

		if(Input.GetKeyDown(controls.GetControl(PlayerActions.Hold)))
		{
			GameObject p1 = GameObject.FindGameObjectsWithTag("Player")[0];
			if((p1.transform.position-transform.position).sqrMagnitude < 9)
			{
				gameObject.GetComponent<PlayerSync>().TestFunctionPleaseIgnore();
			}
		}

		maxSpeed = 6 * speedModifier;
		base.Update();
	}
}
