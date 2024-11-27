using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider progressBar;
    public TMP_Text progressText;
    public Image backgroundImage;
    public TMP_Text tipText;

    [Header("Backgrounds and Tips")]
    public Sprite[] backgrounds;
    public string[] tips;

    private void Start()
    {
        int backgroundIndex = PlayerPrefs.GetInt("BackgroundIndex", 0);

        if (backgrounds.Length > 0 && backgroundIndex < backgrounds.Length)
        {
            backgroundImage.sprite = backgrounds[backgroundIndex];
        }
        else
        {
            Debug.LogError("Background image not assigned or index is out of range.");
        }

        if (tips.Length > 0)
        {
            int randomIndex = Random.Range(0, tips.Length);
            tipText.text = tips[randomIndex];
        }

        if (progressBar != null)
        {
            progressBar.value = 0f;
        }

        if (progressText != null)
        {
            progressText.text = "0%";
        }

        string sceneName = PlayerPrefs.GetString("NextScene");
        if (!string.IsNullOrEmpty(sceneName))
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }
        else
        {
            Debug.LogError("Scene name is not set in PlayerPrefs!");
        }
    }

    public static void LoadScene(string sceneName, int backgroundIndex)
    {
        PlayerPrefs.SetString("NextScene", sceneName);
        PlayerPrefs.SetInt("BackgroundIndex", backgroundIndex);

        SceneManager.LoadScene("Loading_Screen");
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name not set in PlayerPrefs!");
            yield break;
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float targetProgress = 1.0f;
        float currentProgress = 0.0f;
        float speed = 0.1f; 

        while (!operation.isDone)
        {
            currentProgress = Mathf.MoveTowards(currentProgress, targetProgress, speed * Time.deltaTime);

            if (progressBar != null)
            {
                progressBar.value = currentProgress;
            }

            if (progressText != null)
            {
                progressText.text = $"{(currentProgress * 100):0}%";
            }

            if (currentProgress >= 1.0f)
            {
                progressText.text = "100% - Loading Complete!";
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

        GameObject mainMenu = GameObject.Find("MainMenu");
        if (mainMenu != null)
        {
            mainMenu.SetActive(true);
        }
    }
}