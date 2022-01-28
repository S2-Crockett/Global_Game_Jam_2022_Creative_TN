using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public GameObject Mainmenu;
    public GameObject Options;
    public GameObject Audio;
    public GameObject Display;
    public GameObject HowToPlay;

    // Start is called before the first frame update
    void Start()
    {
        Mainmenu.SetActive(true);
        Options.SetActive(false);
        Audio.SetActive(false);
        Display.SetActive(false);
        HowToPlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
