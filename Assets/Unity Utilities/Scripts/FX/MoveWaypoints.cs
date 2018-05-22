using UnityEngine;
using System.Collections;

public class MoveWaypoints : MonoBehaviour {


	public Transform[] waypoints;
	public float moveSpeed = 3f;
	int waypointIndex = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(waypoints[waypointIndex].position,transform.position) < 0.2f){
			TargetNextWaypoint();
		} else {
			Quaternion targetRot = Quaternion.LookRotation(waypoints[waypointIndex].position - transform.position,Vector3.up);
			float rotSpeed = 3f;
			transform.rotation = Quaternion.Slerp(transform.rotation,targetRot,rotSpeed * Time.deltaTime);
//			transform.LookAt(waypoints[waypointIndex],Vector3.up);

			transform.position += Time.deltaTime * moveSpeed * transform.forward;
		}
	}

	public void TargetNextWaypoint(){
		waypointIndex = (waypointIndex + 1) % waypoints.Length;


	}
}
