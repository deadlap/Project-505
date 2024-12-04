using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableEndReached : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) { 
            GameEventManager.INSTANCE.BrokenReached = true;
        }
    }
}
