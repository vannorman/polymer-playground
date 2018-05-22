using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieController : MonoBehaviour {

	public GameObject cameraParent;

	void Start(){
		cameraParent.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.M)){
			cameraParent.SetActive(true);
			Camera.main.rect = new Rect(new Vector2(0,0),new Vector2(0.5f,1));
		}
	}
}
