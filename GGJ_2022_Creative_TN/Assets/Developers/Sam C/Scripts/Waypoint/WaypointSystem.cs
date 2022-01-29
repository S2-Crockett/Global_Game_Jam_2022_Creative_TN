using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSystem : MonoBehaviour
{

    public bool collided = false;
    public Vector3 pos;

    private void Start()
    {
        pos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(SetCollided());
        }
    }

    IEnumerator SetCollided()
    {
        collided = true;
        yield return new WaitForSeconds(0.5f);
        collided = false;
    }
}