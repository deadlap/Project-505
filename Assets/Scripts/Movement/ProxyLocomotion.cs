using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyLocomotion : MonoBehaviour {
    // Public references to the hand GameObjects
    public GameObject leftHandObject;
    public GameObject rightHandObject;

    // Public settings
    public float swingThreshold = 0.05f;  // Minimum arm swing speed to trigger movement
    public float movementSpeed = 1.5f;    // Movement speed multiplier
    public float smoothness = 0.1f;       // Smooth movement factor

    // Private variables to track hand movement
    private Vector3 previousLeftHandPosition;
    private Vector3 previousRightHandPosition;
    private Vector3 currentVelocity;

    // Components for movement
    private CharacterController characterController;
    private Transform headTransform; // For forward movement direction (the VR headset or camera)

    void Start()
    {
        // Initialize positions and components
        previousLeftHandPosition = leftHandObject.transform.position;
        previousRightHandPosition = rightHandObject.transform.position;

        characterController = GetComponent<CharacterController>();
        headTransform = Camera.main.transform; // Assuming VR headset camera is tagged as MainCamera
    }

    void Update()
    {
        // Track the current positions of both hands
        Vector3 currentLeftHandPosition = leftHandObject.transform.position;
        Vector3 currentRightHandPosition = rightHandObject.transform.position;

        // Calculate hand velocities (difference in position over time)
        Vector3 leftHandMovement = currentLeftHandPosition - previousLeftHandPosition;
        Vector3 rightHandMovement = currentRightHandPosition - previousRightHandPosition;

        // Calculate the average hand speed
        float leftHandSpeed = leftHandMovement.magnitude / Time.deltaTime;
        float rightHandSpeed = rightHandMovement.magnitude / Time.deltaTime;

        Debug.Log((new Vector3(headTransform.forward.x, 0, headTransform.forward.z).normalized) * movementSpeed * (leftHandSpeed + rightHandSpeed) / 2f);
        
        Debug.Log("lefthand:" + leftHandSpeed);

        Debug.Log("Righthand:" + rightHandSpeed);

        // Only move forward if both hands are moving fast enough (above the threshold)
        if (leftHandSpeed > swingThreshold && rightHandSpeed > swingThreshold)
        {
            // Get forward direction based on head orientation
            Vector3 forwardDirection = new Vector3(headTransform.forward.x, 0, headTransform.forward.z).normalized;

            // Compute velocity from hand swing speed
            currentVelocity = forwardDirection * movementSpeed * (leftHandSpeed + rightHandSpeed) / 2f;

        }
        else
        {
            // Smoothly decelerate to stop
            currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, smoothness);
        }

        // Apply movement to the character controller
        characterController.Move(currentVelocity * Time.deltaTime);

        // Update the previous hand positions for the next frame
        previousLeftHandPosition = currentLeftHandPosition;
        previousRightHandPosition = currentRightHandPosition;
    }
}
