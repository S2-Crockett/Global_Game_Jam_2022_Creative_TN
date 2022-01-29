using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : Singleton<WaypointManager>
{
    [SerializeField]private WaypointSystem[] _waypoints;
    
    
    public Vector3 respawnPos;
    
    private void Start()
    {
        int index = 0;
        _waypoints = new WaypointSystem[GameObject.FindGameObjectsWithTag("Waypoint").Length];
        
        foreach (GameObject child in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            if (child.CompareTag("Waypoint"))
            {
                _waypoints[index] = child.GetComponent<WaypointSystem>();
                index += 1;
            }
        }
    }

    private void Update()
    {
        foreach (var waypoints in _waypoints)
        {
            if (waypoints.collided)
            {
                SetPos(waypoints.transform.position);
            }
        }
        
    }

    void SetPos(Vector3 pos)
    {
        respawnPos = pos;
    }

    public void Respawn(Transform player)
    {
        player.position = respawnPos;
    }
}
