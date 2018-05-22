using UnityEngine;
using System.Collections;

public class FloatEffect : MonoBehaviour {
	
	Vector3 startPos;
	public float floatDist = 1;
	public float floatSpeed = 1;
	float randomOffset;
	// Use this for initialization
	void Start () {
		startPos=transform.position;
		randomOffset=Random.Range(0f,3f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = startPos + new Vector3(0,Mathf.Sin(Time.time*floatSpeed+randomOffset)*floatDist,0);
	}
}
