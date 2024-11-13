using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Welder : Equipment {
    [SerializeField] GameObject Effects;
    [SerializeField] GameObject WeldingEffect;
    // [SerializeField] GameObject EnabledEffect;
    public bool Activated {get; private set;}
    void Start() {
        // Light.SetActive(false);
        XRGrabInteractable flashlightGrabbable = GetComponent<XRGrabInteractable>();
        flashlightGrabbable.activated.AddListener(EnableWelder);
        flashlightGrabbable.deactivated.AddListener(DisableEquipmentOnEvent);
    }

    public void EnableWelder(ActivateEventArgs arg){
        Effects.SetActive(true);
        Activated = true;
    }
    public void DisableEquipmentOnEvent(DeactivateEventArgs arg){
        DisableEquipment();
    }

    public override void DisableEquipment(){
        Effects.SetActive(false);
        Activated = false;
        WeldingEffect.SetActive(false);
    }

    public void ToggleCurrentlyWelding(bool toggle){
        // IsWelding = toggle;
        WeldingEffect.SetActive(toggle);
    }
}
