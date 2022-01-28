using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private List<PlayerMovement> _playerMovements = new List<PlayerMovement>();
    void Start()
    {
        foreach (var players in GameObject.FindObjectsOfType<PlayerMovement>())
        {
            _playerMovements.Add(players);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var players in _playerMovements)
        {
            if (ArePlayersFalling())
            {
                players.isManagerGrounded = false;
            }
            else
            {
                players.isManagerGrounded = true;
            }
        }
    }

    private bool ArePlayersFalling()
    {
        for (int i = 0; i < _playerMovements.Count; i++)
        {
            if (!_playerMovements[i].isGrounded)
            {
                return true;
            }
        }
        return false;
    }
}
