using UnityEngine;
using System.Collections;

public class ScrollTextureComplex : MonoBehaviour {
	
	public string texName = "_MainTex"; // "_BumpMap"
	public Vector2 scrollDirection = new Vector2(0,1);
	public float scrollSpeed = .2f;
	public int matIndex = 0;
	Vector2 offset;
	public bool oscillate = false;
	public float oscillateInterval = 1.0f;
	Vector3 scrollDirA = new Vector2(1,0);
//	Vector3 scrollDirB = new Vector2(1,0);
	Renderer rend;
//	RecordPosition rp;
	public bool moveExtraWhenCameraMoves = false;
	public float moveExtraAmount = 1f;

		
	// Use this for initialization
	void Start () {
		offset = gameObject.GetComponent<Renderer>().material.GetTextureOffset(texName);
		rend = gameObject.GetComponent<Renderer>();
		scrollDirA = scrollDirection;
//		rp = Camera.main.GetComponent<RecordPosition>();
//		scrollDirB = -scrollDirection;
	}

	Vector3 lastCamDiff = Vector3.zero;

	// Update is called once per frame
	void Update () {
		Material[] mats = rend.materials;
		mats[matIndex].SetTextureOffset(texName,offset);
		rend.materials = mats;
		if (oscillate){
			scrollDirection = Mathf.Sin(Time.unscaledTime * 3.1415f*2f/oscillateInterval) * scrollDirA;
		}
		if (moveExtraWhenCameraMoves){
//			Vector3 moved = rp.nowPosition - rp.lastPosition;
			Vector3 nowCamDiff = transform.position - Camera.main.transform.position;
			Vector3 moved = lastCamDiff - nowCamDiff;
			float maxMovedMagnitude = 1f;
			if (moved.magnitude > maxMovedMagnitude) {
				moved = moved.normalized;
			}
			scrollDirection = new Vector2(moved.x+moved.z,moved.y);
			lastCamDiff = nowCamDiff;
			//		Debug.Log("scrolldir;"+scrollDirection);
			offset += scrollDirection*moveExtraAmount*Time.unscaledDeltaTime;
		}
		offset += scrollDirection *scrollSpeed*Time.unscaledDeltaTime;
	}
}
