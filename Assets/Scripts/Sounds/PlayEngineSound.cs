using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEngineSound : MonoBehaviour {
    void OnEnable() {
        GameEventManager.CompleteGaugeEvent += EngineSound;
    }

    void OnDisable() {
        GameEventManager.CompleteGaugeEvent -= EngineSound;
    }
    [SerializeField] PlayAudio AudioPlayer;
    void Start(){
        AudioPlayer = GetComponent<PlayAudio>();
    }
    void EngineSound() {
        AudioPlayer.Play();
    }
}
