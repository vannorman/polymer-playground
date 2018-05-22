using UnityEngine;
using System.Collections;

public class TriggerMessageBroadcaster : MonoBehaviour {
	
	public string functionToBroadcast;
	public string functionToBroadcastExit;
//	public string argsToBroadcast;
	public GameObject objToBroadcastTo;
	public bool oneUse = false;
	public bool passGameObject = false;
	
	
	void OnTriggerEnter(Collider other){
//		Debug.Log("other:"+other.name);

		// done to test timing of arena
		// should actually be done "when you first touch the current arena key
//		// commented Debug.Log("trig");
		if (passGameObject) {
			
			objToBroadcastTo.SendMessage(functionToBroadcast,other.gameObject,SendMessageOptions.DontRequireReceiver); // functionToBroadcast+"("+gameObject+")",SendMessageOptions.DontRequireReceiver);
//			// commented Debug.Log ("hi");
		}
		else objToBroadcastTo.SendMessage(functionToBroadcast,SendMessageOptions.DontRequireReceiver);
//		if (oneUse) Destroy (gameObject);
	}

	void OnTriggerExit(Collider other){
		//		Debug.Log("other:"+other.name);

		// done to test timing of arena
		// should actually be done "when you first touch the current arena key
		//		// commented Debug.Log("trig");
		if (passGameObject) {

			objToBroadcastTo.SendMessage(functionToBroadcastExit,other.gameObject,SendMessageOptions.DontRequireReceiver); // functionToBroadcast+"("+gameObject+")",SendMessageOptions.DontRequireReceiver);
			//			// commented Debug.Log ("hi");
		}
		else objToBroadcastTo.SendMessage(functionToBroadcastExit,SendMessageOptions.DontRequireReceiver);
		//		if (oneUse) Destroy (gameObject);
	}
}
