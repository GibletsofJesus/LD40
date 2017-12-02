using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public enum GameSate
    {
        Paused,
        Gameplay,
        GameOver
    }

    public GameSate m_currentState;

    [Header("References")]
    [SerializeField]
    GameObject m_pauseMenu, m_gameplayUI, m_trackMakerUI;
    [SerializeField]
    UiScroller PauseUI;

    // Use this for initialization
    void Start()
    {
        instance = this;
    }

    bool coutningDown = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && m_currentState == GameSate.Gameplay)
        {
            ChangeStategames(GameSate.Paused);
        }
    }

    public void ChangeStategames(int _stateIndex)
    {
        ChangeStategames((GameSate)_stateIndex);
    }

    public void ChangeStategames(GameSate newState)
    {
        //Do things for exiting states
        switch (m_currentState)
        {
            case GameSate.GameOver:
                break;
            case GameSate.Paused:
                m_pauseMenu.SetActive(false);
                PauseUI.enabled = false;   

                break;
            case GameSate.Gameplay:
                break;
        }

        m_currentState = newState;

        //Do things for entering states
        switch (m_currentState)
        {
            case GameSate.GameOver:
                m_gameplayUI.SetActive(false);
                break;
            case GameSate.Paused:
                m_pauseMenu.SetActive(true);
                PauseUI.enabled = true;   
                break;
            case GameSate.Gameplay:
                m_gameplayUI.SetActive(true);
                break;
        }
    }
}