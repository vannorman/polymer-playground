using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLock : VideoSequenceObject {

    public LockingMolecule lockingMolecule;
    public void Fire() {
        lockingMolecule.Lock();
    }
}
