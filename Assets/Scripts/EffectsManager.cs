using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EffectsManager : MonoBehaviour
{
    [SerializeField] Volume postProcessingVolume;
    int depth;
    [SerializeField] VolumeProfile surfaceVolume;
    [SerializeField] VolumeProfile underwaterVolume;
    [SerializeField] Transform mainCamera;
    
    void Update()
    {
        if (mainCamera.position.y < depth)
        {
            EnableEffects(true);
        }
        else
        {
            EnableEffects(false);
        }
    }

    void EnableEffects(bool active)
    {
        if (active)
        {
            postProcessingVolume.profile = underwaterVolume;
        }
        else
        {
            postProcessingVolume.profile = surfaceVolume;
        }
    }
    
}
