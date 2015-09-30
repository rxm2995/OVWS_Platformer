using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float maxSpeed = 6f;
	public float maxForce = 3f;
	public float mass = 1f;

	public float jumpForce = 5f;
	public float jumpDecayRate = 0.8f;
	public float minYVelocity = 0.01f;

	public float minYValue = -100;
	
	protected bool onGround;
	protected Vector3 velocity;
	protected Vector3 acceleration;
	protected CharacterController charControl;

	private float yVelocity;
	private bool canAirJump = true;

	// Use this for initialization
	void Start ()
	{
		charControl = gameObject.GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 velocity = Vector3.zero;

		//Input handling
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow))
		{
			velocity.x += -1 * maxSpeed;// * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow))
		{
			velocity.x += maxSpeed;// * Time.deltaTime;
		}
		if ((onGround || canAirJump) && (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.Space)))
		{
			yVelocity = jumpForce;

			//This will be changed before the player actually leaves the ground, meaning double jumps should work fine.
			canAirJump = onGround;
		}

		//Jump arc handling
		if (yVelocity >= minYVelocity)
		{
			yVelocity *= jumpDecayRate;
			charControl.Move(new Vector3(0, yVelocity, 0));

		}
		//Normal movement handling
		onGround = charControl.SimpleMove (velocity);

		if (transform.position.y < minYValue)
		{
			//If we're here, the player must have fallen off the screen. Return them to start.
			transform.position = new Vector3(0, 0, 0);
		}
		
	}
} 
