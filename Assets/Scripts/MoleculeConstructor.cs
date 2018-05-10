using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeConstructor : MonoBehaviour {

	public static MoleculeConstructor inst;
	public void Start(){
		inst = this;
	}
	// Creates small molecules

	public class Molecule {
		public GameObject atom;
		public GameObject stem;
		public List<Molecule> children = new List<Molecule>(); // for easy parenting/recursion
		public List<Molecule> DeepChilds {
			get { 
				List<Molecule> deepChilds = new List<Molecule> ();
				foreach (Molecule ch1 in children) {
					deepChilds.Add (ch1);
					deepChilds.AddRange (ch1.DeepChilds);
				}
				return deepChilds;
			}
			// Including self
//			if (childs == null)
//				childs = new List<Molecule> ();
//			if (parent.children.Count > 0) {
//				foreach (Molecule ch in parent.children) {
//					childs.AddRange (GetAllChildren (ch, childs));
//
//				}
//			}
//			childs.Add (this);
//			return childs;
		}

	}

	public Molecule molecule;
	public Transform moleculeCenter;
//	GameObject currentMolecule;

	public void CreateMolecule(){
		molecule = new Molecule ();
		molecule.atom = (GameObject)Instantiate (Prefabs.inst.sphere);
		molecule.atom.transform.position = moleculeCenter.position;
		MoleculeClicker.inst.ClickMolecule (molecule.atom);

	}

	public void AddToBaseMolecule(){
		AddToMolecule (currentMolecule);
	}


	Molecule currentMolecule;
	public void SelectMolecule(GameObject a){
//		Debug.Log ("selecting:" + c);
		if (a == molecule.atom) {
			currentMolecule = molecule;
		} else {
		
			foreach (Molecule m in molecule.DeepChilds) {
//				Debug.Log ("m:" + m.atom.name);
				if (m.atom == a) {
//					Debug.Log ("match!");
					currentMolecule = m;
				} else {
//					Debug.Log ("no match:"+m.atom.name+","+c.gameObject.name);
				}
			}
		}
	}

	Material lastMaterialUsed;
	public void CycleColor(){
		if (currentMolecule != null) {
			currentMolecule.atom.GetComponent<Renderer> ().material.color = Random.ColorHSV ();
		}
	}

	public void SetColor(UnityEngine.UI.Image im){
		if (currentMolecule != null) {
			Material m = im.gameObject.GetComponent<MaterialRef> ().m;
			currentMolecule.atom.GetComponent<Renderer> ().material = m;
			lastMaterialUsed = m;
		}
	}

	public void AddToMolecule(Molecule source){
//		Debug.Log ("add?");
		Molecule newMol = new Molecule ();
		newMol.atom = (GameObject)Instantiate (Prefabs.inst.sphere);

		Vector3 offset = Vector2.zero;
		int checks = 50;
		while(Physics.CheckSphere(source.atom.transform.position + offset, 0.4f) && checks > 0){
			checks --;
			offset	= Random.onUnitSphere * 2f;
		}
//		Debug.Log ("checks:" + checks);
		newMol.atom.transform.position = source.atom.transform.position + offset;
		newMol.stem = GameObject.CreatePrimitive (PrimitiveType.Cube);
		newMol.stem.transform.position = (newMol.atom.transform.position + source.atom.transform.position) / 2f;
		newMol.stem.transform.LookAt (source.atom.transform);
		newMol.stem.transform.localScale = new Vector3 (0.05f, 0.05f, 1);
		newMol.stem.transform.SetParent (newMol.atom.transform);
		newMol.atom.transform.SetParent (source.atom.transform);
		source.children.Add (newMol);
		newMol.atom.name = "new atom at " + Time.time;
		newMol.atom.GetComponent<Renderer> ().material = lastMaterialUsed;
//		Debug.Log ("added! source childs:"+source.children.Count);
	}

	public void DestroyMolecule(GameObject o){
		if (o == molecule.atom) {
			Destroy (molecule.atom);
			molecule = null;
			currentMolecule = null;
			return;
		}
		List<Molecule> childsToDestroy = new List<Molecule> ();
		Molecule molToRemove= null;
		foreach (Molecule m in molecule.DeepChilds) {
			if (m.atom == o) {
				molToRemove = m;
//				childsToDestroy.Add (m);
//				childsToDestroy.Add (m.DeepChilds);
			}
		}

		foreach (Molecule m in molecule.DeepChilds) {
			if (m.children.Contains (molToRemove)) {
				m.children.Remove (molToRemove);
			}
		}
		Destroy (o);

	}
}
