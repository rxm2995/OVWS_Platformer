using UnityEngine;
using System.Collections;

public class LightColorShifter : MonoBehaviour {
	public Light currentlight;
	public float r;
	public float g;
	public float b;
	public float rChange;
	public float gChange;
	public float bChange;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		currentlight.color = new Color(r,g,b);
		r += rChange;
		g += gChange;
		b += bChange;
		if(r > 1 || r < 0) rChange *= -1;
		if(g > 1 || g < 0) gChange *= -1;
		if(b > 1 || b < 0) bChange *= -1;
	}
}
