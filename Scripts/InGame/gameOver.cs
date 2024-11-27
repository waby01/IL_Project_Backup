using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1; // Pastikan waktu di-reset agar game berjalan normal
        SceneManager.LoadScene("SceneSubmarine"); // Ganti "SceneSubmarine" dengan nama scene gameplay Anda
    }
}
