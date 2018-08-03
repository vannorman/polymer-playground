using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFlashlight : VideoSequenceObject {

    override public void Fire() {
        FindObjectOfType<Flashlight>().TurnOn();
    }
}
