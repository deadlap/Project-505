using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Welder : Equipment {
    [SerializeField] GameObject Effects;

    void Start() {
        // Light.SetActive(false);
        XRGrabInteractable flashlightGrabbable = GetComponent<XRGrabInteractable>();
        flashlightGrabbable.activated.AddListener(EnableWelder);
        flashlightGrabbable.deactivated.AddListener(DisableEquipmentOnEvent);
    }

    public void EnableWelder(ActivateEventArgs arg){
        Effects.SetActive(true);
    }
    public void DisableEquipmentOnEvent(DeactivateEventArgs arg){
        DisableEquipment();
    }

    public override void DisableEquipment(){
        Effects.SetActive(false);
    }
}
