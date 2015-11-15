using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : MonoBehaviour {
	[SerializeField]
	protected Camera localCam;
	[SerializeField]
	protected float maxFOV;
	[SerializeField]
	protected float minFOV;
	[SerializeField]
	private PlayerSync sync;

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
	
	protected Vector3 persistentVelocity;
	private bool rageQuit = false;
	
	// Use this for initialization
	public virtual void Start ()
	{
		charControl = gameObject.GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	public virtual void Update ()
	{
		Vector3 firstPos = transform.position;

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
			persistentVelocity.y = jumpForce;
		}

		//Jump arc handling
		persistentVelocity *= jumpDecayRate;

		//Camera zoom based on speed, done before delta time scaling
		//Debug.Log ("Vel Y: " + Mathf.Floor(charControl.velocity.y));
		//Debug.Log ("Vel X: " + velocity.x);
		//Debug.Log (Mathf.Floor(Mathf.Sqrt((velocity.x * velocity.x) + (charControl.velocity.y * charControl.velocity.y))));
		float velMag = Mathf.Sqrt((velocity.x * velocity.x) + (charControl.velocity.y * charControl.velocity.y));
		if(velMag > 0 && velMag > 10) localCam.fieldOfView += .6f;
		else localCam.fieldOfView -= .6f;
		//if(velMag > 0 && localCam.fieldOfView > velMag*10) localCam.fieldOfView = velMag*10;
		if(localCam.fieldOfView > maxFOV) localCam.fieldOfView = maxFOV;
		else if(localCam.fieldOfView < minFOV) localCam.fieldOfView = minFOV;
		//

		//Normal movement handling
		velocity *= Time.deltaTime;
		velocity += persistentVelocity;
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
		sync.localVelocity = (transform.position-firstPos)/Time.deltaTime;
	}
} 
