using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    private enum MoveStages
    {
        Idle,
        AtPos,
        SetNewPos,
        Move
    }
    
    private Vector3[] _waypointsPos;
    private Vector3 _movePosition;
    private MoveStages _moveStages = MoveStages.Idle;
    private float _timer; 
    private void Start()
    {
        int index = 0;
        
        _waypointsPos = new Vector3[2];
        
        foreach (GameObject child in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            if (child.CompareTag("Waypoint"))
            {
                _waypointsPos[index] = child.transform.position;
                index += 1;
            }
        }
        transform.position = _waypointsPos[0];
    }
    
    private void Update()
    {
        switch (_moveStages)
        {
            case MoveStages.Idle:
            {
                transform.position = _waypointsPos[0];
                _moveStages = MoveStages.SetNewPos;
                break;
            }
            case MoveStages.SetNewPos:
            {
                if (transform.position == _waypointsPos[0])
                {
                    _movePosition = _waypointsPos[1];
                    _moveStages = MoveStages.Move;
                }
                else
                {
                    _movePosition = _waypointsPos[0];
                    _moveStages = MoveStages.Move;
                }
                break;
            }
            case MoveStages.Move:
            {
                if (Vector3.Distance(transform.position, _movePosition) > 0.01)
                {
                    transform.position = Vector3.MoveTowards(transform.position, _movePosition, 0.01f);
                }
                else
                {
                    transform.position = _movePosition;
                    _timer = 3;
                    _moveStages = MoveStages.AtPos;
                }
                break;
            }
            case MoveStages.AtPos:
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    _moveStages = MoveStages.SetNewPos;
                }
                break;
            }
        }
    }
}
