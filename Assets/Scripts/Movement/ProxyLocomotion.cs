using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ProxyLocomotion : MonoBehaviour {

    //Player Objects
    [SerializeField] private GameObject LeftHand;
    [SerializeField] private GameObject RightHand;

    private Transform HeadTransform;
    private CharacterController PlayerCharacterController;
    
    [SerializeField] DynamicMoveProvider NormalMove;
    [SerializeField] Transform ForwardTransform;

    // Settings 
    [SerializeField] float SwingThreshold; //A minimum value for how much the arms should swing to move the user
    [SerializeField] float MovementSpeed; //How fast the user should move;
    [SerializeField] float Smoothness;
    [SerializeField] int MovementType; //1 is normal controller, 3 is holding buttons down and swinging arms, 2 is nikolajs suggestion.
    
    
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
        var leftHandStickValue = LeftJoystickInput.action?.ReadValue<Vector2>() ?? Vector2.zero; //InverseTransformPoint
        Vector3 fixedVectorThing = (new Vector3(leftHandStickValue.x, 0, leftHandStickValue.y));
        Vector3 leftHandValue = ForwardTransform.TransformDirection(fixedVectorThing);

        //We only want to see direction in the y and z axis to check if the user is swinging their arms.
        Vector3 currentLeftHandPosition = RemoveXCoordinate(LeftHand.transform.position);
        Vector3 currentRightHandPosition = RemoveXCoordinate(RightHand.transform.position);
        
        float leftHandVelocity = (currentLeftHandPosition-PreviousLeftHandPosition).magnitude;
        float rightHandVelocity = (currentRightHandPosition-PreviousRightHandPosition).magnitude;
        if (MovementType == 1) {
            NormalMove.enabled = true;
        } else {
            NormalMove.enabled = false;
        }

        if (MovementType == 2 && (leftHandVelocity >= SwingThreshold && rightHandVelocity >= SwingThreshold && fixedVectorThing.magnitude > 0.25)) {
            MotionVector = leftHandValue * MovementSpeed * Time.deltaTime;
        
        } else if (MovementType == 3 && ((leftHandVelocity >= SwingThreshold && LeftControllerTrigger.action?.ReadValue<float>() > 0)
            || (rightHandVelocity >= SwingThreshold && RightControllerTrigger.action?.ReadValue<float>() > 0))) {

            MotionVector = RemoveYCoordinate(ForwardTransform.forward).normalized * MovementSpeed * Time.deltaTime;
            Debug.Log(MotionVector);
        }
        MotionVector = Vector3.Lerp(MotionVector, Vector3.zero, Smoothness);
        PlayerCharacterController.Move(MotionVector);

        // set previous position of hands to what they currently are, for the next update
        PreviousLeftHandPosition = currentLeftHandPosition;
        PreviousRightHandPosition = currentRightHandPosition;
    }

    private Vector3 RemoveXCoordinate(Vector3 inputVector) {
        return new Vector3(0, inputVector.y, inputVector.z);
    }
    private Vector3 RemoveYCoordinate(Vector3 inputVector) {
        return new Vector3(inputVector.x, 0, inputVector.z);
    }

    public void ChangeLocomotionType(int type){
        MovementType = type;
    }
}
