using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWeldingDoneClip : MonoBehaviour {
    [SerializeField] PlayAudio AudioPlayer;
    [SerializeField] AudioClip AC;
    void OnEnable() {
        GameEventManager.CompleteWeldingEvent += VoiceOver;
    }
    void OnDisable() {
        GameEventManager.CompleteWeldingEvent -= VoiceOver;
    }
    void VoiceOver() {
        AudioPlayer.Play(AC);
    }
}
