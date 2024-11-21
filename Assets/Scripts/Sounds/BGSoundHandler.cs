using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SpookyAudio
{
    [Serializable]
    public class SoundSetup
    {
        public SoundTypes soundType;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1;
        public bool loop = true;
        public bool playOnAwake = false;
        [HideInInspector] public AudioSource audioSource;
    }

    [Serializable]
    public enum SoundTypes
    {
        UnderwaterLoop,
        Music
    }

    public class BGSoundHandler : MonoBehaviour
    {
        [SerializeField, Tooltip("The SFX/Music clip and what they should be known as")] private SoundSetup[] sfxSources;

        // For checking and warning about duplicates
        private Dictionary<SoundTypes, int> dupePass = new Dictionary<SoundTypes, int>();

        private void Awake()
        {
            // For checking and warning about duplicates
            bool dupes = false;

            foreach (SoundSetup sfxSource in sfxSources)
            {
                // To avoid duplicates
                if (dupePass.ContainsKey(sfxSource.soundType)) { dupePass[sfxSource.soundType]++; dupes = true; continue; } // If a duplicate is registered, count it and don't react on it
                else dupePass.Add(sfxSource.soundType, 1);

                AddAudioClip(sfxSource);
            }

            // For checking and warning about duplicates
            if (dupes)
            {
                string errorMessage = "Duplicate Sound Types registered:";

                // For each duplicate, list them out in a string with the amount
                foreach (SoundTypes key in dupePass.Keys)
                {
                    if (dupePass[key] <= 1) continue; // If this is not a duplicate, do not add it to the message as a duplicate
                    errorMessage += $"\n{key} has {dupePass[key]} instances";
                }

                // Send the error message
                Debug.LogError(errorMessage);
            }
        }

        public void AddAudioClip(SoundSetup soundToAdd)
        {
            AudioSource alteredSource;
            if (dupePass.ContainsKey(soundToAdd.soundType))
            {
                Debug.LogWarning($"A {soundToAdd.soundType} sound already exists. Transfering this ones settings to that one");
                // FIGURE OUT HOW TO GET CUSTOM CLASS BY KEY
                return;
            }
            else
            {
                alteredSource = gameObject.AddComponent<AudioSource>();
            }

            alteredSource.playOnAwake = soundToAdd.playOnAwake;
            alteredSource.clip = soundToAdd.clip;
            alteredSource.loop = soundToAdd.loop;
            alteredSource.volume = soundToAdd.volume;
            soundToAdd.audioSource = alteredSource;
        }

        /// <summary>
        /// Toggles a sound on and off
        /// </summary>
        /// <param name="whichSound"></param>
        /// <param name="turnOn"></param>
        //public void ToggleSound(SoundTypes whichSound, bool turnOn)
        //{
        //    if (turnOn)
        //    {
        //        if (sfxSources[whichSound].isPlaying) return; // Already playing can't turn on what is running
        //        sfxSources[whichSound].Play();
        //        return;
        //    }

        //    if (!sfxSources[whichSound].isPlaying) return; // Already isn't playing, no reason to try and stop it from playing
        //    sfxSources[whichSound].Stop();
        //}

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

