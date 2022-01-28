using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    
    public enum MoveStages
    {
        IDLE,
        AT_POS,
        SET_NEW_POS,
        MOVE
    }
    
    public Vector3[] waypointsPos;
    private Vector3 movePosition;
    public MoveStages _moveStages = MoveStages.IDLE;
    private float timer; 
    void Start()
    {
        int index = 0;
        
        waypointsPos = new Vector3[2];
        
        foreach (GameObject child in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            if (child.CompareTag("Waypoint"))
            {
                waypointsPos[index] = child.transform.position;
                index += 1;
            }
        }
        transform.position = waypointsPos[0];
    }
    
    void Update()
    {
        switch (_moveStages)
        {
            case MoveStages.IDLE:
            {
                transform.position = waypointsPos[0];
                _moveStages = MoveStages.SET_NEW_POS;
                break;
            }
            case MoveStages.SET_NEW_POS:
            {
                if (transform.position == waypointsPos[0])
                {
                    movePosition = waypointsPos[1];
                    _moveStages = MoveStages.MOVE;
                }
                else
                {
                    movePosition = waypointsPos[0];
                    _moveStages = MoveStages.MOVE;
                }
                break;
            }
            case MoveStages.MOVE:
            {
                if (Vector3.Distance(transform.position, movePosition) > 0.01)
                {
                    transform.position = Vector3.MoveTowards(transform.position, movePosition, 0.01f);
                }
                else
                {
                    transform.position = movePosition;
                    timer = 3;
                    _moveStages = MoveStages.AT_POS;
                }
                break;
            }
            case MoveStages.AT_POS:
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    _moveStages = MoveStages.SET_NEW_POS;
                }
                break;
            }
                
        }
    }
}
