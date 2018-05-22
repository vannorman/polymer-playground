using UnityEngine;
using System.Collections;

public class RecordPosition : MonoBehaviour {
	
	public Vector3 lastPosition;
	public Vector3 nowPosition;
	
	void Start() {
		lastPosition = transform.position;
		nowPosition = transform.position;
	}
	
	void LateUpdate () {
		lastPosition = nowPosition;
		nowPosition = transform.position;
	}
}
