using UnityEngine;
using System.Collections;

public class ControlPresetter : MonoBehaviour
{

	public void assignWasdControls()
	{
		GameObject.Find("Game Manager").GetComponent<ControlManager>().SetWASDControls();
	}

	public void assignArrowControls()
	{
		GameObject.Find("Game Manager").GetComponent<ControlManager>().SetArrowControls();
	}
}