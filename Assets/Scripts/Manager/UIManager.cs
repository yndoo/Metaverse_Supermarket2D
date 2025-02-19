using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    GameUI
}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    GameUI gameUI;
    public UIState currentState;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        gameUI = GetComponentInChildren<GameUI>();
        gameUI.Init(this);

        ChangeState(UIState.GameUI);
    }

    public void ChangeState(UIState state)
    {
        currentState = state;
        gameUI.SetActive(currentState);
    }
}
