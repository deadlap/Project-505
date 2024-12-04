using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPipeBreakingSound : MonoBehaviour {
    void OnEnable() {
        GameEventManager.EndEvent += PlayBreakSound;
    }

    void OnDisable() {
        GameEventManager.EndEvent -= PlayBreakSound;
    }
    [SerializeField] PlayAudio AudioPlayer;
    void Start(){
        AudioPlayer = GetComponent<PlayAudio>();
    }
    void PlayBreakSound() {
        Invoke("AudioPlayer.Play()", 3);
    }
}
