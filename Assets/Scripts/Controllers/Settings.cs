using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class Settings : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer mixer;

    [Header("Audio Labels")]
    [SerializeField] private TMP_Text masterVol;
    [SerializeField] private TMP_Text musicVol;
    [SerializeField] private TMP_Text sfxVol;

    [Header("Audio Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    public Toggle fullScreenTog;
    public TMP_Dropdown resDropdown;

    Resolution[] resolutions;

    void Start() {
        fullScreenTog.isOn = Screen.fullScreen;

        // --------------------------
        resolutions = Screen.resolutions;

        List<string> options = new List<string>();
        int currentIndex = 0;
        foreach (var res in resolutions) {
            string option = res.width + " x " + res.height;
            options.Add(option);

            if ((res.width == Screen.width) && (res.height == Screen.height)) {
                currentIndex = Array.IndexOf(resolutions, res);
            }
        }

        resDropdown.AddOptions(options);
        resDropdown.value = currentIndex;
        resDropdown.RefreshShownValue();
        // --------------------------

        float vol = 0f;

        mixer.GetFloat("MasterVolume", out vol);
        masterSlider.value = vol;

        mixer.GetFloat("MusicVolume", out vol);
        musicSlider.value = vol;

        masterVol.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();
        musicVol.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
    }

    public void ApplyGraphics(GameObject panel) {
        Resolution selected = resolutions[resDropdown.value];
        Screen.SetResolution(selected.width, selected.height, fullScreenTog.isOn);

        panel.SetActive(false);
    }

    public void SetMasterVolume() {
        masterVol.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();

        mixer.SetFloat("MasterVolume", masterSlider.value);
        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
    }

    public void SetMusicVolume() {
        musicVol.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();

        mixer.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }
}
