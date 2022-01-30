using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITime : MonoBehaviour
{
    [Header("References")] 
    public Text minuteText;
    public Text secondText;

    private int _seconds = 0;
    private int _mintues = 0;

    public void UpdateMinute(int amount)
    {
        _mintues = amount;
        if (_mintues < 10)
        {
            string s = String.Format("{0}{1}", 0, _mintues);
            minuteText.text = s;
        }
        else
        {
            string s = String.Format("{0}", _mintues);
            minuteText.text = s;
        }
        
    }

    public void UpdateSecond(int amount)
    {
        _seconds = amount;
        if (_seconds < 10)
        {
            string s = String.Format("{0}{1}", 0, _seconds);
            secondText.text = s;
        }
        else
        {
            string s = String.Format("{0}", _seconds);
            secondText.text = s;
        }
        
    }
}
