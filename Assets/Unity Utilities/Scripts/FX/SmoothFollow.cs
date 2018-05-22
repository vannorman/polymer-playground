using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{
	public Transform objectToFollow;
	
	void Start(){
//		// commented Debug.Log("created cam smooth follow at pos:"+transform.position);
	}
	
	float heightOffset = .5f;
	public float followDistance = 1.5f;
	public float speed=6f;
	// Update is called once per frame
	void LateUpdate ()
	{
		
//		// trace camera path
//		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
//		sphere.transform.position =transform.position;
//		sphere.transform.localScale *=.1f;
//		sphere.name = "CamSphere@"+Time.time;
		if (objectToFollow){
			
			Vector3 cameraTarget = objectToFollow.transform.position + new Vector3(0,heightOffset,0) - transform.forward*followDistance;
			transform.position = Vector3.Slerp(transform.position,cameraTarget,Time.deltaTime*speed);
			Quaternion rotation=Quaternion.identity;
			if (Vector3.SqrMagnitude(cameraTarget-transform.position)!=0) // prevent "0" vector for look rotation
				rotation = Quaternion.LookRotation(cameraTarget - transform.position);
			float damping = 4f;
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
		} else {
//			// commented Debug.Log("Destroying smoothfollow object, as we lost our target.");
			Destroy(gameObject); // We lost our target. This is too shameful to bear. Kill ourselves.
		}
		
		// // commented Debug.Log("smooth follow at pos:"+transform.position);
		
//		camera.transform.LookAt(cameraTarget);
	}
	
	// Update is called once per frame
	/*void LateUpdate ()
	{
		//Calculate desired position for camera to be at
		float x1 = transform.localPosition.x;
		float y1 = transform.localPosition.y;
		float z1 = transform.localPosition.z;
		Vector3 desiredWorldPosition = transform.parent.TransformPoint(new Vector3(x1,y1,-1.0f*minDistance));
	
		//Detect if there are any objects obstructing the camera's view of the player
		int layermask = ~ (1 << 8); //Linecast shouldn't hit the player layer
		RaycastHit hit; //Holds information about successful Linecasts
		if(Physics.Linecast(target.position, desiredWorldPosition, out hit, layermask))
		{ //If we've hit an object obstructing the camera's view of the player
			//Calculate new camera position
			transform.position = hit.point + (objectToFollow.position - hit.point).normalized * cameraCollisionTolerance; //Move camera slightly away from object
		}
		else
		{
			//Damp to desired camera positions
//			transform.localPosition = new Vector3(Mathf.SmoothDamp(transform.localPosition.x, x, ref xVelocity, damping),
//												  Mathf.SmoothDamp(transform.localPosition.y, y, ref yVelocity, damping),
//												  Mathf.SmoothDamp(transform.localPosition.z, z, ref zVelocity, damping));
		}
	}*/
}


	//		Vector3 newPos3 = (lastRotation * ((Vector3.back * distanceX) + (Vector3.up * distanceY))) + objectToFollow.position;
	//		transform.position = newPos3;
		//	transform.position = new Vector3(cameraPos.x, objectToFollow.position.y + distanceY, cameraPos.z);
		//	transform.position = new Vector3(cameraPos.x, objectToFollow.position.y + distanceY, objectToFollow.position.z + ( Vector3.back * distanceX ).z);
			//Vector3 newPos2 = new Vector3(cameraPos.x, objectToFollow.position.y + distanceY, cameraPos.z);
			//transform.position = Vector3.Lerp(transform.position, newPos2, 4.0f * Time.deltaTime); // AMT: Jan28
			
