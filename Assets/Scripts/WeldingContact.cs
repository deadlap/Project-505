using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldingContact : MonoBehaviour {
    [SerializeField] public Welder WelderGun;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("WeldingSpot")) {
            WelderGun.ToggleCurrentlyWelding(true);
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.CompareTag("WeldingSpot")) {
            WelderGun.ToggleCurrentlyWelding(false);
        }
    }
}
