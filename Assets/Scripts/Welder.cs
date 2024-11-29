using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Welder : Equipment {
    [SerializeField] InputActionReference ActivateButton;
    [SerializeField] InputActionReference Activate2Button;
    [SerializeField] GameObject Effects;
    [SerializeField] GameObject WeldingEffect;
    public bool Activated {get; private set;}
    bool Grabbed;
    void Start() {
        Grabbed = false;
        XRGrabInteractable welderGrabbable = GetComponent<XRGrabInteractable>();
        welderGrabbable.selectEntered.AddListener(EnableWelder);
        welderGrabbable.selectExited.AddListener(DisableEquipmentOnEvent);
        ActivateButton.action.started += ActivateWelder;
        ActivateButton.action.canceled += UnActivateWelder;
        Activate2Button.action.started += ActivateWelder;
        Activate2Button.action.canceled += UnActivateWelder;
    }

    void ActivateWelder(InputAction.CallbackContext context){
        if (Grabbed) {
            Effects.SetActive(true);
            Activated = true;
        }        
    }
    void UnActivateWelder(InputAction.CallbackContext context){
        DisableEquipment();
    }

    public void EnableWelder(SelectEnterEventArgs arg){
        Grabbed = true;
    }
    public void DisableEquipmentOnEvent(SelectExitEventArgs arg){
        DisableEquipment();
        Grabbed = false;
    }

    public override void DisableEquipment(){
        Effects.SetActive(false);
        Activated = false;
        WeldingEffect.SetActive(false);
    }

    public void ToggleCurrentlyWelding(bool toggle){
        if (Activated)
            WeldingEffect.SetActive(toggle);
    }
}
