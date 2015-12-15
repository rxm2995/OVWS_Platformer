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
	protected ControlManager controls;
	
	protected Vector3 persistentVelocity;
	private bool rageQuit = false;

	protected Animator animCont;
	protected GameObject robotMesh;
	
	// Use this for initialization
	public virtual void Start ()
	{
		charControl = gameObject.GetComponent<CharacterController> ();
		controls = GameObject.Find("Game Manager").GetComponent<ControlManager>();

		animCont = gameObject.GetComponent<Animator> ();
		robotMesh = this.transform.FindChild ("OVWSCharEngAnim").gameObject;
	}
	
	// Update is called once per frame
	public virtual void Update ()
	{
		Vector3 firstPos = transform.position;

		Vector3 velocity = Vector3.zero;

		//Input handling
		if (Input.GetKey (controls.GetControl(PlayerActions.MoveLeft)))
		{
			velocity.x += -1 * maxSpeed;// * Time.deltaTime;
			robotMesh.transform.localEulerAngles = new Vector3(0, 90, 0);

			if (onGround) {
				animCont.SetInteger("animState", 1);
			}
			else {
				animCont.SetInteger("animState", 2);
			}
		}
		if (Input.GetKey (controls.GetControl(PlayerActions.MoveRight)))
		{
			velocity.x += maxSpeed;// * Time.deltaTime;
			robotMesh.transform.localEulerAngles = new Vector3(0, -90, 0);

			if (onGround) {
				animCont.SetInteger("animState", 1);
			}
			else {
				animCont.SetInteger("animState", 2);
			}
		}
		if(Input.GetKeyDown(controls.GetControl(PlayerActions.Ragequit)) || rageQuit)
		{
			rageQuit = true;
			velocity.z -= 20;
		}
		if (onGround && Input.GetKeyDown (controls.GetControl(PlayerActions.Jump)))
		{
			persistentVelocity.y = jumpForce;
		}
		if (Input.anyKey == false) {

			if (onGround) {
				animCont.SetInteger("animState", 0);
			}
			else {
				animCont.SetInteger("animState", 2);
			}
		}

		//Jump arc handling
		persistentVelocity *= jumpDecayRate;

		//Camera zoom based on speed, done before delta time scaling
		//Debug.Log ("Vel Y: " + Mathf.Floor(charControl.velocity.y));
		//Debug.Log ("Vel X: " + velocity.x);
		//Debug.Log (Mathf.Floor(Mathf.Sqrt((velocity.x * velocity.x) + (charControl.velocity.y * charControl.velocity.y))));
		float velMag = Mathf.Sqrt((velocity.x * velocity.x) + (charControl.velocity.y * charControl.velocity.y));
		if(velMag > 0 && velMag > 15) localCam.fieldOfView += .3f;
		else localCam.fieldOfView -= .45f;
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
