using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            animator.SetBool("ScareTrigger", true);
    }
}
