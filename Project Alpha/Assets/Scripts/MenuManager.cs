using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            if (currentState != MenuState.Closed)
                Menus[(int)currentState].SetActive(true);
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

public enum MenuState: int // Closed must be the last element in the list
{
    Paused = 0, Main = 1, Closed
}  