using UnityEngine;

public class LowHumTone : MonoBehaviour
{
    [SerializeField] float buildUpMultiplier;
    float buildUpTime;
    [SerializeField] bool isHumming;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0;
    }

    void OnEnable()
    {
        GameEventManager.BrokenEvent += PlayHum;
    }

    void OnDisable()
    {
        GameEventManager.BrokenEvent -= PlayHum;
    }
    
    void PlayHum()
    {
        audioSource.Play();
        isHumming = true;
    }
    
    void Update()
    {
        if(!isHumming) return;
        buildUpTime += buildUpMultiplier * Time.deltaTime;
        audioSource.volume = Mathf.Clamp01(buildUpTime);
        print(buildUpTime);
    }
}
