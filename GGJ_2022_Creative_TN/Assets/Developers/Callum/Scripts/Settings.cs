using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Dropdown displayType;

    private Resolution[] storeResolutions;

    private FullScreenMode screenMode;

    private int countRes;

    void Start()
    {
        Resolution[] resolutions = Screen.resolutions;
        Array.Reverse(resolutions);
        resolutions = new Resolution[resolutions.Length];

        ScreenInitialize();
        AddResolution(resolutions);
        ResolutionInitialize(storeResolutions);

        displayType.onValueChanged.AddListener(delegate { ScreenOptions(displayType.options[displayType.value].text); });
        resolutionDropdown.onValueChanged.AddListener(delegate
        {
            Screen.SetResolution(storeResolutions[resolutionDropdown.value].width,
            storeResolutions[resolutionDropdown.value].height, screenMode);
        });

    }

    void AddResolution(Resolution[] res)
    {
        countRes = 0;

        for (int i = 0; i < res.Length; i++)
        {
            if (res[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                storeResolutions[countRes] = res[i];
                countRes++;
            }
        }

        for (int i = 0; i < countRes; i++)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(ResolutionToString(storeResolutions[i])));
        }
    }

    public void ScreenOptions(string mode)
    {
        if (mode == "FullScreen")
        {
            screenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else if (mode == "Windowed")
        {
            screenMode = FullScreenMode.Windowed;
        }
        else
        {
            screenMode = FullScreenMode.FullScreenWindow;
        }

        Screen.fullScreenMode = screenMode;
    }

    void ResolutionInitialize(Resolution[] res)
    {
        for (int i = 0; i < res.Length; i++)
        {
            if (Screen.width == res[i].width && Screen.height == res[i].height)
            {
                resolutionDropdown.value = i;
            }
        }
        resolutionDropdown.RefreshShownValue();
    }

    public void ScreenInitialize()
    {
        if (Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen)
        {
            displayType.value = 0;
            screenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else if (Screen.fullScreenMode == FullScreenMode.Windowed)
        {
            displayType.value = 1;
            screenMode = FullScreenMode.Windowed;
        }
        else
        {
            displayType.value = 2;
            screenMode = FullScreenMode.FullScreenWindow;
        }
        displayType.RefreshShownValue();
    }

    string ResolutionToString(Resolution screenRes)
    {
        return screenRes.width + "x" + screenRes.height;
    }
}
