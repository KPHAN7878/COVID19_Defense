using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSpeed : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject deathMenuUI;

    public GameObject winMenuUI;

    void Update()
    {
        if (PlayerAttributes.health < 1)
        {
            DeathScene();
        }

        if (PlayerAttributes.roundTracker > 50)
        {
            WinScene();
        }
    }
    void Start()
    {
        Time.timeScale = 1f;
    }

    //Sets the time scale back to speed before pausing and closes pause menu ui
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    //Sets the time scale to 0 and brings up pause menu ui
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    //Sets the time scale back to normal speed and load the Main menu(Scene 2 in the build index currently)
    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

    //Sets the time scale back to normal speed and resets level completely, loads current scene again.
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Increases the time scale by a multiplier of 2, games goes twice as fast
    public void SpeedUp2()
    {
        Time.timeScale = 2f;
    }

    //Increases the time scale by a multiplier of 4, games goes four times as fast
    public void SpeedUp4()
    {
        Time.timeScale = 4f;
    }

    //Decreases the time scale to normal speed
    public void RegSpeed()
    {
        Time.timeScale = 1f;
    }

    public void DeathScene()
    {
        deathMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void WinScene()
    {
        winMenuUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = true;
    }
}
