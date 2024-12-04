using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayJumpscareSound : MonoBehaviour {
    [SerializeField] AudioSource AudioPlayer;
    [SerializeField] AudioClip Clip;
    bool PlaySound;
    void Start(){
        AudioPlayer = GetComponent<AudioSource>();
        PlaySound = false;
    }
    void Update(){
        if (PlaySound){
            AudioPlayer.PlayOneShot(Clip); 
            PlaySound = false;
        }
    }
}
