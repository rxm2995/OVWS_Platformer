using UnityEngine;
using System.Collections;

public class StrongPlayer : Player
{
	public float sprintModifier = 2.0f;
	
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
		
		base.Update();
	}
}
