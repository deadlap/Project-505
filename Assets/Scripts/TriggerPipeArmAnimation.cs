using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPipeArmAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator animator;
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && GameEventManager.INSTANCE.GaugeDone) { 
            animator.SetBool("ActivateAnimation", true);
        }
    }
}
