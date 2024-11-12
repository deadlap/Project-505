using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashLight : Equipment {
    [SerializeField] GameObject Light;

    void Start() {
        Light.SetActive(false);
        XRGrabInteractable flashlightGrabbable = GetComponent<XRGrabInteractable>();
        flashlightGrabbable.activated.AddListener(ToggleFlashlight);
    }

    public void ToggleFlashlight(ActivateEventArgs arg){
        Light.SetActive(!Light.activeSelf);
    }
    public override void DisableEquipment(){
        Light.SetActive(false);
    }
}
