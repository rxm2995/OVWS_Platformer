using UnityEngine;
using System.Collections;

public class TutorialTextManager : MonoBehaviour {
	[SerializeField]
	TextMesh tutorialText;
	ControlManager controlManager;
	// Use this for initialization
	void Start () {
		controlManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ControlManager>();
		controlManager.GetControl(PlayerActions.Jump);
		tutorialText.text = "Controls! \n" +
			"Jump: " + controlManager.GetControl(PlayerActions.Jump) + "\n" +
			"Move Left: " + controlManager.GetControl(PlayerActions.MoveLeft) + "\n" +
			"Move Right: " + controlManager.GetControl(PlayerActions.MoveRight) + "\n";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
