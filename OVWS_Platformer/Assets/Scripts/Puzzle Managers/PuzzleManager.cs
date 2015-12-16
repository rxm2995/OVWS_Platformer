using UnityEngine;
using System.Collections;

public class PuzzleManager : MonoBehaviour {

	public GameObject movingPlatform, switchOne, switchTwo;
	public Vector3 finalPlatformPos;

	public AudioClip doorOpening;

	private SwitchBehavior switchOneStatus, switchTwoStatus;

	// Use this for initialization
	void Start ()
	{
		switchOneStatus = switchOne.GetComponent<SwitchBehavior> ();
		switchTwoStatus = switchTwo.GetComponent<SwitchBehavior> ();
	}

    // Update is called once per frame
    void Update()
    {
        if (!switchOneStatus.puzzleSolved && switchOneStatus.activated && switchTwoStatus.activated)
        {
            switchOneStatus.solvePuzzle();
			switchTwoStatus.solvePuzzle();

			movingPlatform.GetComponent<AudioSource> ().PlayOneShot (doorOpening, 0.8F);
			
			print ("PuzzleSolved! ");
        }
        if (switchOneStatus.puzzleSolved)
            movingPlatform.transform.position = Vector3.MoveTowards(movingPlatform.transform.position, finalPlatformPos, Time.deltaTime * 2.0f);
    }
}
