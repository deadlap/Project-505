using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class FlashLight : MonoBehaviour {
    // private bool active;
    [SerializeField] GameObject Light;
    void Start() {
        Light.SetActive(false);
    }

    // public void ToggleFlashlight(ActivateEventArgs arg){
        
    // }
}
