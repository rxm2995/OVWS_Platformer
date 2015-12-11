using UnityEngine;
using System.Collections;

public class MinimapBehavior : MonoBehaviour {

	public RectTransform panel;
	private Camera thisCamera;
	float lastScreenHeight;
	float lastScreenWidth;
	Player playerRef;

	// Use this for initialization
	void Start () {
		thisCamera = GetComponent<Camera>();
		thisCamera.aspect = (16f/9f);
		panel.sizeDelta = new Vector2(thisCamera.aspect * Screen.width * .575f, thisCamera.aspect * Screen.height * .575f); 
		lastScreenWidth = Screen.width;
		lastScreenHeight = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {

		if(lastScreenWidth != Screen.width || lastScreenHeight != Screen.height)
		{
			lastScreenWidth = Screen.width;
			lastScreenHeight = Screen.height;
			thisCamera.aspect = (16f/9f);
			panel.sizeDelta = new Vector2(thisCamera.aspect * Screen.width * .575f, thisCamera.aspect * Screen.height * .575f); 
		}
	}
}
