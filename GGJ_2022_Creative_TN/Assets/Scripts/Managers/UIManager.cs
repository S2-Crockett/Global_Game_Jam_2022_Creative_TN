using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
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
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void StartFunction()
    {
        SceneManager.LoadScene("Callum_Scene");
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
}
