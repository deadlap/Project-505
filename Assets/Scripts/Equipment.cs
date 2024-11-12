using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Equipment : MonoBehaviour {


    [SerializeField] GameObject BaseLocation;
    [SerializeField] Vector3 BaseLocationVec;
    // [SerializeField]  BaseRotation;
    [SerializeField] bool Equipped;
    [SerializeField] float Speed;
    [SerializeField] float DistanceThreshold;
    [SerializeField] Rigidbody RB;

    void Start() {
        // RB = GetComponent<Rigidbody>();)
        BaseLocationVec = Vector3.zero;
        // XRGrabInteractable equipmentGrabbable = ;
        GetComponent<XRGrabInteractable>().selectExited.AddListener(OnSelectExited);
    }

    void OnSelectExited(SelectExitEventArgs arg){
        DisableEquipment();
    }
    public virtual void DisableEquipment(){}
    void Update() {
        if (!Equipped && Vector3.Distance(transform.position, BaseLocation.transform.position) > DistanceThreshold) {
            transform.position = Vector3.Lerp(transform.position, BaseLocation.transform.position, Speed);
            transform.rotation = BaseLocation.transform.rotation;
            RB.velocity = Vector3.zero;
        } else {
            transform.position = BaseLocation.transform.position;
            transform.rotation = BaseLocation.transform.rotation;
            RB.velocity = Vector3.zero;
        }
        // if (!Equipped) {
        //     DisableEquipment();
        // }

    	// RB.useGravity = Equipped;
    }
}
