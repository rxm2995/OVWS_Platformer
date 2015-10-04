using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public float maxSpeed = 6f;
	public float maxForce = 3f;
	public float mass = 1f;
	
	public float jumpForce = 1.5f;
	public float jumpDecayRate = 0.8f;
	
	private float minYValue = -100;
	
	protected bool onGround;
	protected Vector3 velocity;
	protected Vector3 acceleration;
	protected CharacterController charControl;
	
	protected float yVelocity;
	private bool rageQuit = false;
	
	// Use this for initialization
	public virtual void Start ()
	{
		charControl = gameObject.GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	public virtual void Update ()
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
		if(Input.GetKeyDown(KeyCode.K) || rageQuit)
		{
			rageQuit = true;
			velocity.z -= 20;
		}
		if (onGround && (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.Space)))
		{
			yVelocity = jumpForce;
		}
		
		//Jump arc handling
		yVelocity *= jumpDecayRate;
		
		//Normal movement handling
		velocity *= Time.deltaTime;
		velocity.y = yVelocity;
		charControl.Move(velocity);
		onGround = charControl.SimpleMove (Vector3.zero);
		
		//Locking movement on Z axis
		transform.position = new Vector3(transform.position.x, transform.position.y, rageQuit ? transform.position.z : 0);
		
		if (transform.position.y < minYValue)
		{
			rageQuit = false;
			//If we're here, the player must have fallen off the screen. Return them to start.
			transform.position = new Vector3(0, 0, 0);
		}
		
	}
} 
