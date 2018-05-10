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

	public static PolymerTwist inst;
	void Start(){
		inst = this;

		InitPolymers ();
		Spiral (0.5f);
	}


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



//	GameObject s = (GameObject)Instantiate (molToTwist); // GameObject.CreatePrimitive (PrimitiveType.Sphere);
	List<List<GameObject>> cachedPolymers = new List<List<GameObject>>();
	void InitPolymers (){
		spiralPiecesParent2 = new GameObject ("par2");
		spiralPiecesParent1 = new GameObject ("par1");
		List<GameObject> poly1 = new List<GameObject> ();
		List<GameObject> poly2 = new List<GameObject> ();
		for (int i = 0; i < polymerLength; i++) {
			GameObject p = (GameObject)Instantiate (Prefabs.inst.molecule1);
			poly1.Add (p);
			p.transform.SetParent (spiralPiecesParent1.transform);

			GameObject p2 = (GameObject)Instantiate (Prefabs.inst.molecule1);
			poly2.Add (p2);
			p2.transform.SetParent (spiralPiecesParent2.transform);

		}
		cachedPolymers.Add (poly1);
		cachedPolymers.Add (poly2);
		spiralPiecesParent2.transform.position += Vector3.right * leftOffset;
		Debug.Log ("polymers init. cached len:" + cachedPolymers.Count + ", cahcned[0]:" + cachedPolymers [0].Count+", 1:"+cachedPolymers[1].Count);

	}

	// Spiral equation
	public LineRenderer lr;
//	List<GameObject> spiralPieces = new List<GameObject>();
//	List<GameObject> spiralPieces2 = new List<GameObject>();
	GameObject spiralPiecesParent1;
	GameObject spiralPiecesParent2;
	public float spiralOffset = 0.5f;
	public float radius = 5;
	public float radiusFactor = 1.01f;
	public bool reverse = false;
	public float reverseFactorP = 0.5f;
	float maxTwists = 15;
	public float subTwistAmount = 30;
	public float spacing = 1.667f;
	int polymerLength = 300;
	public void Spiral(float twistAmount){
		spacing = 500f / (float)polymerLength;
//		Vector3[] pts = Utils2.Spiral (50,spiral.value * maxTwists, spacing);
		float spiralOffset1 = reverse ? 0 : spiralOffset; 
		float spiralOffset2 = reverse ? spiralOffset : 0;
		float reverseFactor = reverse ? reverseFactorP : 0;
		Vector3[] pts1 = Utils2.Spiral2 (polymerLength,Mathf.Max(0,(twistAmount) - spiralOffset1 + reverseFactor),spacing,radius, reverse);
//		Vector3[] pts2 = Utils2.Spiral2 (50,Mathf.Max(0,(spiral.value - 0.5f)) * maxTwists,spacing);
		Vector3[] pts2 = Utils2.Spiral2 (polymerLength,Mathf.Max(0,(twistAmount) - spiralOffset2 + reverseFactor),spacing,radius,reverse);

		// Bend the pts
		int arc = 120;
		pts1 = Utils2.BendVectorArray(pts1,Vector3.right,arc);
		pts2 = Utils2.BendVectorArray(pts2,Vector3.right,arc,radiusFactor);


		spiralPiecesParent1.transform.rotation = Quaternion.identity;
		spiralPiecesParent1.transform.position = Vector3.zero;

		spiralPiecesParent2.transform.rotation = Quaternion.identity;
		spiralPiecesParent2.transform.position = Vector3.zero;
//		spiralPiecesParent2 = new GameObject ();

//		spiralPieces.Clear ();
//
//
//		spiralPieces2.Clear ();
//		Debug.Log ("pts:" + pts.Length);

		for(int i=0;i<pts1.Length;i++){
			GameObject s = cachedPolymers [0] [i];
			s.transform.localPosition = pts1 [i];
			if (i > 0) {
				s.transform.LookAt (pts1 [i - 1]);
				s.transform.Rotate (Vector3.forward * subTwistAmount * i, Space.Self);
			}
		}

		for(int i=0;i<pts2.Length;i++){
			

			
			GameObject s = cachedPolymers [1] [i];

			s.transform.localPosition = pts2 [i];
			if (i > 0) {
				s.transform.LookAt (pts2 [i - 1]);
				s.transform.Rotate (Vector3.forward * subTwistAmount * i, Space.Self);
			}
		}


//		spiralPiecesParent2.transform.rotation = Quaternion.Euler (0, 90, 0);
	} 
	public float leftOffset = 10f;


	public void TwistSlider(){
		Spiral (spiral.value);


	}

	public void Update(){
//		if (Input.GetKey(KeyCode.A)){
//			int sign = Input.GetKey (KeyCode.LeftShift) ? -1 : 1;
//			spiral.value += 0.1f * Time.deltaTime * sign;
//			reverse = !reverse;
//			TwistSlider ();
//		}
	}
}
