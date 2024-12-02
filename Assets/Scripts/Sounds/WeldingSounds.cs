using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldingSounds : MonoBehaviour
{
    [SerializeField] private AudioSource sparkSource;
    [SerializeField] private AudioSource buzzSource;


    /// <summary>
    /// Call when turning on welding gun
    /// </summary>
    public void BeginSpark()
    {
        sparkSource.Play();
    }

    /// <summary>
    /// Call when welding gun turns off
    /// </summary>
    public void EndSpark()
    {
        sparkSource.Stop();
        buzzSource.Stop();
    }

    /// <summary>
    /// Call when beginning to weld correct place
    /// </summary>
    public void WeldCorrect()
    {
        buzzSource.Play();
        if (!sparkSource.isPlaying) sparkSource.Play();
    }

    /// <summary>
    /// Call when no longer welding correct place
    /// </summary>
    public void EndWeld()
    {
        buzzSource.Stop();
    }
}
