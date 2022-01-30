using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWaypoint : MonoBehaviour
{
    private bool _active;
    private float _activeTime;
    private CanvasGroup _canvasGroup;

    public void SetActive(float time)
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 1;
        _active = true;
        _activeTime = time;
        gameObject.SetActive(true);
    }
    
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
