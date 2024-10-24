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
    var rotation = new Vector3(camera.forward.x, 0, camera.forward.z);
    transform.rotation =  Quaternion.LookRotation(rotation);

    } 
}
