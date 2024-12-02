using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class FlashLight : Equipment {
    [SerializeField] GameObject Light;
    public override void Start() {
        base.Start();
        Light.SetActive(false);
    }
    
    public override void ActivateEquipment(InputAction.CallbackContext context){
        if ((LeftHandGrabbed && context.action.name == "PrimaryButtonLeft") || (RightHandGrabbed && context.action.name == "PrimaryButtonRight")) {
            ToggleFlashlight(new InputAction.CallbackContext());
        }
    }
    public override void GrabEquipment(SelectEnterEventArgs arg) {
        if (arg.interactorObject.transform.name == "LeftHand") {
            LeftHandGrabbed = true;
            RightHandGrabbed = false;
        } else if (arg.interactorObject.transform.name == "RightHand") {
            RightHandGrabbed = true;
            LeftHandGrabbed = false;
        }
    }
    public override void UngrabEquipment(SelectExitEventArgs arg) {
        DisableEquipment();
        LeftHandGrabbed = false;
        RightHandGrabbed = false;
    }

    public void ToggleFlashlight(InputAction.CallbackContext context) {
        Light.SetActive(!Light.activeSelf);
    }
    public override void DisableEquipment() {
        Light.SetActive(false);
    }
}
