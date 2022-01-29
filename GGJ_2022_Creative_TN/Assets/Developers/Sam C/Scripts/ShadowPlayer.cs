using System;
using UnityEngine;

public class ShadowPlayer : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private Vector3 offset;
    
    [HideInInspector]
    public bool isGrounded, onLeftWall, onRightWall;
    
    [SerializeField] private float checkArea;

    [HideInInspector]
    public Transform groundCheck, leftCheck, rightCheck;
    
    
    private LayerMask _whatIsGround;
    
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _whatIsGround = LayerMask.GetMask("Ground");
    }

    private void Awake()
    {
        foreach (var child in GetComponentsInChildren<Transform>())
        {
            if (child.name == "Ground Check")
            {
                groundCheck = child;
            }
            if (child.name == "Left Check")
            {
                leftCheck = child;
            }
            if (child.name == "Right Check")
            {
                rightCheck = child;
            }
        }
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkArea, _whatIsGround);
        onLeftWall = Physics2D.OverlapCircle(leftCheck.position, checkArea, _whatIsGround);
        onRightWall = Physics2D.OverlapCircle(rightCheck.position, checkArea, _whatIsGround);


        Vector3 posOffset = new Vector3(0.6f, 0, 0);
        if (Physics2D.Raycast(transform.position - posOffset, Vector2.down, Mathf.Infinity) ||
            Physics2D.Raycast(transform.position + posOffset, Vector2.down, Mathf.Infinity) )
        {
            transform.position = _player.transform.position + offset;
        }
    }
}
