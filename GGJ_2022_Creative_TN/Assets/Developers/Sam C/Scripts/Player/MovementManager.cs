using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovements;
    [SerializeField] private ShadowPlayer shadowPlayer;
    private void Start()
    {
        playerMovements = GameObject.FindObjectOfType<PlayerMovement>();
        shadowPlayer = GameObject.FindObjectOfType<ShadowPlayer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (ArePlayersFalling())
        {
            playerMovements.isManagerGrounded = false;
        }
        else
        {
            playerMovements.isManagerGrounded = true;
        }

        if (CanPlayerMoveLeft())
        {
            playerMovements.canMoveRight = true;
        }
        else
        {
            playerMovements.canMoveRight = false;
        }
        
        if (CanPlayerMoveRight())
        {
            playerMovements.canMoveLeft = true;
        }
        else
        {
            playerMovements.canMoveLeft = false;
        }

    }

    private bool ArePlayersFalling()
    {
        if (!playerMovements.isGrounded || !shadowPlayer.isGrounded)
        {
            return true;
        }

        return false;
    }

    private bool CanPlayerMoveLeft()
    {
        if (playerMovements.onRightWall || shadowPlayer.onRightWall)
        {
            return false;
        }

        return true;
    }
    private bool CanPlayerMoveRight()
    {
        if (playerMovements.onLeftWall || shadowPlayer.onLeftWall)
        {
            return false;
        }

        return true;
    }
}
