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
    public GameObject Mainmenu;
    public GameObject Options;
    public GameObject Audio;
    public GameObject Display;
    public GameObject HowToPlay;
    public Dropdown resolutionDropdown;

    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private TMP_Text brightnessTextValue;
    [SerializeField] private float defaultBrightness = 1;

    private bool _isFullScreen;
    private float _brightnessLevel;

    private Resolution[] resolutions;

    private FullScreenMode screenMode;

    private int countRes;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayedStart());

        Mainmenu.SetActive(true);
        Options.SetActive(false);
        Audio.SetActive(false);
        Display.SetActive(false);
        HowToPlay.SetActive(false);

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

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(0.5f);
        if (GameManager.instance.GetGameState() == GameState.Menu)
        {
            
        }

    }

    public void StartFunction()
    {
        SceneManager.LoadScene("Callum_Scene");
        Debug.Log("starting game");
    }
    public void OptionsFunction()
    {
        Mainmenu.SetActive(false);
        Options.SetActive(true);
    }
    public void BackToMenuFunction()
    {
        Mainmenu.SetActive(true);
        Options.SetActive(false);
        HowToPlay.SetActive(false);
    }
    public void AudioFunction()
    {
        Mainmenu.SetActive(false);
        Options.SetActive(false);
        Audio.SetActive(true);
        Display.SetActive(false);
    }
    public void DisplayFunction()
    {
        Mainmenu.SetActive(false);
        Options.SetActive(false);
        Audio.SetActive(false);
        Display.SetActive(true);
    }
    public void BackToOptionsFunction()
    {
        Mainmenu.SetActive(false);
        Options.SetActive(true);
        Audio.SetActive(false);
        Display.SetActive(false);
        HowToPlay.SetActive(false);
    }
    public void HowToPlayFunction()
    {
        Mainmenu.SetActive(false);
        Options.SetActive(false);
        Audio.SetActive(false);
        Display.SetActive(false);
        HowToPlay.SetActive(true);
    }
    public void QuitFunction()
    {
        Debug.Log("QUIT");
        Application.Quit();
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

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
       // brightnessTextValue.text = brightness.ToString("0.0");
    }

    public void SetFullScreen(bool isFullscreen)
    {
        _isFullScreen = isFullscreen;
    }
    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);

        PlayerPrefs.SetInt("masterFullscreen", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;
    }
}
