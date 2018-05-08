using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolymerTwist : MonoBehaviour {

	public GameObject molToTwist;
	List<GameObject> twisted = new List<GameObject>();
	GameObject anchor;
	GameObject anchor2;

	public Slider twistAmt;
	public Slider spiral;

	public void Twist(){
		foreach (GameObject o in twisted) {
			DestroyImmediate (o);
			Destroy (o);

		}
		twisted.Clear ();
		Vector3[] vs = Utils2.SimpleSpiral ();
		for(int i=0;i< vs.Length;i++){
//			if (i == 0) {
//				anchor = new GameObject ("anchor");
//				anchor.transform.position = vs [0];
//				Rigidbody rb = anchor.AddComponent<Rigidbody> ();
//				rb.isKinematic = true;
//			}
//			if (i == vs.Length - 1) {
//				anchor2 = new GameObject ("anchor2");
//				anchor2.transform.position = vs [i];
//				Rigidbody rb = anchor2.AddComponent<Rigidbody> ();
//				rb.isKinematic = true;
//			}
			Vector3 v = vs [i];
			GameObject t = (GameObject) GameObject.Instantiate (molToTwist);
			t.transform.position = transform.position+	v;
			if (i > 0) {
				t.transform.up = -(vs [i] - vs [i - 1]);
			}
			twisted.Add (t);
			t.transform.SetParent (this.transform);
			Vector3 dir = Vector3.up;
			if (i > 0)
				dir = vs [i] - vs [i - 1];
			else
				dir = vs [i + 1] - vs [i];
			t.transform.rotation = Quaternion.AngleAxis (0, dir);

		}
		for (int i=0;i<twisted.Count;i++){
//			GameObject tw = twisted [i];
//			Rigidbody rb = tw.AddComponent<Rigidbody> ();
//
//			rb.useGravity = false;
//
//			HingeJoint hj = tw.AddComponent<HingeJoint> ();
//			if (i == 0) {
//				hj.anchor = anchor.transform.position;
//				hj.connectedBody = anchor.GetComponent<Rigidbody>();
//			}
//			if (i > 0) {
//				hj.connectedBody = twisted [i - 1].GetComponent<Rigidbody> ();
//			}
//			if (i == twisted.Count - 1) {
//				hj.anchor = anchor2.transform.position;
//				hj.connectedBody = anchor2.GetComponent<Rigidbody>();
//			}
//			hj.spring
//				hj.anchor = twisted [i - 1].transform.position;
					

		}
	}

	float t = 0;
	float int1 = 1;
	int twistIndex = 0;

	void Start(){
		spiralPiecesParent2 = new GameObject ("par2");
	}

//	GameObject s = (GameObject)Instantiate (molToTwist); // GameObject.CreatePrimitive (PrimitiveType.Sphere);
	List<List<GameObject>> cachedPolymers = new List<List<GameObject>>();
	void InitPolymer(int len){
		List<GameObject> poly = new List<GameObject> ();
		for (int i = 0; i < len; i++) {
			GameObject p = (GameObject)Instantiate (Prefabs.inst.molecule1);

			poly.Add (p);
		}
		cachedPolymers.Add (poly);
	}

	// Spiral equation
	public LineRenderer lr;
	List<GameObject> spiralPieces = new List<GameObject>();
	List<GameObject> spiralPieces2 = new List<GameObject>();
	GameObject spiralPiecesParent2;
	public void Spiral(){
		float spacing = 2f;
		float maxTwists = 15;
		int polymerLength = 150;
//		Vector3[] pts = Utils2.Spiral (50,spiral.value * maxTwists, spacing);
		Vector3[] pts1 = Utils2.Spiral2 (polymerLength,spiral.value * maxTwists,spacing);
		Vector3[] pts2 = Utils2.Spiral2 (polymerLength,Mathf.Max(0,(spiral.value * maxTwists) - 0.5f),spacing);
//		Vector3[] pts2 = Utils2.Spiral2 (50,Mathf.Max(0,(spiral.value - 0.5f)) * maxTwists,spacing);

		// Bend the pts
		pts1 = Utils2.BendVectorArray(pts1,Vector3.right,180);
		pts2 = Utils2.BendVectorArray(pts2,Vector3.right,180);


		spiralPiecesParent2.transform.rotation = Quaternion.identity;
		spiralPiecesParent2.transform.position = Vector3.zero;
//		spiralPiecesParent2 = new GameObject ();

		spiralPieces.Clear ();


		spiralPieces2.Clear ();
//		Debug.Log ("pts:" + pts.Length);

		for(int i=0;i<pts1.Length;i++){
			if (cachedPolymers.Count < 1) {
				InitPolymer (polymerLength);
			}
			GameObject s = cachedPolymers [0] [i];
			

			spiralPieces.Add (s);
			s.transform.position = pts1 [i];
			if (i > 0)
				s.transform.LookAt (pts1 [i - 1]);
		}

		for(int i=0;i<pts2.Length;i++){
			if (cachedPolymers.Count < 2) {
				InitPolymer (polymerLength);
			}
			GameObject s = cachedPolymers [1] [i];
			spiralPieces2.Add (s);
			s.transform.position = pts2 [i];
			s.transform.SetParent (spiralPiecesParent2.transform);
			if (i > 0)
				s.transform.LookAt (pts2 [i - 1]);
		}

		spiralPiecesParent2.transform.position += Vector3.right * leftOffset;
//		spiralPiecesParent2.transform.rotation = Quaternion.Euler (0, 90, 0);
	} 
	public float leftOffset = 10f;


	public void TwistSlider(){
		Spiral ();


	}
}
