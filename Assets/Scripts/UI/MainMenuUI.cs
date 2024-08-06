using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Panels-Container")]
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private GameObject creditPanel;

    [Header("Menu UI properties")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backCreditButton;

    private void OnEnable()
    {
        ShowMenu(true);
        startButton.onClick.AddListener(GoToGameplayScene);
        creditsButton.onClick.AddListener(OpenCreditsMenu);
        exitButton.onClick.AddListener(ExitGame);
        backCreditButton.onClick.AddListener(ExitCredit);
    }

    private void ExitCredit()
    {
        ShowMenu(true);
    }

    private void OpenCreditsMenu()
    {
        ShowMenu(false);
    }

    private void GoToGameplayScene()
    {
        SceneManager.LoadScene("Gameplay");
    }

    void ShowMenu(bool pShow)
    {
        menuPanel.SetActive(pShow);
        creditPanel.SetActive(!pShow);
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}