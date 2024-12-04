using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGaugeDoorOpen : MonoBehaviour {
    [SerializeField] Animator animator;
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && GameEventManager.INSTANCE.WeldingDone) {
            animator.SetBool("ActivateAnimation", true);
        }
    }
}
