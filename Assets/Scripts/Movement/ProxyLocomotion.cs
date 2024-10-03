using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ProxyLocomotion : MonoBehaviour {

    //Player Objects
    [SerializeField] private GameObject LeftHand;
    [SerializeField] private GameObject RightHand;

    private Transform HeadTransform;
    private CharacterController PlayerCharacterController;
    [SerializeField] Transform ForwardTransform;

    // Settings 
    [SerializeField] float SwingThreshold; //A minimum value for how much the arms should swing to move the user
    [SerializeField] float MovementSpeed; //How fast the user should move;
    [SerializeField] float Smoothness;
    
    private Vector3 MotionVector;
    //Vector3 Positions for previous hand positions
    private Vector3 PreviousLeftHandPosition;
    private Vector3 PreviousRightHandPosition;

    [SerializeField] private InputActionProperty LeftJoystickInput;
    [SerializeField] private InputActionProperty LeftControllerTrigger;
    [SerializeField] private InputActionProperty RightControllerTrigger;

    void Start() {
        PreviousLeftHandPosition = RemoveXCoordinate(LeftHand.transform.position); //set previous positions
        PreviousRightHandPosition = RemoveXCoordinate(RightHand.transform.position);

        PlayerCharacterController = GetComponent<CharacterController>();
        MotionVector = Vector3.zero;
    }

    void Update() {
        // var leftHandStickValue = LeftJoystickInput.action?.ReadValue<Vector2>() ?? Vector2.zero; //InverseTransformPoint
        // Vector3 leftHandValue = Vector3.InverseTransformPoint(new Vector3(leftHandStickValue.y, 0,leftHandStickValue.x), ForwardTransform.forward);

        //We only want to see direction in the y and z axis to check if the user is swinging their arms.
        
        // var leftHandStickValue = LeftJoystickInput.action?.ReadValue<Vector2>() ?? Vector2.zero; //InverseTransformPoint

        Vector3 currentLeftHandPosition = RemoveXCoordinate(LeftHand.transform.position);
        Vector3 currentRightHandPosition = RemoveXCoordinate(RightHand.transform.position);

        float leftHandVelocity = (currentLeftHandPosition-PreviousLeftHandPosition).magnitude;
        float rightHandVelocity = (currentRightHandPosition-PreviousRightHandPosition).magnitude;

        // if (leftHandVelocity >= SwingThreshold && rightHandVelocity >= SwingThreshold && leftHandStickValue.magnitude > 0.25) {
        if ((leftHandVelocity >= SwingThreshold && LeftControllerTrigger.action?.ReadValue<float>() > 0)
            || (rightHandVelocity >= SwingThreshold && RightControllerTrigger.action?.ReadValue<float>() > 0)) {

            MotionVector = ForwardTransform.forward * MovementSpeed * Time.deltaTime;

            // MotionVector = leftHandValue * MovementSpeed * Time.deltaTime;
        } else {
            MotionVector = Vector3.Lerp(MotionVector, Vector3.zero, Smoothness);
        }
        PlayerCharacterController.Move(MotionVector);

        // set previous position of hands to what they currently are, for the next update
        PreviousLeftHandPosition = currentLeftHandPosition;
        PreviousRightHandPosition = currentRightHandPosition;
    }

    private Vector3 RemoveXCoordinate(Vector3 inputVector) {
        return new Vector3(0, inputVector.y, inputVector.z);
    }
}
