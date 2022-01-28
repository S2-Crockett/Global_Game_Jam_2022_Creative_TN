using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    [Header("References")] 
    public Text scoreText;

    private int _score = 0;

    public void InitScore(int amount)
    {
        _score = amount;
        scoreText.text = _score.ToString();
    }
    public void UpdateScore(int amount)
    {
        _score += amount;
        scoreText.text = _score.ToString("#");
    }
}
