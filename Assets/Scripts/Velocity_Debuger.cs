using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Velocity_Debuger : MonoBehaviour {
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject Hand;
    private Vector3 PreviousHandPosition;

    void Start() {
        PreviousHandPosition = RemoveXCoordinate(Hand.transform.position);
    }

    void Update() {
        Vector3 HandPosition = RemoveXCoordinate(Hand.transform.position);
        float HandVelocity = (HandPosition-PreviousHandPosition).magnitude;
        HandVelocity = Mathf.Round(HandVelocity * 1000.0f) * 0.001f;
        text.text = ""+HandVelocity;
        PreviousHandPosition = HandPosition;
    }
    
    private Vector3 RemoveXCoordinate(Vector3 inputVector) {
        return new Vector3(0, inputVector.y, inputVector.z);
    }
}