using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] bool isCollider;

    void Play()
    {
        //animator.SetBool("Play");
    }
    void OnTriggerEnter(Collider other)
    {
        if(!isCollider) return;
        if(other.CompareTag("Player"))
            animator.SetBool("ScareTrigger", true);
    }
}
