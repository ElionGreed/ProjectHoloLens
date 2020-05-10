using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SceneSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject menuPanel;
    public GameObject optionsPanel;
    public GameObject creditsPanel;
    public UnityEngine.UI.Slider volSlider;

    //private async void TransitionToScene()
    //{
    //    IMixedRealitySceneSystem sceneSystem = MixedRealityToolkit.Instance.GetService<IMixedRealitySceneSystem>();
    //    ISceneTransitionService transition = MixedRealityToolkit.Instance.GetService<ISceneTransitionService>();

    //    ListenToSceneTransition(sceneSystem, transition);

    //    await transition.DoSceneTransition(
    //            () => sceneSystem.LoadContent("TestScene1")
    //        );
    //}

    //private async void ListenToSceneTransition(IMixedRealitySceneSystem sceneSystem, ISceneTransitionService transition)
    //{
    //    transition.SetProgressMessage("Starting transition...");

    //    while (transition.TransitionInProgress)
    //    {
    //        if (sceneSystem.SceneOperationInProgress)
    //        {
    //            transition.SetProgressMessage("Loading scene...");
    //            transition.SetProgressValue(sceneSystem.SceneOperationProgress);
    //        }
    //        else
    //        {
    //            transition.SetProgressMessage("Finished loading scene...");
    //            transition.SetProgressValue(1);
    //        }

    //        await Task.Yield();
    //    }
    //}
    public void LoadDung(string sceneName)
    {
        Debug.Log("Game started");
        SceneManager.LoadScene(sceneName);
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
    }
}
