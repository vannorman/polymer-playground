using UnityEngine;
using System.Collections;

public class ScaleUpAndStop : MonoBehaviour {

	public Vector3 stopScale = Vector3.one;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float speed = 20f;
		transform.localScale = Vector3.MoveTowards(transform.localScale,stopScale,Time.deltaTime * speed);
		if (Vector3.SqrMagnitude(transform.localScale-stopScale)<.01f){
			

			Destroy(this);
		}
	}
}
