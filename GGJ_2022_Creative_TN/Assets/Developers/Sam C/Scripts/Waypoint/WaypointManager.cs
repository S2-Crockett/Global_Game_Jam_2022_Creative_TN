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
            if (_waypointsOne[i].collided && _waypointsTwo[i].collided && !_waypointsOne[i].set)
            {
                //When both Have collided Set respawn position for both players 
                StartCoroutine(_waypointsOne[i].SetCollided());  
                SetPos(_waypointsOne[i].transform.position, _waypointsTwo[i].transform.position);
                StartCoroutine(MoveFlag(_waypointsOne[i].transform, i));
                StartCoroutine(MoveFlagTwo(_waypointsTwo[i].transform));
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
        Vector3 newPos = new Vector3(respawnPosOne.x, respawnPosOne.y + offset.y, respawnPosOne.z);
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
            player.position = respawnPosOne;
        }
        else
        {
            player.position = respawnPosTwo;
        }
    }
}
