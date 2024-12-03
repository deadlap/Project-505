using UnityEngine;

public class FogController : MonoBehaviour
{
    float changeTime;
    [SerializeField] float changeDuration;
    Color newColor;
    Color surfaceColor = new(0f, .79f, 1);
    Color depthColor = new(0, 0.1f, 0.11f);
    
    
    void Start()
    {
        newColor = surfaceColor;
    }

    void Update()
    {
        newColor = Color.Lerp(newColor, depthColor, Mathf.Clamp01(Time.deltaTime / changeDuration));
        RenderSettings.fogColor = newColor;
    }
}
