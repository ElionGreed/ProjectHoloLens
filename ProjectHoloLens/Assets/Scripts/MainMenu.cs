using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject menuPanel;
    public GameObject optionsPanel;
    public UnityEngine.UI.Slider volSlider;

    public void LoadDung(string sceneName)
    {
        Debug.Log("Game started");
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Game quit");
        Application.Quit();
    }

    public void ShowOptions()
    {
        optionsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void ChangeVol()
    {
        AudioListener.volume = volSlider.value;
    }

    public void BackToMenu()
    {
        menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }
}
