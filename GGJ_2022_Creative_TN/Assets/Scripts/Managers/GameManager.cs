using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        UpdateGameState(GameState.Playing);
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

    public GameState GetGameState()
    {
        return _state;
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

    public void DecreaseHealth(int amount)
    {
        if (playerController)
        {
            PlayerHealth health = playerController.GetComponent<PlayerHealth>();
            health.DecreaseHealth(amount);
        }
    }

    public void IncreaseHealth(int amount)
    {
        if (playerController)
        {
            PlayerHealth health = playerController.GetComponent<PlayerHealth>();
            health.IncreaseHealth(amount);
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
       
    }
    
    // Event called once state has changed to this (not updated).
    private void HandlePlayingState()
    {
        _timer = GetComponent<GameTimer>();
        _timer.StartTimer();
    }
    
    // Event called once state has changed to this (not updated).
    private void HandleLoseState()
    {
        // this will automatically be called when you die.
        SceneManager.LoadScene("Loss_Scene");
    }
}

//States that are present throughout gameplay.
public enum GameState
{
    Menu,
    Playing,
    Lose
}
