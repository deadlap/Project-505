using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TonalAmbienceZone : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField, Multiline] private string notes;
#endif
    [SerializeField] private bool repeatable = false;


    private bool playedOnce = false;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (repeatable) return;
        if (playedOnce && !source.isPlaying) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (source.isPlaying) return;
        source.Play();
        playedOnce = true;
    }
}
