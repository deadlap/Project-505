using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEngineSound : MonoBehaviour {
    void OnEnable() {
        GameEventManager.EndEvent += EngineSound;
    }

    void OnDisable() {
        GameEventManager.EndEvent -= EngineSound;
    }
    [SerializeField] PlayAudio AudioPlayer;
    void Start(){
        AudioPlayer = GetComponent<PlayAudio>();
    }
    void EngineSound() {
        AudioPlayer.Play();
    }
}
