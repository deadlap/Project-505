using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldingFishTrigger : MonoBehaviour {
    [SerializeField] Animator animator;
    void Start(){
        
    }

    void OnEnable() {
        GameEventManager.CompleteWeldingEvent += TriggerAnimation;
    }

    void OnDisable() {
        GameEventManager.CompleteWeldingEvent -= TriggerAnimation;
    }
    void TriggerAnimation(){
        animator.SetBool("ActivateAnimation", true);
    }
}
