using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SceneSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject menuPanel;
    public GameObject optionsPanel;
    public GameObject creditsPanel;
    public GameObject loadingPanel;
    
    public UnityEngine.UI.Slider volSlider;


    public void LoadGame()
    {
        StartCoroutine(LoadingWait());
    }

    public IEnumerator LoadingWait()
    {
        loadingPanel.SetActive(true);
        menuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainScene");
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
        optionsPanel.SetActive(false);
        menuPanel.SetActive(false);
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
        creditsPanel.SetActive(false);
    }
}
