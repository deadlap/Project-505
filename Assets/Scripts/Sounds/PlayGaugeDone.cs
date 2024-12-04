using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayGaugeDone : MonoBehaviour {
    void OnEnable() {
        GameEventManager.CompleteGaugeEvent += Voiceover;
    }

    void OnDisable() {
        GameEventManager.CompleteGaugeEvent -= Voiceover;
    }
    [SerializeField] PlayAudio AudioPlayer;
    void Start(){
        AudioPlayer = GetComponent<PlayAudio>();
    }
    void Voiceover() {
        Invoke("PlayVoiceover",5.5f);
    }
    void PlayVoiceover(){
        AudioPlayer.Play();
    }
}
