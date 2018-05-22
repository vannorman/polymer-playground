using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SinGrow : MonoBehaviour {
	
	float factor = 1.6f;
	float t = 0;
	float speed = 5.5f;

	float scale = 1;

	public void SetAttr(float factor1=1.6f, float speed1=5.5f){
		factor = factor1;
		speed = speed1;
	}

	// Use this for initialization
	void Start () {
		lastTime = Time.realtimeSinceStartup;
		scale = transform.localScale.x;
	}
	
	// Update is called once per frame
	float timer = 0;
	float lastTime = 0;
	void Update () {
		
//		// commented Debug.Log("dtime:"+dTime+",timer:"+timer);
		timer += Time.deltaTime;
		if (timer > 1) Destroy (this);
		t = Mathf.Min(1, t + Time.unscaledDeltaTime * speed);
		transform.localScale = Vector3.one * scale * (Mathf.Sin(t * 3.14159f) * (factor - 1) + 1);
	}
	

}
