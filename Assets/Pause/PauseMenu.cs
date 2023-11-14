using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;
    public GameObject promptPanel;

    void Start()
    {
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ShowPanel()
    {
        pausePanel.SetActive(true);
    }

    public void HidePanel()
    {
        pausePanel.SetActive(false);
    }

    public void ResumeGame()
    {
        HidePanel();
        Time.timeScale = 1;
        isPaused = false;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void PauseGame()
    {
        ShowPanel();
        Time.timeScale = 0;
        isPaused = true;
    }

    public void PromptPanelShow()
    {
        promptPanel.SetActive(true);
    }

    public void PromptPanelHide()
    {
        promptPanel.SetActive(false);
    }
}
