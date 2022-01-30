using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private int _seconds = 0;
    private int _minutes = 0;
    private bool _timerStart = false;
    public void StartTimer()
    {
        _timerStart = true;
        StartCoroutine(Timer());
    }
    
    public void ResetTimer(bool oneWay)
    {
        if (oneWay)
        {
            _minutes = 0;
            _seconds = 0;
            StopCoroutine(Timer());
            _timerStart = false; 
        }
        else
        {
            _minutes = 0;
            _seconds = 0;
            StopCoroutine(Timer());
        }
    }
    
    private IEnumerator Timer()
    {
        while (_timerStart)
        {
            yield return new WaitForSeconds(1.0f);
            _seconds++;
            if (UIManager.instance.timeUI)
            {
                UIManager.instance.timeUI.UpdateSecond(_seconds);
            }
            if (_seconds >= 60)
            {
                _seconds = 0;
                _minutes++;
                
                UIManager.instance.timeUI.UpdateSecond(_seconds);
                UIManager.instance.timeUI.UpdateMinute(_minutes);
            }
        }
        

        // add one second to timer
        // if timer == 60, add minute count..
    }
}
