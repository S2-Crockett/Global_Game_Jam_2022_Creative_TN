using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : Singleton<WaypointManager>
{
    [SerializeField]private WaypointSystem[] _waypointsOne;
    [SerializeField]private WaypointSystem[] _waypointsTwo;
    
    
    public Vector3 respawnPosOne;
    public Vector3 respawnPosTwo;
    
    
    int index = 0;
    private void Start()
    {
        int indexTwo = 0;
        
        _waypointsOne = new WaypointSystem[GameObject.FindGameObjectsWithTag("Waypoint One").Length];
        _waypointsTwo = new WaypointSystem[GameObject.FindGameObjectsWithTag("Waypoint Two").Length];

        foreach (GameObject child in GameObject.FindGameObjectsWithTag("Waypoint One"))
        {
            if (child.CompareTag("Waypoint One"))
            {
                _waypointsOne[index] = child.GetComponent<WaypointSystem>();
                index += 1;
            }
        }
        foreach (GameObject child in GameObject.FindGameObjectsWithTag("Waypoint Two"))
        {
            _waypointsTwo[indexTwo] = child.GetComponent<WaypointSystem>();
            indexTwo += 1;
        }
    }

    private void Update()
    {
        for(int i = 0; i < index; i++)
        {
            if (_waypointsOne[i].collided && _waypointsTwo[i].collided)
            {
                SetPos(_waypointsOne[i].transform.position, _waypointsTwo[i].transform.position);
                StartCoroutine(_waypointsOne[i].SetCollided());
                StartCoroutine(_waypointsTwo[i].SetCollided());
            }
        }
        
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
            player.position = respawnPosOne;
        }
        else
        {
            player.position = respawnPosTwo;
        }
    }
}
