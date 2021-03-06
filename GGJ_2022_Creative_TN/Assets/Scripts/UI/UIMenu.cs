using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
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
    private string _levelName;
    
    public void SetMainMenu()
    {
        Mainmenu.SetActive(true);
        Options.SetActive(false);
        Audio.SetActive(false);
        Display.SetActive(false);
        LevelSelect.SetActive(false);

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
    
    public void SetLevel(string level)
    {
        _levelName = level;
        GameManager.instance.UpdateGameState(GameState.Playing);
        SceneManager.LoadScene(_levelName);
        
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
