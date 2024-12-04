using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnterDivingBellBox : MonoBehaviour {
    float MaxTime;
    float CurrentTime;
    bool HasTriggered;
    void Start(){
        MaxTime = 0f;
        MaxTime = 0.5f;
        HasTriggered = false;
    }
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && GameEventManager.INSTANCE.BrokenReached && !HasTriggered) {
            CurrentTime += Time.deltaTime;
        }
    }
    void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player") && GameEventManager.INSTANCE.BrokenReached && !HasTriggered) {
            CurrentTime += Time.deltaTime;
            if (CurrentTime >= MaxTime) {
                HasTriggered = true;
                GameEventManager.INSTANCE.StartEndSequence();
            }
        }
    }
    void OnTriggerExit(Collider other) {
        CurrentTime = 0;
    }
}