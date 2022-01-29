using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;

    public ParticleSystem dust;
    public ParticleSystem dust1;
    public ParticleSystem dust2;

    
    public bool isGrounded,isGrabGrounded, onLeftWall, onRightWall;
    
    [SerializeField] private float checkArea = 0.02f;

    [HideInInspector]
    public int leftCount, rightCount;

    [HideInInspector]
    public Transform leftCheck, rightCheck;
    
    private LayerMask _whatIsGround;

    [SerializeField] private int jumpForce;
    [SerializeField] private int moveSpeed;
    
    public bool isManagerGrounded = true, canMoveRight = true, canMoveLeft = true;

    private float _moveInput;
    private SpriteRenderer _spriteRenderer;


    public Transform wallGrabPoint;
    public bool isGrabbingLeft, isGrabbingRight;
    private float gravityStore;
    public float wallJumpTime;
    private float wallJumpCounter;
    private int leftGrab, rightGrab;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _whatIsGround = LayerMask.GetMask("Ground");
        _spriteRenderer = GetComponent<SpriteRenderer>();

        gravityStore = _rb.gravityScale;
        
        foreach (var child in GetComponentsInChildren<Transform>())
        {
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
        if (_moveInput > 0)
        {
            _spriteRenderer.flipX = false;
            //true
        }
        else if(_moveInput < 0)
        {
            _spriteRenderer.flipX = true;
            //false
        }
    }

    private void Update()
    {
        if (wallJumpCounter <= 0)
        {
            _moveInput = Input.GetAxis("Horizontal");
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.5f);
            isGrabGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.5f);
            onLeftWall = Physics2D.Raycast(transform.position, Vector2.left, 0.5f);
            onRightWall = Physics2D.Raycast(transform.position, Vector2.right, 0.5f);
            
            if (isGrounded)
            {
                _rb.velocity = new Vector2(_moveInput * moveSpeed, _rb.velocity.y);
                leftGrab = 0;
                rightGrab = 0;
            }
            else
            {
                _rb.velocity = new Vector2(_moveInput * moveSpeed / 2, _rb.velocity.y);
            }
            

            

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                CreateDust(dust);
                _rb.velocity = Vector2.up * jumpForce;
            }

            
            if (onLeftWall && !isGrabGrounded && leftGrab == 0)
            {
                if (_moveInput < 0)
                {
                    isGrabbingLeft = true;
                    leftGrab += 1;
                    rightGrab = 0;
                }
            }
            else if (onRightWall && !isGrabGrounded && rightGrab == 0)
            {
                if (_moveInput > 0)
                {
                    isGrabbingRight = true;
                    rightGrab += 1;
                    leftGrab = 0;
                }
            }
            else
            {
                if (!isGrabbingLeft)
                {
                    isGrabbingLeft = false;
                }
            }

            if (isGrabbingLeft)
            {
                _rb.gravityScale = 0f;
                _rb.velocity = Vector2.zero;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    wallJumpCounter = wallJumpTime;
                    _rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed/2, jumpForce);
                    _rb.gravityScale = gravityStore;
                    isGrabbingLeft = false;
                }
            }
            else if (isGrabbingRight)
            {
                _rb.gravityScale = 0f;
                _rb.velocity = Vector2.zero;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    wallJumpCounter = wallJumpTime;
                    _rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed/2, jumpForce);
                    _rb.gravityScale = gravityStore;
                    isGrabbingRight = false;
                }
            }
            else
            {
                _rb.gravityScale = gravityStore;
            }
        }
        else
        {
            wallJumpCounter -= Time.deltaTime;
        }


    }

    void CreateDust(ParticleSystem dust_)
    {
        dust_.Play();
    }
}
