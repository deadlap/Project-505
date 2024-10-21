using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckPoint : MonoBehaviour {
    // [SerializeField] int CheckPointNr;
    // public static event Action<int> ChangeCheckpointEvent;
    // public static void OnChangeCheckpoint(int number) => ChangeCheckpointEvent?.Invoke(number);


    void OnTriggerEnter(Collider other) {
        Debug.Log("hej");
        if (other.CompareTag("Player")) {
            Destroy(gameObject); Debug.Log("hej 2");
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
