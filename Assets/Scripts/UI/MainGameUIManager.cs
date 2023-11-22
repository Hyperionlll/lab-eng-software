using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainGameUIManager : MonoBehaviour
{
    public static bool isPaused = false;
    [SerializeField] private GameObject menuCanvas, pauseMenu, optionsMenu, deathScreen, gameWonScreen;

    private void Start()
    {
        optionsMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                optionsMenu.SetActive(false);
                AudioManager.instance.PlayPauseSound();
            }
            else // isPaused == false
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                optionsMenu.SetActive(false);
                AudioManager.instance.PlayUnpauseSound();
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    public void Options()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void BackFromOptions()
    {
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        isPaused = false;
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void DeathScreen()
    {
        deathScreen.SetActive(true);
    }

    public void ShowGameWonScreen()
    {
        Time.timeScale = 0;
        gameWonScreen.SetActive(true);
    }
}
