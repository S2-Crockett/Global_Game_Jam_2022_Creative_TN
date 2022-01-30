using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILose : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    public void SetActive()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 1;
    }

    public void ReturnToCheckPoint()
    {
        GameManager.instance.RespawnPlayer();
    }
}
