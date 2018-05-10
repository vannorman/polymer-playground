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
		foreach (Vector3 p in Utils2.HexGrid (size,scale)) {
			GameObject s = (GameObject)Instantiate (spherePrefab, transform.position + p, Quaternion.identity);
			s.transform.SetParent (transform);
			surface.Add (p, s);

//				s.name = i + ", " + j;

		}
	}
	void Start(){
//		CreateMembrane();
	}
}
