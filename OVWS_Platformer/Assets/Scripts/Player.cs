using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float maxSpeed = 6f;
	public float maxForce = 3f;
	public float mass = 1f;
	public float gravity = 20f;

	protected Vector3 velocity;
	protected Vector3 acceleration;

	protected CharacterController charControl;

	// Use this for initialization
	void Start () {
		charControl = gameObject.GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {

		string input;
		input = Input.inputString;
		switch (input) {
		case "a":
			charControl.Move (new Vector3 (0, transform.position.y, 0) * 10 * Time.deltaTime);
			break;
		case "d":
			charControl.Move (new Vector3 (0, transform.position.y, 0) * -10 * Time.deltaTime);
			break;
		}
		
	}
} 
