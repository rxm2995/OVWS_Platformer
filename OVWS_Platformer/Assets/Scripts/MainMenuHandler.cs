using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toggleVisible(GameObject parent)
	{
		for(int i = parent.transform.childCount-1; i >= 0; i--)
		{
			GameObject g = parent.transform.GetChild(i).gameObject;
			g.SetActive(!g.activeSelf);
		}
	}
}
