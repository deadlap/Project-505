using UnityEngine;
using Random = UnityEngine.Random;

public class FishKnocking : MonoBehaviour
{
    AudioSource audiosource;

    float timer;
    float randomTime;
    float newTime;

    void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        randomTime = newTime;
        if (timer > randomTime) 
        {
            audiosource.PlayOneShot(audiosource.clip);
            newTime = Random.RandomRange(6, 9);
            timer = 0;
        }
    }
}
