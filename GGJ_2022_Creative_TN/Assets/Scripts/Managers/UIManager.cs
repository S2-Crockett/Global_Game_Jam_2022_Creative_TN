using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [Header("Game HUD")] public UITime timeUI;
    public UIScore scoreUI;
    public UIHealth healthUI;
    public UITutorial tutorialUI;
    public UIWaypoint waypointUI;
    public UILose loseUI;
    public UIWin winUI;

    [Header("Menu HUD")] 
    public UIMenu menuUI;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public IEnumerator DelayedStart(GameState state)
    {
        yield return new WaitForSeconds(0.2f);
        switch (state)
        {
            case GameState.Menu:
                HandleMenuUI();
                break;
            case GameState.Playing:
                HandlePlayingUI();
                break;
            case GameState.Lose:
                HandleLoseUI();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void HandleMenuUI()
    {
        menuUI = GameObject.Find("MenuUI").GetComponent<UIMenu>();
        menuUI.SetMainMenu();
    }

    private void HandleLoseUI()
    {
    }

    private void HandlePlayingUI()
    {
        timeUI = GameObject.Find("Panel-Time").GetComponent<UITime>();
        healthUI = GameObject.Find("Panel-Health").GetComponent<UIHealth>();
        scoreUI = GameObject.Find("Panel-Score").GetComponent<UIScore>();
        tutorialUI = GameObject.Find("Panel-Tutorial").GetComponent<UITutorial>();
        waypointUI = GameObject.Find("Panel-Checkpoint").GetComponent<UIWaypoint>();
    }
}