using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public static MenuState currentState = MenuState.Closed;
    public GameObject[] Menus;
    public bool isMainMenu = false;

    // Start is called before the first frame update
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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

    public void Exit() => Application.Quit();
    public void Play() => SceneManager.LoadScene("Main");

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