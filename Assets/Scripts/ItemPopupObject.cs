using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPopupObject : VideoSequenceObject {

    public ItemPopup.PopupType popType;

    public string message = "2Molecules sensitive to light rotate when light is on2";
	// Use this for initialization
	public override void Fire() {
        //ItemPopup.inst.Show(transform.position, message,popType);
	}
	
}
