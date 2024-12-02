using System;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField, Tooltip("If this is checked, the collider on the GameObject will check for the player and call PlayVaried() when they enter it.")] bool isTrigger;
    [SerializeField, Tooltip("If this is checked, the audio will only play the first time the player enters the collider.")] bool playOnce;
    bool hasPlayed;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    [SerializeField, Tooltip("PlayVaried prioritises playing a random of these over the singular audioClip")] private AudioClip[] multiClips;

    [Header("PlayVaried Audio Settings")]
    [SerializeField, Range(0f, 1f)] private float minVol = 0.7f;
    [SerializeField, Range(0f, 1f)] private float maxVol = 1f;
    [SerializeField, Range(0f, 2f)] private float minPitch = 0.85f;
    [SerializeField, Range(0f, 2f)] private float maxPitch = 1.2f;

    void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        if (audioClip == null)
            audioClip = audioSource.clip;
    }

    public void Play(AudioClip ac)
    {
        if (audioClip != null)
            audioSource.PlayOneShot(audioClip);
        else
            audioSource.PlayOneShot(ac);
    }

    public void PlayVaried(AudioClip ac)
    {
        audioSource.volume = UnityEngine.Random.Range(minVol, maxVol);
        audioSource.pitch = UnityEngine.Random.Range(minPitch, maxPitch);

        audioSource.clip = ac;
        audioSource.Play();
    }

    public void PlayVaried()
    {
        AudioClip ac;
        if (multiClips != null) ac = multiClips[UnityEngine.Random.Range(0, multiClips.Length)];
        else ac = audioClip;
        PlayVaried(ac);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isTrigger)
        {
            if (playOnce)
            {
                if(hasPlayed) return;
                PlayVaried();
                hasPlayed = true;
            }
            else
            {
                PlayVaried(audioSource.clip);
            }
        }
    }
}
