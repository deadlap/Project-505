using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EffectsManager : MonoBehaviour
{
    [SerializeField] Volume postProcessingVolume;
    [SerializeField] VolumeProfile surfaceVolume;
    [SerializeField] VolumeProfile underwaterVolume;
    [SerializeField] Transform mainCamera;
    
    int surfaceDepth;
    float minDepth = 0f;
    float maxDepth = 100f;
    
    void Update()
    {
        if (mainCamera.position.y < surfaceDepth)
        {
            EnableEffects(true);
        }
        else
        {
            EnableEffects(false);
        }
        // Depth();
    }

    void EnableEffects(bool active)
    {
        if (active)
        {
            postProcessingVolume.profile = underwaterVolume;
            RenderSettings.fog = true;
        }
        else
        {
            postProcessingVolume.profile = surfaceVolume;
            RenderSettings.fog = false;
        }
    }

    void Depth()
    {
        // float cameraY = mainCamera.transform.position.y;
        // float t = Mathf.InverseLerp(minDepth, maxDepth, cameraY);
    }
    
    
}
