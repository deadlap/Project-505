using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckPoint : MonoBehaviour {
    // [SerializeField] int CheckPointNr;
    // public static event Action<int> ChangeCheckpointEvent;
    // public static void OnChangeCheckpoint(int number) => ChangeCheckpointEvent?.Invoke(number);


    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") || other.CompareTag("Hand")) {
            Destroy(gameObject);
        }
    }
    // void OnEnable() {
    //     ChangeCheckpointEvent += ChangeCheckpoint;
    // }

    // void OnDisable() {
    //     ChangeCheckpointEvent -= ChangeCheckpoint;
    // }

    // void ChangeCheckpoint(int number) {
    //     if (number == CheckPointNr){

    //     }
    // }



}
