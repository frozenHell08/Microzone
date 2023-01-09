using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMP_Dropdown resolutionDropdown;
    public TMP_Text txt;
    Resolution[] resolutions;
     

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 1; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;

            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
            resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }

        }
       
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    
       
        // txt.text = $"reso : {Screen.currentResolution} \npref : {PlayerPrefs.GetInt("resW")} x {PlayerPrefs.GetInt("resH")}";
    }


    
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        if (!Screen.fullScreen) 
        {
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            // PlayerPrefs.SetString("resolution", "full");
            // PlayerPrefs.SetInt("resW", resolution.width);
            // PlayerPrefs.SetInt("resH", resolution.height);
            // txt.text = $"reso : {Screen.currentResolution} \npref : {PlayerPrefs.GetInt("resW")} x {PlayerPrefs.GetInt("resH")}" +
            //             $"\nset? : {resolution.width} x {resolution.height} \nfullscreen";
        }  
        else
        {
            Screen.SetResolution(resolution.width, resolution.height, !Screen.fullScreen);
            Toggle(false); //changeee this
            // PlayerPre/fs.SetString("resolution", "not full");
            // PlayerPrefs.SetInt("resW", resolution.width);
            // PlayerPrefs.SetInt("resH", resolution.height);
            // txt.text = $"reso : {Screen.currentResolution} \npref : {PlayerPrefs.GetInt("resW")} x {PlayerPrefs.GetInt("resH")}" +
            //             $"\nset? : {resolution.width} x {resolution.height}  \nnot fullscreen";
        }
        
    }


    public void Setvolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void Toggle(bool turn)
    {
        turn = true;
    }
}
