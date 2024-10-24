using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionSwitcher : MonoBehaviour {
    [SerializeField] ProxyLocomotion proxyLocomotion;
    [SerializeField] int locomotionType;
    [SerializeField] GameObject Light;
    void OnTriggerEnter(Collider other) {
        Debug.Log("hej");
        if (other.CompareTag("Hand")) {
            proxyLocomotion.ChangeLocomotionType(locomotionType);
            Light.SetActive(true);
        }
    }
    void Update(){
        Light.SetActive(false);
    }
}
