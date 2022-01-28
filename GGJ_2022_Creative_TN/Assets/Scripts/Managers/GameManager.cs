using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState state;

    [Header("References")] 
    public GameObject playerController;

    private void Start()
    {
        // default state assigned.
        UpdateGameState(GameState.Menu);
    }

    private void Update()
    {
        switch (state)
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
        state = newState;
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
    
    // Event called once state has changed to this (not updated).
    private void HandleMenuState()
    {
    }
    
    // Event called once state has changed to this (not updated).
    private void HandlePlayingState()
    {
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
