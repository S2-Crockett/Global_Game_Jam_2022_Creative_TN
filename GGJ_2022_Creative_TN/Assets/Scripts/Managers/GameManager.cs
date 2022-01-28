using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("References")] 
    public GameObject playerController;
    private GameTimer _timer;
    
    private GameState _state;

    private int _gameScore = 0;

    private void Start()
    {
        // default state assigned.
        UpdateGameState(GameState.Menu);
        UIManager.instance.scoreUI.InitScore(0);
        
    }
    private void Update()
    {
        switch (_state)
        {
            case GameState.Menu:
                break;
            case GameState.Playing:
                break;
            case GameState.Lose:
                break;
        }
    }
    
    // Call this to change states.
    public void UpdateGameState(GameState newState)
    {
        _state = newState;
        switch (newState)
        {
            case GameState.Menu:
                HandleMenuState();
                break;
            case GameState.Playing:
                HandlePlayingState();
                break;
            case GameState.Lose:
                HandleLoseState();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    public void UpdateScore(int amount)
    {
        _gameScore += amount;
        UIManager.instance.scoreUI.UpdateScore(amount);
    }
    
    // Event called once state has changed to this (not updated).
    private void HandleMenuState()
    {
        _timer = GetComponent<GameTimer>();
        _timer.StartTimer(); 
        
        UIManager.instance.healthUI.InitHealth(5);
    }
    
    // Event called once state has changed to this (not updated).
    private void HandlePlayingState()
    {
        // start the timer in game
        
    }
    
    // Event called once state has changed to this (not updated).
    private void HandleLoseState()
    {
    }
}

//States that are present throughout gameplay.
public enum GameState
{
    Menu,
    Playing,
    Lose
}
