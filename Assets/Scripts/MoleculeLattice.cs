using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeLattice : MonoBehaviour {



	public int size = 25;

	class LatticePiece {
		public GameObject o;
		public Vector3 origP;
		public Vector3 targetP;

	}
	List<LatticePiece> molecules = new List<LatticePiece>();


	void Start(){
		Invoke ("InitLattice", 1);
//		InitLattice ();
	}

//	Dictionary<Vector3,GameObject> molecules = new Dictionary<Vector3,GameObject>();
	public float scale = 8;
	void InitLattice(){
//		for (int i = -size / 2; i < size * 1.5f; i++) {

//			for (int j = 0; j < 2; j++) {
//				for (int k = -size / 2; k < size * 1.5f; k++) {
		for (int i=0;i<1;i++){
			for (int j=0;j<1;j++){
				for (int k=0;k<1;k++){
					LatticePiece p = new LatticePiece ();


					GameObject mol = (GameObject)Instantiate(Prefabs.inst.mol3);
					mol.transform.SetParent (transform);
					mol.transform.localPosition = scale * new Vector3 (i, j, k);
					p.o = mol;
					p.origP = mol.transform.localPosition;
					p.targetP = mol.transform.localPosition;
//					molecules.Add (mol.transform.position,mol);
					molecules.Add(p);
				}
			}
		
		}
	}

	void Update(){
//		Debug.Log ("up");
		if (Input.GetKeyDown (KeyCode.A)) {
//			Debug.Log ("A");
			if (Input.GetKey (KeyCode.LeftShift)) {
				foreach (LatticePiece m in molecules){
					
					GameObject o = m.o;
					o.GetComponent<Folding> ().FoldOut ();
					m.targetP = m.origP * 1.1f;

				}
			} else {
//				Debug.Log ("");
				foreach (LatticePiece m in molecules){
//					Debug.Log ("M:" + m);
					GameObject o = m.o;	
					o.GetComponent<Folding> ().FoldIn ();
					m.targetP = m.origP * 0.9f;
				}
			}
		}

		foreach (LatticePiece m in molecules) {
			float squeezeSpeed = 2f;
			m.o.transform.localPosition = Vector3.MoveTowards (m.o.transform.localPosition, m.targetP, Time.deltaTime * squeezeSpeed);
		}
	}





}
