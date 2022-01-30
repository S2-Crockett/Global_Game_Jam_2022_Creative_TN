using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour
{
    [Header("References")] 
    public Text headerText;
    public Text bodyText;
    
    private bool _active;
    private float _activeTime;
    private CanvasGroup _canvasGroup;
    
    public void SetActive(string tutorialName, string tutorialText, float time)
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _active = true;
        _activeTime = time;
        _canvasGroup.alpha = 1;
        headerText.text = tutorialName;
        bodyText.text = tutorialText;
        
        gameObject.SetActive(true);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_active)
        {
            if (_activeTime > 0)
            {
                _activeTime -= Time.deltaTime;
            }
            else
            {
                _activeTime = 0;
                _active = false;
                gameObject.SetActive(false);
            }
        }
    }
}
