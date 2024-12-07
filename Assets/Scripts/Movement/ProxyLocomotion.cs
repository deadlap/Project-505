using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using System;
// using System;

public class ProxyLocomotion : MonoBehaviour {

    //Player Objects
    [SerializeField] private GameObject LeftHand;
    [SerializeField] private GameObject RightHand;

    private CharacterController PlayerCharacterController;
    [SerializeField] Transform ForwardTransform;

    // Settings 
    [SerializeField] float SwingThreshold; //A minimum value for how much the arms should swing to move the user
    [SerializeField] float HorizontalSwingThreshold;
    [SerializeField] float MovementSpeed; //How fast the user should move;
    [SerializeField] float Smoothness;
    [SerializeField] float MaxSwingSpeed; //0.09'

    
    private Vector3 MotionVector;
    //Vector3 Positions for previous hand positions
    private Vector3 PreviousLeftHandPosition;
    private Vector3 PreviousRightHandPosition; //Holder styr på din tidligere position, så man kan måle
    //hvor lang din hånd har bevæget sig.

    [SerializeField] private InputActionProperty LeftJoystickInput;
    [SerializeField] private InputActionProperty LeftControllerTrigger;
    [SerializeField] private InputActionProperty RightControllerTrigger;

    void Start() 
    {
        PreviousLeftHandPosition = RemoveXCoordinate(LeftHand.transform.localPosition); //set previous positions
        PreviousRightHandPosition = RemoveXCoordinate(RightHand.transform.localPosition);

        PlayerCharacterController = GetComponent<CharacterController>();
        MotionVector = Vector3.zero;
    }
        void OnEnable()
    {
        GameEventManager.BrokenEvent += IncreaseMoveSpeed;
    }

    void OnDisable()
    {
        GameEventManager.BrokenEvent -= IncreaseMoveSpeed; //An animation calls the BrokenEvent and it shows
        //that movement speed goes up and down.
    }

    void IncreaseMoveSpeed()
    {
        MovementSpeed = 3.2f; //Magic number.
    }

    void Update() {
        float speed = 0;

        //We only want to see direction in the y and z axis to check if the user is swinging their arms.
        // Vector3 currentLeftHandPosition = RemoveXCoordinate(LeftHand.transform.localPosition);
        // Vector3 currentRightHandPosition = RemoveXCoordinate(RightHand.transform.localPosition);

        Vector3 currentLeftHandPosition = RemoveXCoordinate(LeftHand.transform.localPosition);
        Vector3 currentRightHandPosition = RemoveXCoordinate(RightHand.transform.localPosition); //Vi får 
        //hele positionen bare uden x-koordinatet fordi det ikke relevant for svingark.

        float leftHandVelocity = (currentLeftHandPosition-PreviousLeftHandPosition).magnitude;
        float rightHandVelocity = (currentRightHandPosition-PreviousRightHandPosition).magnitude; // Længden
        //mellem nuværende position og sidste position er velocity. Vi har glemt Time.deltatime. Skal nævnes
        //i rapporten. Det er dog i movement, bare ikke selve checket.

        if ((leftHandVelocity >= SwingThreshold && LeftControllerTrigger.action?.ReadValue<float>() > 0)
                || (rightHandVelocity >= SwingThreshold && RightControllerTrigger.action?.ReadValue<float>() > 0)) {
            float leftSpeed = getSpeed(leftHandVelocity);
            float rightSpeed = getSpeed(rightHandVelocity);
            if (RightControllerTrigger.action?.ReadValue<float>() > 0 && LeftControllerTrigger.action?.ReadValue<float>() > 0){
                speed = (leftSpeed+rightSpeed)/2;
            } else if (LeftControllerTrigger.action?.ReadValue<float>() > 0) {
                speed = leftSpeed;
            } else {
                speed = rightSpeed;
            }
            MotionVector = RemoveYCoordinate(ForwardTransform.forward).normalized * speed * Time.deltaTime; //Den her
            //script checker den arm der bliver svungets hastighed, og om den er hurtig nok til at få dig til at bevæge
            //dig fremad og om man holder triggeren inde.
        }

        MotionVector = Vector3.Lerp(MotionVector, Vector3.zero, Time.deltaTime*Smoothness); //Laver en formel
        //mellem to punkter, ved at bruge en linje og så se hvor lang man er på den via de to punkter.
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
    public float getSpeed(float handSpeed){
        var speed = Mathf.Clamp(handSpeed, SwingThreshold, MaxSwingSpeed)/MaxSwingSpeed*MovementSpeed;
        return (float)speed;
    }
}