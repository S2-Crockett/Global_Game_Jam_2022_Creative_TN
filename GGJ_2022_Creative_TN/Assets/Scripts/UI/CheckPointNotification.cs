using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointNotification : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player Two"))
        {
            UIManager.instance.waypointUI.SetActive(5.0f);
            Destroy(gameObject);
        }
    }
}
