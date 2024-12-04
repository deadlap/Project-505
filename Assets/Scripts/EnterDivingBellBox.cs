using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDivingBellBox : MonoBehaviour {
    bool HasTriggered;
    void Start(){
        HasTriggered = false;
    }
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && GameEventManager.INSTANCE.BrokenReached && !HasTriggered) {
            HasTriggered = true;
            GameEventManager.INSTANCE.StartEndSequence();
        }
    }
}
