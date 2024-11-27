using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsLoader : MonoBehaviour
{
    private void Start()
    {
        // Brightness
        float brightness = PlayerPrefs.GetFloat("brightnessLevel", 1.0f);
        RenderSettings.ambientLight = Color.white * brightness;

        // Quality
        int qualityLevel = PlayerPrefs.GetInt("qualityLevel", QualitySettings.GetQualityLevel());
        QualitySettings.SetQualityLevel(qualityLevel);

        // Fullscreen
        bool isFullScreen = PlayerPrefs.GetInt("isFullScreen", 1) == 1;
        Screen.fullScreen = isFullScreen;

        // Resolution
        int resolutionIndex = PlayerPrefs.GetInt("resolutionIndex", 0);
        Resolution[] resolutions = Screen.resolutions;
        if (resolutionIndex < resolutions.Length)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, isFullScreen);
        }
    }
}