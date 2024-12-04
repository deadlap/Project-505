using UnityEngine;

public class UnderwaterStuff : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject underwaterEffect;
    [SerializeField] float minVolume, maxVolume;

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.volume = maxVolume;
            underwaterEffect.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.volume = minVolume;
            underwaterEffect.SetActive(false);
            if (GameEventManager.INSTANCE.GaugeDone) {
                GameEventManager.INSTANCE.StartBrokenSequence();
            }
        }
    }
}
