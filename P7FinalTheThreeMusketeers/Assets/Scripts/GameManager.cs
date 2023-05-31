using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject titleScreen;
    private bool paused;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI timerText;
    public Button restartButton;
    public bool isGameActive;
    public float timer = 30;
    // Start is called before the first frame update
    public void StartGame()
    {
        titleScreen.gameObject.SetActive(false);
        isGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
        if (isGameActive && timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Round(timer);
        }
        if (timer < 0)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }
    public void YouWin()
    {
        winText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
