using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SinGrowXZ : MonoBehaviour {
	
	float factor = 1.2f;
	float t = 0;
	float speed = 3.5f;
	
	float scale = 1;
	
	// Use this for initialization
	void Start () {
		scale = transform.localScale.x;
	}
	
	// Update is called once per frame
	float timer = 0;
	void Update () {
		timer += Time.deltaTime;
		if (timer > 1) Destroy (this);
		t = Mathf.Min(1, t + Time.deltaTime * speed);
		Vector3 s = Vector3.one * scale * (Mathf.Sin(t * 3.14159f) * (factor - 1) + 1);
		transform.localScale = new Vector3(s.x,transform.localScale.y,s.z);
	}
	
	public void ReturningToPool() {
		Destroy(this);	
	}
}
