using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WedgeFarm : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GrowFarm ();

	}

	public void GrowFarm(){
		int size = 3;
		float scale = 2;
		foreach (Vector3 p in Utils2.HexGrid (size,scale)) {
			GameObject w = Prefabs.wedge;
			w.transform.SetParent (transform);
			w.transform.localPosition = p;
			w.transform.localRotation = Quaternion.Euler (0, -30, 120);
			w.GetComponent<Wedge>().fixedPosition = true;
			w.GetComponent<Wedge> ().SetMode (Wedge.AttachMode.ReadyToReceive);
		}
	}
}

