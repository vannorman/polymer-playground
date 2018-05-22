using UnityEngine;
using System.Collections;

public class SinHover : MonoBehaviour {

	public float interval=3;
	public float amplitude=1;
	public float offset=0;
	public bool randomOffset=false;
	public bool useParent = false;
	Vector3 startPos;
	public bool ignorePause = false;

	// Use this for initialization
	void Start () {
		startPos=transform.position;
		if (randomOffset){
			offset = Random.Range (-offset,offset);
		}
	}
	
	// Update is called once per frame
	void Update () {
		float newY=0;
		float t = Time.time;
		if (ignorePause) t = Time.realtimeSinceStartup;
		float twopi = (Mathf.PI * 2);
		float sineResult = Mathf.Sin(t * twopi / interval + offset * twopi);
		if (useParent) newY = transform.parent.position.y + sineResult * amplitude;
		else newY = startPos.y + sineResult * amplitude;
		transform.position = new Vector3(transform.position.x,newY,transform.position.z);
	}
}
