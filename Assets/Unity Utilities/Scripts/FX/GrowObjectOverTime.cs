using UnityEngine;
using System.Collections;

public class GrowObjectOverTime : MonoBehaviour {
	
	public float growFactor = 1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float f = growFactor * Time.deltaTime;
		transform.localScale += new Vector3(f,f,f); 
	}
}
