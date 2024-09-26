using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyLocomotion : MonoBehaviour {

    //Player Objects
    [SerializeField] private GameObject LeftHand;
    [SerializeField] private GameObject RightHand;
    private Transform HeadTransform;
    private CharacterController PlayerCharacterController;

    // Settings 

    [SerializeField] float SwingThreshold; //A minimum value for how much the arms should swing to move the user
    [SerializeField] float MovementSpeed; //How fast the user should move;
    [SerializeField] float Smoothness;
    
    private Vector3 MotionVector;
    //Vector3 Positions for previous hand positions
    private Vector3 PreviousLeftHandPosition;
    private Vector3 PreviousRightHandPosition;

    void Start() {
        PreviousLeftHandPosition = RemoveXCoordinate(LeftHand.transform.position); //set previous positions
        PreviousRightHandPosition = RemoveXCoordinate(RightHand.transform.position);

        PlayerCharacterController = GetComponent<CharacterController>();
        HeadTransform = Camera.main.transform;
        MotionVector = Vector3.zero;
    }

    void Update() {
        
        //We only want to see direction in the y and z axis to check if the user is swinging their arms.
        
        Vector3 currentLeftHandPosition = RemoveXCoordinate(LeftHand.transform.position);
        Vector3 currentRightHandPosition = RemoveXCoordinate(RightHand.transform.position);
        float leftHandVelocity = (currentLeftHandPosition-PreviousLeftHandPosition).magnitude;
        float rightHandVelocity = (currentRightHandPosition-PreviousRightHandPosition).magnitude;
        // Debug.Log("Right hand: " + rightHandVelocity);
        // Debug.Log("Left hand: " + leftHandVelocity);

        if (leftHandVelocity >= SwingThreshold && rightHandVelocity >= SwingThreshold) {
            // PlayerCharacterController.Move(HeadTransform.forward * MovementSpeed * Time.deltaTime);

            MotionVector = HeadTransform.forward * MovementSpeed * Time.deltaTime;
        } else {
            MotionVector = Vector3.Lerp(MotionVector, Vector3.zero, Smoothness);
        }
        PlayerCharacterController.Move(MotionVector);

        // set previous position of hands to what they currently are, for the next update
        PreviousLeftHandPosition = currentLeftHandPosition;
        PreviousRightHandPosition = currentRightHandPosition;
    }

    public Vector3 RemoveXCoordinate(Vector3 inputVector) {
        return new Vector3(0, inputVector.y, inputVector.z);
    }
}
