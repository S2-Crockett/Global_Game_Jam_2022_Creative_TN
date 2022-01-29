using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;

    [HideInInspector]
    public bool isGrounded, onLeftWall, onRightWall;
    
    [SerializeField] private float checkArea = 0.02f;

    [HideInInspector]
    public int leftCount, rightCount;

    [HideInInspector]
    public Transform groundCheck, leftCheck, rightCheck;
    
    private LayerMask _whatIsGround;

    [SerializeField] private int jumpForce;
    [SerializeField] private int moveSpeed;

    [HideInInspector]
    public bool isManagerGrounded = true, canMoveRight = true, canMoveLeft = true;

    private float _moveInput;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private float horizontalMove;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _whatIsGround = LayerMask.GetMask("Ground");
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

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

    private void FixedUpdate()
    {
        _moveInput = Input.GetAxis("Horizontal");
        
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        _animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        
        if (_moveInput > 0)
        {
            
            _spriteRenderer.flipX = false;
            //true
        }
        else if(_moveInput < 0)
        {
            _spriteRenderer.flipX = true;

            if (_animator)
            {
                
            }
            //false
        }
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkArea, _whatIsGround);
        onLeftWall = Physics2D.OverlapCircle(leftCheck.position, checkArea, _whatIsGround);
        onRightWall = Physics2D.OverlapCircle(rightCheck.position, checkArea, _whatIsGround);
        
        _animator.SetBool("Grounded", isGrounded);

        
        
        if (canMoveRight && _moveInput > 0)
        {
            _rb.velocity = new Vector2(_moveInput * moveSpeed, _rb.velocity.y);
        }
        else if (canMoveLeft && _moveInput < 0)
        {
            _rb.velocity = new Vector2(_moveInput * moveSpeed, _rb.velocity.y);
        }
        else if (_moveInput == 0 || !canMoveLeft || !canMoveRight)
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }
        
        if(isGrounded)
        {
            leftCount = 0;
            rightCount = 0;
        }
        
        if( Input.GetKeyDown(KeyCode.Space) && leftCount > 0)
        {
            _rb.velocity = Vector2.up * jumpForce;
            leftCount = -20000;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && rightCount > 0)
        {
            _rb.velocity = Vector2.up * jumpForce;
            rightCount = -20000;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && leftCount == 0 && rightCount == 0 && isGrounded && isManagerGrounded)
        {
            _rb.velocity = Vector2.up * jumpForce;
        }

        
        if (!isGrounded && onLeftWall)
        {
            leftCount++;
            rightCount = 0;
        }
        else if (!isGrounded && onRightWall)
        {
            rightCount++;
            leftCount = 0;
        }
    }
}
