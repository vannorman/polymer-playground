using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopup : MonoBehaviour {

    public string message = "2Molecules sensitive to light rotate when light is on2";
	// Use this for initialization
	void Start () {
        ItemPopup.inst.Show(transform.position, message);
	}
	
}
