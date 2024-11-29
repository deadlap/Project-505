using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashLight : Equipment {
    [SerializeField] GameObject Light;
    [SerializeField] InputActionReference ActivateButton;
    [SerializeField] InputActionReference Activate2Button;
    bool Grabbed;
    void Start() {
        Light.SetActive(false);
        Grabbed = false;
        XRGrabInteractable flashlightGrabbable = GetComponent<XRGrabInteractable>();
        flashlightGrabbable.selectEntered.AddListener(GrabFlashlight);
        flashlightGrabbable.selectExited.AddListener(UngrabFlashlight);
        // flashlightGrabbable.activated.AddListener(ToggleFlashlight);
        ActivateButton.action.started += ToggleFlashlight;
        Activate2Button.action.started += ToggleFlashlight;
    }
    
    void GrabFlashlight(SelectEnterEventArgs arg){
        Grabbed = true;
    }
    void UngrabFlashlight(SelectExitEventArgs arg){
        Grabbed = false;
        DisableEquipment();
    }

    public void ToggleFlashlight(InputAction.CallbackContext context){
        if (Grabbed || Light.activeSelf){
            Light.SetActive(!Light.activeSelf);
        }
    }

    public void ToggleFlashlight(ActivateEventArgs arg){
        if (Grabbed || Light.activeSelf){
            Light.SetActive(!Light.activeSelf);
        }
    }
    public override void DisableEquipment(){
        Light.SetActive(false);
    }
}
