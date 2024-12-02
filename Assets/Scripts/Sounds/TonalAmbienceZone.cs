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

    [SerializeField, Tooltip("If we want the audio to faze in following a curve")] private AnimationCurve fadeCurve = AnimationCurve.Constant(0f, 1f, 1f);
    private float elapsedLength = 0f;
    [SerializeField, Min(0.01f)] private float fadeLength = 0.01f;
    private float maxVol = 1f;

    private bool playedOnce = false;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        fadeLength = source.clip.length;
        maxVol = source.volume;
    }

    public void BeginPlaying()
    {
        if (source.isPlaying) return;
        source.Play();
        playedOnce = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (source.isPlaying && elapsedLength < fadeLength)
        {
            elapsedLength += Time.deltaTime;
            source.volume = fadeCurve.Evaluate(Mathf.Min(1f, elapsedLength / fadeLength)) * maxVol;
        }

        if (repeatable) return;
        if (playedOnce && !source.isPlaying) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        BeginPlaying();
    }
}
