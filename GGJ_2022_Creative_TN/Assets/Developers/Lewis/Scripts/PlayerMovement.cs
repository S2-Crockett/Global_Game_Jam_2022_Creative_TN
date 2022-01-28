using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool isGrounded;
    public bool onLeftWall;
    public bool onRightWall;
    public float checkArea;

    public int leftCount;
    public int rightCount;

    public Transform groundCheck;
    public Transform leftCheck;
    public Transform rightCheck;
    public LayerMask whatIsGround;

    public int jumpForce;
    public int moveSpeed;

    private int extraJumps;
    public int wallJumps;
    private float moveInput;

    private bool facingRight = true;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if(facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkArea, whatIsGround);
        onLeftWall = Physics2D.OverlapCircle(leftCheck.position, checkArea, whatIsGround);
        onRightWall = Physics2D.OverlapCircle(rightCheck.position, checkArea, whatIsGround);

        
        rb.velocity = new Vector2(moveInput* moveSpeed, rb.velocity.y);

        if(isGrounded == true)
        {
            extraJumps = 0;
            wallJumps = 0;
            leftCount = 0;
            rightCount = 0;
        }

        

        if( Input.GetKeyDown(KeyCode.Space) && leftCount > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            leftCount = -20000;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && rightCount > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            rightCount = -20000;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && leftCount == 0 && rightCount == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }


        if (wallJumps < 1)
        {
            if (!isGrounded && onLeftWall)
            {
                extraJumps = 1;
                leftCount ++;
                rightCount = 0;
            }
            else if (!isGrounded && onRightWall)
            {
                extraJumps = 1;
                rightCount++; ;
                leftCount = 0;
            }
            else
            {
                extraJumps = 0;
            }
        }

        
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
