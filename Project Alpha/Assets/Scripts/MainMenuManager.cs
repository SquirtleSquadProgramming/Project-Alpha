using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject SettingsUI;
    public void Exit() => Application.Quit();
    public void Play() => SceneManager.LoadScene("Main");
    public void Settings()
    {
        MainMenuUI.SetActive(false);
        SettingsUI.SetActive(true);
    }
    public void Back()
    {
        MainMenuUI.SetActive(true);
        SettingsUI.SetActive(false);
    }
}
