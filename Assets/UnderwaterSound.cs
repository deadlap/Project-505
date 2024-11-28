using UnityEngine;

public class UnderwaterSound : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] float minVolume, maxVolume;
    void Start()
    {
        GetComponent<AudioSource>();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.volume = maxVolume;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.volume = minVolume;
        }
    }
}
