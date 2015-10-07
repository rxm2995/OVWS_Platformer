using UnityEngine;
using System.Collections;

public class PuzzleManager3 : MonoBehaviour
{
	public GameObject switchOne;
	public GameObject dynamicPlatform;

	private SwitchBehavior switchOneBehavior;
	private bool puzzleSolved;

	// Use this for initialization
	void Start ()
	{
		switchOneBehavior = switchOne.GetComponent<SwitchBehavior>();
		puzzleSolved = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!puzzleSolved && switchOneBehavior.activated)
		{
			puzzleSolved = true;
			switchOneBehavior.solvePuzzle();
			dynamicPlatform.transform.position = new Vector3(dynamicPlatform.transform.position.x,
			                                                 2,
			                                                 0);
		}
	}
}
