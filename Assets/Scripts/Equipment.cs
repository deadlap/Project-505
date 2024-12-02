using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Equipment : MonoBehaviour {
    [SerializeField] GameObject BaseLocation;
    [SerializeField] float Speed;
    [SerializeField] float DistanceThreshold;
    [SerializeField] Rigidbody RB;
    internal bool LeftHandGrabbed;
    internal bool RightHandGrabbed;
    [SerializeField] internal InputActionReference ActivateRightButton;
    [SerializeField] internal InputActionReference ActivateLeftButton;
    public virtual void Start() {
        LeftHandGrabbed = false;
        RightHandGrabbed = false;
        GetComponent<XRGrabInteractable>().selectExited.AddListener(OnSelectExited);
        ActivateLeftButton.action.started += ActivateEquipmentEvent;
        ActivateLeftButton.action.canceled += DeactivateEquipmentEvent;
        ActivateRightButton.action.started += ActivateEquipmentEvent;
        ActivateRightButton.action.canceled += DeactivateEquipmentEvent;
        XRGrabInteractable equipmentGrabbable = GetComponent<XRGrabInteractable>();
        equipmentGrabbable.selectEntered.AddListener(GrabEquipment);
        equipmentGrabbable.selectExited.AddListener(UngrabEquipment);
    }
    internal void OnSelectExited(SelectExitEventArgs arg){
        UngrabEquipment(arg);
    }
    public void ActivateEquipmentEvent(InputAction.CallbackContext context){
        ActivateEquipment(context);
    }
    public void DeactivateEquipmentEvent(InputAction.CallbackContext context){
        DeactivateEquipment(context);
    }
    public void GrabEquipmentEvent(SelectEnterEventArgs arg){
        GrabEquipment(arg);
    }
    public void UngrabEquipmentEvent(SelectExitEventArgs arg){
        UngrabEquipment(arg);
    }
    public virtual void ActivateEquipment(InputAction.CallbackContext context){}
    public virtual void DeactivateEquipment(InputAction.CallbackContext context){}
    public virtual void GrabEquipment(SelectEnterEventArgs arg){}
    public virtual void UngrabEquipment(SelectExitEventArgs arg){}
    public virtual void DisableEquipment(){}
    void Update() {
        if (Vector3.Distance(transform.position, BaseLocation.transform.position) > DistanceThreshold) {
            transform.position = Vector3.Lerp(transform.position, BaseLocation.transform.position, Speed);
            transform.rotation = BaseLocation.transform.rotation;
            RB.velocity = Vector3.zero;
        } else {
            transform.position = BaseLocation.transform.position;
            transform.rotation = BaseLocation.transform.rotation;
            RB.velocity = Vector3.zero;
        }
    }
}
