using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Welder : Equipment {
    [SerializeField] GameObject Effects;
    [SerializeField] GameObject WeldingEffect;
    [SerializeField] WeldingSounds SoundEffects;
    public bool Activated {get; private set;}
    public override void Start() {
        base.Start();
    }

    public override void ActivateEquipment(InputAction.CallbackContext context){
        if ((LeftHandGrabbed && context.action.name == "PrimaryButtonLeft") || (RightHandGrabbed && context.action.name == "PrimaryButtonRight")) {
            Effects.SetActive(true);
            Activated = true;
            SoundEffects.BeginSpark();
        }
    }
    public override void DeactivateEquipment(InputAction.CallbackContext context){
        if ((LeftHandGrabbed && context.action.name == "PrimaryButtonLeft") || (RightHandGrabbed && context.action.name == "PrimaryButtonRight")) {
            DisableEquipment();
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
    public override void DisableEquipment(){
        Effects.SetActive(false);
        Activated = false;
        WeldingEffect.SetActive(false);
        SoundEffects.EndSpark();
    }
    public void ToggleCurrentlyWelding(bool toggle){
        if (Activated) {
            WeldingEffect.SetActive(toggle);
            if (toggle) {
                SoundEffects.WeldCorrect();
            } else {
                SoundEffects.EndWeld();
            }
        }
            
    }
    public void DisableEquipmentOnEvent(SelectExitEventArgs arg){
        DisableEquipment();
    }
}
