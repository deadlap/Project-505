using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimationController : MonoBehaviour {

    public InputActionProperty GrabAnimation;
    public InputActionProperty ClenchAnimation;
    public Animator HandAnimator;
    
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        float grabValue = GrabAnimation.action.ReadValue<float>();
        HandAnimator.SetFloat("Grab", grabValue);

        float clenchValue = ClenchAnimation.action.ReadValue<float>();
        HandAnimator.SetFloat("Clench", clenchValue);
    }
}
