using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool isGrounded;
    public Transform groundCheck;
    public float checkArea;
    public LayerMask whatIsGround;
    public int jumpForce;
    public int extraJumps;

    public int moveSpeed;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkArea, whatIsGround);

        rb.velocity = new Vector2(Input.GetAxis("Horizontal")* moveSpeed, rb.velocity.y);

        if(isGrounded == true)
        {
            extraJumps = 0;
        }

        if(Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }
}
