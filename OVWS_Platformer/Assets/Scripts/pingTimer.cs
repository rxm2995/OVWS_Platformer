using UnityEngine;
using System.Collections;

public class pingTimer : MonoBehaviour {

	private float startTime;
	private float duration = 4.5f;
	void Start () {
		startTime = Time.time;
	}

	void Update () {
		if (Time.time > startTime + duration) {
			Destroy(gameObject);
		}
	}
}
