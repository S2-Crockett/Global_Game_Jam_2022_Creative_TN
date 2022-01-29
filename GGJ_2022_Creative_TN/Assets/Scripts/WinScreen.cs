using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene("Game_Scene");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("menu");
    }
}
