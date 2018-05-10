using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

	public GameObject paddleCube1;
	public GameObject paddleCube2;

	public void Swap(){
		paddleCube1.SetActive (!paddleCube1.activeSelf);
		paddleCube2.SetActive (!paddleCube2.activeSelf);
	}
}
