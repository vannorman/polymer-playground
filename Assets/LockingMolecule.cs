using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LockingMolecule : MonoBehaviour {

    public Transform locked;
    public Transform unlocked;
    public Transform obj;
    Transform target;

    public void Lock(){
        rotating = true;
        target = locked;
    }

    public void Unlock(){
        rotating = true;
        target = unlocked;
    }

    bool rotating = false;
    void Update(){
        if (rotating){
            float rotSpeed = 3f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, Time.deltaTime * rotSpeed);
            if (Vector3.Angle(transform.forward, target.forward) < 1) {
                rotating = false;
            }
        }

    }
}
