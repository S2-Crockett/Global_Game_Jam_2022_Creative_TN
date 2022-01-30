using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSystem : MonoBehaviour
{

    public bool collided = false;
    public bool set = false;
    public Vector3 pos;

    private void Start()
    {
        pos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("Player Two"))
        {
            if (!collided)
            {
                UIManager.instance.waypointUI.SetActive(3.0f);
                collided = true;
            }
        }
    }

    public IEnumerator SetCollided()
    {
        yield return new WaitForSeconds(0.5f);
        //collided = false;
    }
}
