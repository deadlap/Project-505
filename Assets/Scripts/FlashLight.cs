using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashLight : Equipment {
    [SerializeField] GameObject Light;
    [SerializeField] MeshRenderer glass;
    [SerializeField] Material turnedOn, turnedOff;
    void Start() {
        Light.SetActive(false);
        XRGrabInteractable flashlightGrabbable = GetComponent<XRGrabInteractable>();
        flashlightGrabbable.activated.AddListener(ToggleFlashlight);
    }
    
    public void ToggleFlashlight(ActivateEventArgs arg){
        Light.SetActive(!Light.activeSelf);
        switch (Light.activeSelf)
        {
            case true: glass.material = turnedOn;
                break;
            case false: glass.material = turnedOff;
                break;
        }
    }
    public override void DisableEquipment(){
        Light.SetActive(false);
    }
}
