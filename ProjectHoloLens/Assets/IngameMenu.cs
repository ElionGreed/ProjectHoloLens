using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IngameMenu : MonoBehaviour
{
    public TextMeshProUGUI txtScore;
    public GameObject panelGameover;
    public GameObject panelLoading;

    private int score;

    void Start()
    {
        txtScore.text = "0";
    }

    public void OnPlayerDead()
    {
        //score =
        panelGameover.SetActive(true);
        txtScore.text = score.ToString();
    }

    public void TryAgain()
    {
        StartCoroutine(LoadingWait());
    }

    public IEnumerator LoadingWait()
    {
        panelLoading.SetActive(true);
        panelGameover.SetActive(false);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainScene");
    }
}
