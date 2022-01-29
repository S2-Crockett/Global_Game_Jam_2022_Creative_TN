using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;

    public ParticleSystem dust;
    public ParticleSystem dust1;
    public ParticleSystem dust2;
    
    [HideInInspector]
    public bool isGrounded,isGrabGrounded, onLeftWall, onRightWall;

    [SerializeField] private int jumpForce;
    [SerializeField] private int moveSpeed;
    

    private float _moveInput;
    private SpriteRenderer _spriteRenderer;
    
    private bool _isGrabbingLeft, _isGrabbingRight;
    private float gravityStore, wallJumpTime = 0.4f, wallJumpCounter;
    private int leftGrab, rightGrab;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        gravityStore = _rb.gravityScale;
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
                    _isGrabbingLeft = true;
                    leftGrab += 1;
                    rightGrab = 0;
                }
            }
            else if (onRightWall && !isGrabGrounded && rightGrab == 0)
            {
                if (_moveInput > 0)
                {
                    _isGrabbingRight = true;
                    rightGrab += 1;
                    leftGrab = 0;
                }
            }
            else
            {
                if (!_isGrabbingLeft)
                {
                    _isGrabbingLeft = false;
                }
            }

            if (_isGrabbingLeft)
            {
                JumpOffWall(dust2, ref _isGrabbingLeft);
            }
            else if (_isGrabbingRight)
            {
                JumpOffWall(dust1, ref _isGrabbingRight);
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

    void JumpOffWall(ParticleSystem dustPs, ref bool isGrabbing)
    {
        _rb.gravityScale = 0f;
        _rb.velocity = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateDust(dustPs);
            wallJumpCounter = wallJumpTime;
            _rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed/2, jumpForce);
            _rb.gravityScale = gravityStore;
            isGrabbing = false;
        }
    }

    void CreateDust(ParticleSystem dust_)
    {
        dust_.Play();
    }
}
