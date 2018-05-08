using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinPop : MonoBehaviour {

	// Use this for initialization
	Vector3 startSize;
	void Start () {
		startSize = transform.localScale;
	}

	float popSpeed = 3;
	bool popping = false;
	public void Pop(float amt = 1.2f, float speed = 1){
		popping = true;
		popSpeed = speed;
		transform.localScale = startSize * amt;
	}
	// Update is called once per frame
	void Update () {
		if (popping) {
			transform.localScale = Vector3.Lerp (transform.localScale, startSize, Time.deltaTime * popSpeed);
			if (Vector3.Magnitude (transform.localScale - startSize) < .01f) {
				popping = false;
				transform.localScale = startSize;
			}
		}
	}
}
