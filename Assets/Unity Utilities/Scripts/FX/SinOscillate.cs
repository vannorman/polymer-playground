using UnityEngine;

public class SinOscillate : MonoBehaviour {

	float scale=1;
	float factor=1;	
	public float pulsateSpeed=3;
	public float amplitude=1; 
	Vector3 startPos;
	public Vector3 localDirection;


	void Start () {
		startPos = transform.localPosition;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.localPosition = startPos + localDirection * (scale + (amplitude*(Mathf.Sin(Time.time*pulsateSpeed))));
	}
}
