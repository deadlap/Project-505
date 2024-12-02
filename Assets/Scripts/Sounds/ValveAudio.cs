using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ValveAudio : MonoBehaviour
{
    private AudioSource source;
    [Header("General Settings")]
    [SerializeField, Tooltip("Whether Small Screeching or Looping Clip is used")] private bool randomSmallScreeches = false;
    [SerializeField, Range(0f, 1f)] private float maxVol = 0.4f;
    [Header("Used for looping audioclip")]
#if UNITY_EDITOR
    [SerializeField, Multiline] private string loopNotes = "Looping clip plays a screeching sound constantly.\nThis clip changes in volume based on the rotational speed of the valves.\nPlays between 0 and maxVol as the volume";
#endif
    [SerializeField] private AudioClip loopingClip;
    [SerializeField, Range(0f, 1f)] private float minRotationSpeed = 0.01f;
    [SerializeField, Range(0f, 1f)] private float maxRotationSpeed = 0.1f;
    [SerializeField, Min(0f), Tooltip("Time for sound to fade out when not being turned")] private float falloffSeconds = 0.2f;
    private float currentFalloff = 0f;
    private float lastInput = 0.0f;
    [Header("Used for small screeches")]
#if UNITY_EDITOR
    [SerializeField, Multiline] private string creekNotes = "Small screeches plays a screeching sounds every once in a while.\nThey only play while turning the valve, and have set spaces between them.";
#endif
    [SerializeField] private AudioClip[] screechClips;
    [SerializeField, Range(0f, 1f)] private float minVol = 0.2f;
    [SerializeField, Range(0.5f, 2f)] private float minPitch = 0.85f;
    [SerializeField, Range(0.5f, 2f)] private float maxPitch = 1.2f;
    [SerializeField, Min(0f)] private float clipWaitMin = 0.2f;
    [SerializeField, Min(0f)] private float clipWaitMax = 0.4f;
    private bool canScreechAgain = true;
    private float myCurrentWait = 0.0f;
    private float effectiveClipWait = 0.3f;


    // Awake is called real early
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        source.playOnAwake = !randomSmallScreeches;
        source.loop = !randomSmallScreeches;
        if (randomSmallScreeches)
        {
            effectiveClipWait = Random.Range(clipWaitMin, clipWaitMax);
            source.pitch = Random.Range(minPitch, maxPitch);
            source.volume = Random.Range(minVol, maxVol);
            return;
        }
        source.volume = 0;
        source.pitch = 1;
        source.clip = loopingClip;
        source.Play();
    }

    private void Update()
    {
        if (randomSmallScreeches)
        {
            if (source.isPlaying || canScreechAgain) return;

            myCurrentWait += Time.deltaTime;

            if (myCurrentWait > effectiveClipWait)
            {
                canScreechAgain = true;
            }
        }
        else
        {
            float newVol = (1 - Mathf.Clamp(currentFalloff, 0f, falloffSeconds) / falloffSeconds) * maxVol;

            source.volume = newVol;

            currentFalloff += Time.deltaTime;
        }
    }

    public void ValveCreeking(float value)
    {
        if (randomSmallScreeches)
        {
            if (!canScreechAgain) return;
            // Make sure creek don't happen too often
            canScreechAgain = false;
            effectiveClipWait = Random.Range(clipWaitMin, clipWaitMax);

            source.pitch = Random.Range(minPitch, maxPitch);
            source.volume = Random.Range(minVol, maxVol);
            source.clip = screechClips[Random.Range(0, screechClips.Length)];
            source.Play();
        }
        else
        {
            currentFalloff = 0f;

            float speed = Mathf.Abs(value - lastInput);

            lastInput = value;

            source.volume = (Mathf.Clamp(speed, minRotationSpeed, maxRotationSpeed) - minRotationSpeed) / maxRotationSpeed * maxVol;
        }
    }
}
