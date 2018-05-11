using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeLattice : MonoBehaviour {

//
//	public class Node {
//		public GameObject obj;
//		public List<Node> children = new List<Node>();
//	}
//
//	Node node;

	public class MoleculeString {
		public Transform container;
		public List<LatticePiece> mols = new List<LatticePiece> ();
	}

	List<MoleculeString> strings = new List<MoleculeString>();
	List<MoleculeString> strings2 = new List<MoleculeString>();

	public Vector3 size = new Vector3 (1, 1, 15);
	public 

	class LatticePiece : MonoBehaviour {
		public GameObject o;
		public Vector3 origP;
		public Vector3 targetP;

	}
	List<LatticePiece> molecules = new List<LatticePiece>();


	void Start(){
		Invoke ("InitLattice", 1);
//		InitLattice ();
	}
	int numNodes = 4;
//	Dictionary<Vector3,GameObject> molecules = new Dictionary<Vector3,GameObject>();
	public float scale = 8;
	GameObject mainContainer1;
	GameObject mainContainer2;
	void InitLattice(){
		Debug.Log ("makin it");
//		for (int i = -size / 2; i < size * 1.5f; i++) {

//			for (int j = 0; j < 2; j++) {
//				for (int k = -size / 2; k < size * 1.5f; k++) {
		mainContainer1 = new GameObject("init container");
		for (int n=0;n<numNodes;n++){
			MoleculeString ms = new MoleculeString ();
			ms.container = new GameObject ("ms container "+n).transform;





			for (int i=0;i<size.x;i++){
				for (int j=0;j<size.y;j++){
					for (int k=0;k<size.z;k++){
						
//						GameObject cont = new GameObject ("container");

						GameObject mol = (GameObject)Instantiate(Prefabs.inst.mol3);
						LatticePiece p = mol.AddComponent<LatticePiece> (); //new LatticePiece ();
						mol.transform.SetParent (ms.container);
						mol.transform.localPosition = scale * new Vector3 (i, j, k);
						p.o = mol;
						p.origP = mol.transform.localPosition;
						p.targetP = mol.transform.localPosition;
						ms.mols.Add (p);
						//					molecules.Add (mol.transform.position,mol);
//						molecules.Add(p);
					}
				}
			}
			if (n == 0) {
				ms.container.SetParent (mainContainer1.transform);
				ms.container.transform.Rotate (Vector3.up * 60);
			} else {
				ms.container.SetParent(strings[strings.Count - 1].mols[strings[strings.Count - 1].mols.Count - 1].o.transform); // put this entire molecule string as a child of the LAST molecule in the previous string, for stretching porposes.
				ms.container.transform.localPosition = Vector3.zero;
				if (n % 2 == 0) {
					ms.container.transform.Rotate (Vector3.up * 60);
				} else {
					ms.container.transform.Rotate (Vector3.up * -60);
				}
			}
			strings.Add (ms);


		}



		mainContainer1.transform.SetParent (this.transform);


		mainContainer2 = new GameObject("init container");
		for (int n=0;n<12;n++){
			MoleculeString ms = new MoleculeString ();
			ms.container = new GameObject ("ms container "+n).transform;





			for (int i=0;i<size.x;i++){
				for (int j=0;j<size.y;j++){
					for (int k=0;k<size.z;k++){

						//						GameObject cont = new GameObject ("container");

						GameObject mol = (GameObject)Instantiate(Prefabs.inst.mol3);
						LatticePiece p = mol.AddComponent<LatticePiece> (); //new LatticePiece ();
						mol.transform.SetParent (ms.container);
						mol.transform.localPosition = scale * new Vector3 (i, j, k);
						p.o = mol;
						p.origP = mol.transform.localPosition;
						p.targetP = mol.transform.localPosition;
						ms.mols.Add (p);
						//					molecules.Add (mol.transform.position,mol);
						//						molecules.Add(p);
					}
				}
			}
			if (n < 4) {
				ms.container.SetParent (mainContainer2.transform);
				ms.container.name = "Container <4";
//				ms.container.transform.Rotate (Vector3.up * 60);
			} else if (n < 8) {
				ms.container.SetParent(strings2[3].mols[strings2[3].mols.Count - 1].o.transform); // put this entire molecule string as a child of the LAST molecule in the previous string, for stretching porposes.
				ms.container.name = "Container <8";
			} else if (n <  12) {
				ms.container.SetParent(strings2[7].mols[strings2[7].mols.Count - 1].o.transform); // put this entire molecule string as a child of the LAST molecule in the previous string, for stretching porposes.
				ms.container.name = "Container <12";
			}
			ms.container.transform.localPosition = Vector3.zero;

			switch(n%4){
			case 0:
				break;
			case 1:
				ms.container.transform.Rotate (Vector3.up * 90);
				break;
			case 2:
				ms.container.transform.Rotate (Vector3.up * 180);
				break;
			case 3:
				ms.container.transform.Rotate (Vector3.up * 270);
				break;
			}



			strings2.Add (ms);

			mainContainer2.transform.position += Vector3.right * 100;
			mainContainer2.transform.SetParent (this.transform);

		}
	}

	void Update(){
//		Debug.Log ("up");
		if (Input.GetKeyDown (KeyCode.A)) {
//			Debug.Log ("A");
			if (Input.GetKey (KeyCode.LeftShift)) {
				foreach (Folding f in  this.GetComponentsInChildren<Folding>()){
					
					f.FoldOut ();


				}
				foreach (LatticePiece m in this.GetComponentsInChildren<LatticePiece>()) {
					Debug.Log ("got m:" + m);
					m.targetP = m.origP * 1.3f;
				}
			} else {
				foreach (Folding f in  this.GetComponentsInChildren<Folding>()){

					f.FoldIn ();


				}
				foreach (LatticePiece m in this.GetComponentsInChildren<LatticePiece>()) {

//					m.targetP = m.origP * 1.1f;
					m.targetP = m.origP * 0.6f;
				}
			}
		}

		foreach (LatticePiece m in this.GetComponentsInChildren<LatticePiece>()) {
			float d = (m.o.transform.localPosition - m.targetP).magnitude;
			float squeezeSpeed = 0.5f;
			m.o.transform.localPosition = Vector3.Lerp (m.o.transform.localPosition, m.targetP, Time.deltaTime * squeezeSpeed);
		}
	}





}
