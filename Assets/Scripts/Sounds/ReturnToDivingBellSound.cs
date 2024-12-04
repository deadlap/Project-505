using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToDivingBellSound : MonoBehaviour {
    [SerializeField] PlayAudio AudioPlayer;
    [SerializeField] AudioClip AC;
    [SerializeField] float InvokeTime;
    void OnEnable() {
        GameEventManager.BrokenEvent += VoiceOver;
    }
    void OnDisable() {
        GameEventManager.BrokenEvent -= VoiceOver;
    }
    void VoiceOver() {
        Invoke("PlayVoiceover", InvokeTime);
    }
    void PlayVoiceover(){
        AudioPlayer.Play();
    }
}
