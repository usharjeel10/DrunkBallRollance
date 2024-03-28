using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
public class mainMenuBehaviour : MonoBehaviour
{
    [Header("Menu-Buttons")]
    [SerializeField] private Button playBtn;
    [SerializeField] private Button continuewBtn;
    [SerializeField] private Button descriptionBtn;
    [SerializeField] private Button aboutUsBtn;
    [SerializeField] private Button quitBtn;

    [Header("Menu-UI")]
    [SerializeField] private GameObject descriptionUI;
    [SerializeField] private GameObject AboutUsUI;
    private int currentScene = 1;
    private void Awake()
    {
        playBtn.onClick.AddListener(PlayButtonClicked);
        continuewBtn.onClick.AddListener(ContinueButtonClicked);
        descriptionBtn.onClick.AddListener(DescriptionButtonClicked);
        aboutUsBtn.onClick.AddListener(AboutUsButtonClicked);
        quitBtn.onClick.AddListener(QuitButtonClicked);
        if (PlayerPrefs.HasKey("sceneIndex"))
        {
            currentScene = PlayerPrefs.GetInt("sceneIndex");
        }
        else
        {
            continuewBtn.gameObject.SetActive(false);
        }
    }
    private void PlayButtonClicked()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }

    private void ContinueButtonClicked()
    {
        SceneManager.LoadScene(currentScene);
    }

    private void DescriptionButtonClicked()
    {
        descriptionUI.SetActive(true);
       // InitializeAdsManager.instance.ShowBanner();
    }

    private void AboutUsButtonClicked()
    {
        AboutUsUI.SetActive(true);
    }

    private void QuitButtonClicked()
    {
        Application.Quit();
    }

}
