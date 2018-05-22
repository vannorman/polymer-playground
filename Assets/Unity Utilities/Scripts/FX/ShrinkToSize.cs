using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkToSize : MonoBehaviour {

	public float finalSize = 1f;
	public float moveSpeed = 2f;
	public bool destroyIfShrunk = true;

	void LateUpdate(){
		transform.localScale = Vector3.MoveTowards(transform.localScale,Vector3.one*finalSize,moveSpeed * Time.deltaTime);
		if (Vector3.SqrMagnitude(transform.localScale-Vector3.one*finalSize) < .01f){
			transform.localScale = Vector3.one * finalSize;
			if (destroyIfShrunk) Destroy(this);
		}
	}
}
