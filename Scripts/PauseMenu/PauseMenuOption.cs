using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuOption : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider bgmSlider;
    public Slider soundSlider;
    public Slider brightnessSlider;
    public Button backButton;
    public Button applyButton;
    public Button resetButton;
    public Button mainMenuButton;

    [Header("Audio Settings")]
    public AudioSource bgmSource;
    public AudioSource soundSource;
    public AudioClip interactionClip;

    [Header("Brightness Settings")]
    public Light sceneLight;

    private float defaultBGMVolume = 1.0f;
    private float defaultSoundVolume = 1.0f;
    private float defaultBrightness = 1.0f;

    private float currentBGMVolume;
    private float currentSoundVolume;
    private float currentBrightness;

    void Start()
    {
        LoadSettings();

        if (bgmSlider != null)
            bgmSlider.value = currentBGMVolume;

        if (soundSlider != null)
            soundSlider.value = currentSoundVolume;

        if (brightnessSlider != null)
            brightnessSlider.value = currentBrightness;

        ApplySettings();

        if (bgmSlider != null)
        {
            bgmSlider.onValueChanged.AddListener(value =>
            {
                currentBGMVolume = value;
                if (bgmSource != null)
                    bgmSource.volume = currentBGMVolume;
            });
        }

        if (soundSlider != null)
        {
            soundSlider.onValueChanged.AddListener(value =>
            {
                currentSoundVolume = value;
                if (soundSource != null)
                    soundSource.volume = currentSoundVolume;
            });
        }

        if (brightnessSlider != null)
        {
            brightnessSlider.onValueChanged.AddListener(value =>
            {
                currentBrightness = value;
                if (sceneLight != null)
                    sceneLight.intensity = currentBrightness;
            });
        }

        if (backButton != null)
            backButton.onClick.AddListener(BackToPauseMenu);

        if (applyButton != null)
            applyButton.onClick.AddListener(SaveAndApplySettings);

        if (resetButton != null)
            resetButton.onClick.AddListener(ResetToDefault);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(BackToMainMenu);
    }

    private void LoadSettings()
    {
        currentBGMVolume = PlayerPrefs.GetFloat("BGMVolume", defaultBGMVolume);
        currentSoundVolume = PlayerPrefs.GetFloat("SoundVolume", defaultSoundVolume);
        currentBrightness = PlayerPrefs.GetFloat("Brightness", defaultBrightness);
    }

    private void ApplySettings()
    {
        if (bgmSource != null)
            bgmSource.volume = currentBGMVolume;

        if (soundSource != null)
            soundSource.volume = currentSoundVolume;

        if (sceneLight != null)
            sceneLight.intensity = currentBrightness;
    }

    private void SaveAndApplySettings()
    {
        PlayerPrefs.SetFloat("BGMVolume", currentBGMVolume);
        PlayerPrefs.SetFloat("SoundVolume", currentSoundVolume);
        PlayerPrefs.SetFloat("Brightness", currentBrightness);
        PlayerPrefs.Save();

        ApplySettings();
    }

    private void ResetToDefault()
    {
        currentBGMVolume = defaultBGMVolume;
        currentSoundVolume = defaultSoundVolume;
        currentBrightness = defaultBrightness;

        if (bgmSlider != null)
            bgmSlider.value = defaultBGMVolume;

        if (soundSlider != null)
            soundSlider.value = defaultSoundVolume;

        if (brightnessSlider != null)
            brightnessSlider.value = defaultBrightness;

        ApplySettings();
    }

    private void BackToPauseMenu()
    {
        gameObject.SetActive(false);
    }

    private void BackToMainMenu()
    {
        LoadingScreen.LoadScene("MainMenu", 3);
        gameObject.SetActive(false);
    }

    public void PlayInteractionSound()
    {
        if (soundSource != null && interactionClip != null)
        {
            soundSource.PlayOneShot(interactionClip, currentSoundVolume);
        }
    }
}
