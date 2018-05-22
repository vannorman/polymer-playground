using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainExactFixedPositionToObject : MonoBehaviour {


	public Transform point;

	// Update is called once per frame
	void LateUpdate () {
		transform.position = point.position;	
	}
}
