using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance;
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GameOver()
    {
        scoreText.text = "";
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
