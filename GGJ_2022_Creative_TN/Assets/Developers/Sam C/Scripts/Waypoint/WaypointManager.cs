using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class WaypointManager : Singleton<WaypointManager>
{
    private List<WaypointSystem> _waypointsOne = new List<WaypointSystem>();
    private List<WaypointSystem> _waypointsTwo = new List<WaypointSystem>();


    public Vector3 respawnPosOne;
    public Vector3 respawnPosTwo;
    
    public void GetWaypoints()
    {
        foreach (GameObject child in GameObject.FindGameObjectsWithTag("Waypoint One"))
        {
            _waypointsOne.Add(child.GetComponent<WaypointSystem>());
        }

        foreach (GameObject child in GameObject.FindGameObjectsWithTag("Waypoint Two"))
        {
            _waypointsTwo.Add(child.GetComponent<WaypointSystem>());
        }
    }

    public void ClearWaypoints()
    {
        foreach (var wp in _waypointsOne)
        {
            Destroy(wp);
        }
        
        foreach (var wp in _waypointsTwo)
        {
            Destroy(wp);
        }
    }

    private void Update()
    {
        for (int i = 0; i < _waypointsOne.Count; i++)
        {
            if (_waypointsOne[i] && _waypointsTwo[i])
            {
                if (_waypointsOne[i].collided && _waypointsTwo[i].collided && !_waypointsOne[i].set)
                {
                    //When both Have collided Set respawn position for both players 
                    StartCoroutine(_waypointsOne[i].SetCollided());
                    SetPos(_waypointsOne[i].transform.position, _waypointsTwo[i].transform.position);
                    StartCoroutine(MoveFlag(_waypointsOne[i].transform, i));
                    StartCoroutine(MoveFlagTwo(_waypointsTwo[i].transform));
                    _waypointsOne[i].GetComponent<BoxCollider2D>().enabled = false;
                    _waypointsTwo[i].GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
    }

    IEnumerator MoveFlag(Transform flag, int index)
    {
        Vector3 offset = new Vector3(0, 0.25f, 0);
        Vector3 newPos = respawnPosOne + offset;
        flag.transform.position = Vector3.MoveTowards(respawnPosOne, newPos, 0.005f);
        yield return new WaitForSeconds(0.75f);
        _waypointsOne[index].set = true;
    }

    IEnumerator MoveFlagTwo(Transform flag)
    {
        Vector3 offset = GameObject.FindGameObjectWithTag("Player Two").GetComponent<ShadowPlayer>().offset;
        Vector3 newPos = new Vector3(respawnPosOne.x, respawnPosOne.y + offset.y, respawnPosOne.z - 20);
        flag.transform.position = newPos;
        yield return new WaitForSeconds(0.75f);
    }

    void SetPos(Vector3 pos, Vector3 posTwo)
    {
        respawnPosOne = pos;
        respawnPosTwo = posTwo;
    }

    public void Respawn(Transform player)
    {
        if (player.CompareTag("Player"))
        {
            Vector3 offset = new Vector3(0, 0, 10);
            player.position = respawnPosOne - offset;
        }
        else
        {
            player.position = respawnPosTwo;
        }
    }
}