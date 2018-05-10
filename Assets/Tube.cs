using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tube : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InitTube();
	}

	List<List<GameObject>> tubeObjs = new List<List<GameObject>> ();
	void InitTube(){
//		Vector3[] p = Utils2.Tube (3,14);
		List<List<Vector3>> tube = Utils2.Tube2(3,14);
//		List<Vector3> p = new List<Vector3> (); // for all pts, for center
		for (int j = 0; j < tube.Count; j++) {
			List<GameObject> layero = new List<GameObject> ();
			List<Vector3> layer = tube [j];
			for (int i = 0; i < layer.Count; i++) {
				GameObject o = Prefabs.atomWithPaddle;
				o.transform.position = layer [i];
				layero.Add (o);
			}
			tubeObjs.Add (layero);
		}


			
		GameObject center = new GameObject ("center");
		Vector3[] p = tubeObjs.SelectMany (array => array).Select(go => go.transform.position).ToArray();
		center.transform.position = Utils2.center (p);
		for (int j=0;j<tubeObjs.Count;j++){
			for (int i = 0; i < tubeObjs [j].Count; i++) {
				GameObject o = tubeObjs [j] [i];

				o.transform.LookAt (center.transform, Vector3.up);
			
				o.transform.rotation = Utils2.FlattenRotation (o.transform.rotation);

				if (j % 2 == 0) {
					o.GetComponent<Paddle> ().Swap ();
//					o.transform.Rotate (Vector3.up * 180);
				}

			}
//			foreach(
		}
//		center.transform.LookAt (transform, Vector3.up);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
