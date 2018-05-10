using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour {

	public GameObject spherePrefab;
	public int scale = 1;
	List<GameObject> pieces = new List<GameObject>();
	public void BuildNow(){
		foreach (GameObject o in pieces) {
			DestroyImmediate (o);
		}
		pieces.Clear ();
		foreach (Vector3 p in Utils2.GetCircleOfPoints(360,10,scale)) {
			GameObject s = (GameObject)Instantiate (spherePrefab, transform.position + p, Quaternion.identity);
			pieces.Add (s);
			s.transform.SetParent (transform);
		}
	}
}
