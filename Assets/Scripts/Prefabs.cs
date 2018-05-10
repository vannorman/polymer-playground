using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour {

	public static Prefabs inst;
	void Start(){
		SetInstance ();
	}
	public void SetInstance(){
		inst = this;
	}
	public GameObject sphere;
	public GameObject molecule1;
	public GameObject mol3;
	public GameObject wedgePrefab;
	public static GameObject wedge {
		get { 
			return (GameObject)Instantiate(inst.wedgePrefab);
		}
	}

}
