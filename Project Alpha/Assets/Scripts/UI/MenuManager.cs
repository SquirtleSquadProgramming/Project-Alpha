using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    #region Variables
    public static MenuManager instance;
    public static MenuState currentState = MenuState.Closed;
    public GameObject[] Menus;
    public bool isMainMenu = false;
    private UISliders[] sliders;
    #endregion

    void Awake()
    {
        instance = this;

        foreach (GameObject Menu in Menus)
            Menu.SetActive(false);

        if (isMainMenu)
        {
            currentState = MenuState.Main;
            Menus[(int)currentState].SetActive(true);
        }

        List<UISliders> _sliders = new List<UISliders>();
        foreach (GameObject menu in Menus)
            foreach (UISliders uislider in menu.GetComponentsInChildren<UISliders>())
                _sliders.Add(uislider);

        sliders = _sliders.ToArray();

        Refresh();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ApplySettings();
            if (currentState != MenuState.Closed)
                Menus[(int)currentState].SetActive(false);

            if (currentState == MenuState.Closed || currentState == MenuState.Main)
                currentState = MenuState.Paused;
            else currentState = decrement(currentState);

            if (currentState != MenuState.Closed) {
                Menus[(int)currentState].SetActive(true);
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
            }
            else {
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void SetMenu(int state)
    {
        if (currentState != MenuState.Closed)
            Menus[(int)currentState].SetActive(false);
        
        currentState = (MenuState)state;
        
        if (currentState != MenuState.Closed)
            Menus[(int)currentState].SetActive(true);
    }

    public void Exit()
        => Application.Quit();
    public void Play()
        => SceneManager.LoadScene("Main");

    public void ApplySettings()
    {
        float[] vals = new float[sliders.Length];
        for (int i = 0; i < sliders.Length; i++)
            vals[i] = sliders[i].slider.value;
        PlayerData.SliderValues = vals;
        Refresh();
    }

    public void Refresh()
    {
        Camera.main.fieldOfView = PlayerData.FieldOfView;

        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].slider.value = PlayerData.SliderValues[i];
            sliders[i].UpdateSlider();
        }
    }

    public MenuState decrement(MenuState state)
    {
        if (isMainMenu && state == MenuState.Paused)
            return MenuState.Main;
        switch (state)
        {
            case MenuState.Paused:
            default:
                return MenuState.Closed;
        }
    }
}

public enum MenuState: int // MenuState.Main always needs to go last.
{
    Closed = -1, Paused = 0, Main
}  