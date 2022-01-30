using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [Header("Game HUD")] 
    public UITime timeUI;
    public UIScore scoreUI;
    public UIHealth healthUI;
    public UITutorial tutorialUI;
    public UIWaypoint waypointUI;

    [Header("Menu HUD")] public GameObject Mainmenu;
    public GameObject Options;
    public GameObject Audio;
    public GameObject Display;
    public GameObject LevelSelect;
    public Dropdown resolutionDropdown;
    
    private bool _isFullScreen;
    private Resolution[] resolutions;
    private FullScreenMode screenMode;
    private int countRes;
    
    public GameState _state;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public IEnumerator DelayedStart(GameState state)
    {
        yield return new WaitForSeconds(0.1f);
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
        Mainmenu.SetActive(true);
        Options.SetActive(false);
        Audio.SetActive(false);
        Display.SetActive(false);
        LevelSelect.SetActive((false));

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
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

    public void DisplayOptions()
    {
        Mainmenu.SetActive(false);
        Options.SetActive(true);
    }

    public void DisplayLevelSelect()
    {
        Mainmenu.SetActive(false);
        LevelSelect.SetActive(true);
    }
    public void ReturnToMenu()
    {
        Mainmenu.SetActive(true);
        LevelSelect.SetActive(false);
        Options.SetActive(false);
    }

    public void DisplayAudio()
    {
        Mainmenu.SetActive(false);
        Options.SetActive(false);
        Audio.SetActive(true);
        Display.SetActive(false);
    }

    public void DisplayGraphics()
    {
        Mainmenu.SetActive(false);
        Options.SetActive(false);
        Audio.SetActive(false);
        Display.SetActive(true);
    }

    public void ReturnToOptions()
    {
        Mainmenu.SetActive(false);
        Options.SetActive(true);
        Audio.SetActive(false);
        Display.SetActive(false);
    }

    public void SetGraphics(int GraphicsIndex)
    {
        QualitySettings.SetQualityLevel(GraphicsIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        _isFullScreen = isFullscreen;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetInt("masterFullscreen", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;
    }
}