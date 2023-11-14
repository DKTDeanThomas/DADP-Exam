using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject keybindsPanel;
    public Button playButton;
    public Button quitButton;

    void Start()
    {
        optionsPanel.SetActive(false);
        keybindsPanel.SetActive(false);
    }

    public void LoadScene1()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void HideOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleKeybindsPanel()
    {
        bool isActive = keybindsPanel.activeSelf;

        keybindsPanel.SetActive(!isActive);
        playButton.gameObject.SetActive(isActive);
        quitButton.gameObject.SetActive(isActive);
    }
}
