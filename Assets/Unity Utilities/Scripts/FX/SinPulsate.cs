using UnityEngine;
using System.Collections;

public class SinPulsate : MonoBehaviour {

	float scale=1;
	float factor=1;	
	public float pulsateSpeed=3;
	public float amplitude=1; 
	
	// Use this for initialization
	void Start () {
		scale=transform.localScale.x;
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = Vector3.one * (scale + (amplitude*(Mathf.Sin(Time.time*pulsateSpeed))));
	}
}
