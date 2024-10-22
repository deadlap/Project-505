using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;

public class Belt : MonoBehaviour {
    [SerializeField] Transform camera;
    void Start() { 
        camera = Camera.main.transform;
    }
    void Update() {
        FaceCameraDirection();
    }
    void FaceCameraDirection() {
    transform.rotation = Quaternion.LookRotation(camera.forward);
    } 
}
