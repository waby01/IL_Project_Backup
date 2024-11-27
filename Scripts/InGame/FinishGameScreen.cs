using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGameScreen : MonoBehaviour
{
    public GameObject finishScreen; // Referensi ke GameObject untuk layar selesai

    void Start()
    {
       
    }

    public void setup()
    {
        finishScreen.SetActive(true); // Menampilkan layar selesai

        // Menampilkan kursor mouse
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // Membuka kunci kursor dari tengah layar
    }

    public void restartButton()
    {
        Time.timeScale = 1; // Reset waktu saat restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload scene saat ini
    }

    public void nextButton()
    {
        SceneManager.LoadScene(3);
    }
}
