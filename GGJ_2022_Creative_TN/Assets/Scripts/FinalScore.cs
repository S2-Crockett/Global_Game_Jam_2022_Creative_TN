using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    public Text score_text;
    private int score = 0;

    private void Start()
    {

        score = GetComponent<GameManager>()._gameScore;
    }

    // Update is called once per frame
    void Update()
    {
        score_text.text = "Score: " + score.ToString();
    }
}
