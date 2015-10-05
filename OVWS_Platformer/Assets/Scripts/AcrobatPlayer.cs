using UnityEngine;
using System.Collections;

public class AcrobatPlayer : Player
{
	private bool canAirJump = true;

	public override void Start()
	{
		//First player in gets Acrobat, everyone after gets Strong.
		if(GameObject.FindGameObjectsWithTag("Player").Length > 1)
		{
			gameObject.GetComponent<StrongPlayer>().enabled = true;
			enabled = false;
		}
		base.Start ();
	}
	
	// Update is called once per frame
	public override void Update ()
	{
		canAirJump = onGround || canAirJump;
		if (!onGround && canAirJump && (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.Space)))
		{
			yVelocity = jumpForce/jumpDecayRate;
			canAirJump = false;
		}
		
		base.Update();
	}
}
