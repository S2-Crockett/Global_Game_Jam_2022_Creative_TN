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
    private GameState _state = GameState.Menu;

    private int _gameScore = 0;
    private bool firstLoad = true;
    private string _levelName;

    private void Start()
    {
        UpdateGameState(GameState.Menu); 
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
            case GameState.Win:
                break;
        }
    }

    public GameState GetGameState()
    {
        return _state;
    }

    // hacky way to do it for the main menu
    public void SetPlayingGameState()
    {
        UpdateGameState(GameState.Playing);
    }

    // Call this to change states.
    public void UpdateGameState(GameState newState)
    {
        _state = newState;
        switch (newState)
        {
            case GameState.Menu:
                StartCoroutine(HandleMenuState());
                break;
            case GameState.Playing:
                StartCoroutine(HandlePlayingState());
                break;
            case GameState.Lose:
                StartCoroutine(HandleLoseState());
                break;
            case GameState.Win:
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

    public void SetLevel(string level)
    {
        _levelName = level;
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
    private IEnumerator HandleMenuState()
    {
        StartCoroutine(UIManager.instance.DelayedStart(GameState.Menu));
        yield return new WaitForSeconds(0.2f);
    }

    // Event called once state has changed to this (not updated).
    public IEnumerator HandlePlayingState()
    {
        SceneManager.LoadScene(_levelName);
        StartCoroutine(UIManager.instance.DelayedStart(GameState.Playing));
        yield return new WaitForSeconds(0.2f);
        
        playerController = GameObject.Find("Player");
        PlayerHealth health = playerController.GetComponent<PlayerHealth>();
        playerController.GetComponent<PlayerMovement>()._gameState = GameState.Playing;
        
        UIManager.instance.healthUI.InitHealth(health.health);
        SoundManager.instance.pMovement = playerController.GetComponent<PlayerMovement>();
        WaypointManager.instance.GetWaypoints();
        
        _timer = GetComponent<GameTimer>();
        _timer.StartTimer();
    }

    // Event called once state has changed to this (not updated).
    public IEnumerator HandleLoseState()
    {
        playerController.GetComponent<PlayerMovement>()._gameState = GameState.Lose;
        playerController.GetComponent<PlayerMovement>().DisablePlayer();
        StartCoroutine(UIManager.instance.DelayedStart(GameState.Lose));
        UIManager.instance.loseUI.SetActive();
        _timer.StopTimer();
        yield return new WaitForSeconds(0.2f);
        // this will automatically be called when you die.
    }
    
    private IEnumerator HandleWinState()
    {
        playerController.GetComponent<PlayerMovement>()._gameState = GameState.Win;
        StartCoroutine(UIManager.instance.DelayedStart(GameState.Win));
        _timer.StopTimer();
        yield return new WaitForSeconds(0.2f);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RespawnPlayer()
    {
        playerController.GetComponent<PlayerMovement>().SetSpawn();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu_Scene");
        _timer.StopTimer();
        UpdateGameState(GameState.Menu);
    }
}

//States that are present throughout gameplay.
public enum GameState
{
    Menu,
    Playing,
    Lose,
    Win
}