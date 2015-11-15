using UnityEngine;
using System.Collections;

public class SwitchBehavior : MonoBehaviour {

	private Light solveIndicator;

	public float activateRadius = 1;
	public bool activated;
	public bool puzzleSolved;

	public Color inactiveColor = Color.red;
	public Color activeColor = Color.yellow;
	public Color solvedColor = Color.green;

	public bool toggle;
	public bool pressure;

	// Use this for initialization
	void Start ()
	{
		puzzleSolved = false;
		solveIndicator = gameObject.GetComponentInChildren<Light> ();
		solveIndicator.color = Color.red;
	}
	
	// Update is called once per frame
	void Update ()
	{
//		if (!puzzleSolved)
//		{
//			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
//
//			for(int i = 0; i < players.Length; i++)
//			{
//				if((transform.position-players[i].transform.position).magnitude <= activateRadius)
//				{
//					activated = true;
//					solveIndicator.color = activeColor;
//					return;
//				}
//			}
//
//			activated = false;
//			solveIndicator.color = inactiveColor;
//		}
	}

	void OnTriggerEnter(Collider col)
	{
		Debug.Log("Test");
		if(col.gameObject.tag == "Player")
		{
			if(toggle)
			{
				activated = !activated;
				if(activated)
					solveIndicator.color = activeColor;
				else
					solveIndicator.color = inactiveColor;
			}
			if(pressure)
			{
				if(!puzzleSolved)
				{
					activated = true;
					solveIndicator.color = activeColor;
				}
			}
		}
	}

	void OnTriggerExit(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
//			if(toggle)
//			{
//
//			}
			if(pressure)
			{
				if(!puzzleSolved)
				{
					activated = false;
					solveIndicator.color = inactiveColor;
				}
			}
		}
	}

	public void solvePuzzle()
	{
		puzzleSolved = true;
		solveIndicator.color = solvedColor;
	}

	public void unSolvePuzzle()
	{
		puzzleSolved = false;
		solveIndicator.color = inactiveColor;
	}
}
