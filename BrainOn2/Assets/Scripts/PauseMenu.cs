using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;


    void Update()
    {
                // Si on appuie sur ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Si le jeu est déjà en pause
            if (GameIsPaused)
            {
                Resume();
            }
            // Si le jeu n'est pas en pause
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        // Désactive le menu Pause
        pauseMenuUI.SetActive(false);
        // Reprend le temps réel du jeu
        Time.timeScale = 1f;

        GameIsPaused = false;
    }

    void Pause()
    {
            // Active le menu Pause
        pauseMenuUI.SetActive(true);
            // Arrête le temps du jeu en fond
        Time.timeScale = 0f;

        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        // Reprend le temps réel du jeu
        Time.timeScale = 1f;
        // Mène vers une autre scène appelé "MainMenu" (/!\check les build settings qu'il s'agisse du bon nom)
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
