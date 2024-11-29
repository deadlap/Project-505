using System;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        if (audioClip == null)
            audioClip = GetComponent<AudioSource>().clip;
    }

    public void Play(AudioClip ac)
    {
        if(audioClip != null)
            audioSource.PlayOneShot(ac);
        else
            audioSource.PlayOneShot(ac);
    }
}
