using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceSetter : MonoBehaviour {

    public ItemPopup itemPopup;
	// Use this for initialization
	void Start () {
        ItemPopup.inst = itemPopup;
	}

	
}
