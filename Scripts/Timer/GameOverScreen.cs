using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameOverText;

    public void setup(string message)
    {
        Debug.Log("Game Over setup called");

        gameObject.SetActive(true); // Aktifkan UI Game Over

        if (gameOverText != null)
        {
            gameOverText.text = message;  // Set pesan Game Over
            Debug.Log("Game Over Text Set: " + message);
        }
        else
        {
            Debug.LogError("Game Over Text not assigned!");
        }

        Cursor.visible = true; // Tampilkan kursor
        Cursor.lockState = CursorLockMode.None; // Lepaskan kursor
    }


    public void restartButton()
    {
        Time.timeScale = 1; // Reset waktu saat restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload scene saat ini
    }
}
