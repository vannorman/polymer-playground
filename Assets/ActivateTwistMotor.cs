using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTwistMotor : VideoSequenceObject {

    public enum ActivationType { 
        On,
        Off
    }
    public ActivationType activationType;
    public override void Fire() {
        switch (activationType) { 
            case ActivationType.On:
                FindObjectOfType<TwistController>().TurnOnMotor();
                break;
            case ActivationType.Off:
                FindObjectOfType<TwistController>().TurnOffMotor();
                break;
        }
    }
}
