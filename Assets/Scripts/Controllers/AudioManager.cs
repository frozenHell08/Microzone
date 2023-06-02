using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer mixer;

    void Start() {
        if (PlayerPrefs.HasKey("MasterVolume")) {
            mixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
        }

        if (PlayerPrefs.HasKey("MusicVolume")) {
            mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        }
    }
}
