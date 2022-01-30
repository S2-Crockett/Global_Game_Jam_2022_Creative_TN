using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNotification : MonoBehaviour
{
    [Header("Tutorial Info")] 
    public string tutorialName;
    [TextArea(15,20)]
    public string tutorialText;
    public float timeToDisplay = 5.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player Two"))
        {
            UIManager.instance.tutorialUI.SetActive(tutorialName, tutorialText, 5.0f);
            Destroy(gameObject);
        }
    }
}
