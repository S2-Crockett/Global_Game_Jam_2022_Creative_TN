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

    [ItemCanBeNull] public List<Transform> waypoints = new List<Transform>();
    private Transform movePosition;
    public MoveStages _moveStages = MoveStages.IDLE;
    private float timer; 
    void Start()
    {
        foreach (GameObject child in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            if (child.CompareTag("Waypoint"))
            {
                waypoints.Add(child.transform);
            }
        }
        transform.position = waypoints[0].position;
    }
    
    void Update()
    {
        switch (_moveStages)
        {
            case MoveStages.IDLE:
            {
                if (waypoints[0] != null)
                {
                    transform.position = waypoints[0].position;
                }

                _moveStages = MoveStages.SET_NEW_POS;
                break;
            }
            case MoveStages.SET_NEW_POS:
            {
                if (transform.position == waypoints[0].position)
                {
                    movePosition = waypoints[1].transform;
                    _moveStages = MoveStages.MOVE;
                }
                else
                {
                    movePosition = waypoints[0].transform;
                    _moveStages = MoveStages.MOVE;
                }
                break;
            }
            case MoveStages.MOVE:
            {
                if (Vector3.Distance(transform.position, movePosition.position) > 0.01)
                {
                    transform.position = Vector3.MoveTowards(transform.position, movePosition.position, 0.01f);
                }
                else
                {
                    transform.position = movePosition.position;
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
