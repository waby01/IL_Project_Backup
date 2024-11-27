using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    public GameOverScreen gameOverScreen;

    // Flag untuk memastikan game over hanya dipanggil sekali
    private bool gameOverCalled = false;

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 4)
            {
                timerText.color = Color.red;
            }
        }
        else if (remainingTime <= 0 && !gameOverCalled)
        {
            remainingTime = 0;
            gameOverCalled = true; // Tandai bahwa game over sudah dipanggil
            gameOver(); // Panggil game over hanya sekali
            Time.timeScale = 0; // Hentikan waktu
        }

        // Menampilkan timer
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void gameOver()
    {
        gameOverScreen.setup("Time is out!");
    }
}
