using UnityEngine;
using System.Collections;

public class ShrinkAndDisappear : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale *= .85f;
		if (transform.localScale.x <= .1f){
			Destroy(gameObject);
		}
	}
	
}
