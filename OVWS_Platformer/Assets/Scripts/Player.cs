using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("w"))
		{

		}
		else if(Input.GetKey("s"))
		{
			
		}
		if(Input.GetKey("a"))
		{
			transform.position = new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z);
		}
		else if(Input.GetKey("d"))
		{
			transform.position = new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z);
		}
	}
}
