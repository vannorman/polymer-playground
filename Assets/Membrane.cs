using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Membrane : MonoBehaviour {

	public int size = 100;
	public GameObject spherePrefab;
	Dictionary<Vector3,GameObject> surface = new Dictionary<Vector3, GameObject>();
	float scale = 1;
	public void BuildNow(){
		
		foreach (Vector3 p in surface.Keys) {
			DestroyImmediate (surface [p]);
		}
		List<Transform> chlds = new List<Transform> ();
		foreach (Transform t in transform) {
			chlds.Add (t);
		}
		foreach(Transform t in chlds){
			DestroyImmediate (t.gameObject);
		}
		surface.Clear ();
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {			
				int sign = i % 2 == 0 ? 1 : -1;
//				int sign = 1;
				Vector3 p = transform.position + (new Vector3 (i * 0.67f, 0, j+sign * 0.25f))  * scale;
				GameObject s = (GameObject)Instantiate (spherePrefab, p, Quaternion.identity);
				s.transform.SetParent (transform);
				surface.Add (p, s);
				s.name = i + ", " + j;
			}
		}
	}
	void Start(){
//		CreateMembrane();
	}
}
