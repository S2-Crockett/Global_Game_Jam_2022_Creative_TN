using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;

    public ParticleSystem dust;
    public ParticleSystem dust1;
    public ParticleSystem dust2;
    
    
    public bool isGrounded,isGrabGrounded, onLeftWall, onRightWall;

    [SerializeField] private int jumpForce;
    [SerializeField] private int moveSpeed;
    

    private float _moveInput;
    private SpriteRenderer _spriteRenderer;
    
    private bool _isGrabbingLeft, _isGrabbingRight;
    private float _gravityStore, wallJumpTime = 0.4f, _wallJumpCounter;
    private int _leftGrab, _rightGrab;
    private Animator _animator;
    private float _horizontalMove;

    private int _layermask;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _gravityStore = _rb.gravityScale;
        _animator = GetComponent<Animator>();

        _layermask = LayerMask.GetMask("Ground");
    }

    private void FixedUpdate()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        _animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));
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
        if (_wallJumpCounter <= 0)
        {
            _moveInput = Input.GetAxis("Horizontal");
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.75f);
            isGrabGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.5f);
            onLeftWall = Physics2D.Raycast(transform.position, Vector2.left, 0.75f, _layermask);
            onRightWall = Physics2D.Raycast(transform.position, Vector2.right, 0.75f, _layermask);
            
            Debug.DrawRay(transform.position, Vector2.down, Color.blue);
            
            if (isGrounded)
            {
                _rb.velocity = new Vector2(_moveInput * moveSpeed, _rb.velocity.y);
                _leftGrab = 0;
                _rightGrab = 0;
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

            
            if (onLeftWall && !isGrabGrounded && _leftGrab == 0)
            {
                if (_moveInput < 0)
                {
                    _isGrabbingLeft = true;
                    _leftGrab += 1;
                    _rightGrab = 0;
                }
            }
            else if (onRightWall && !isGrabGrounded && _rightGrab == 0)
            {
                if (_moveInput > 0)
                {
                    _isGrabbingRight = true;
                    _rightGrab += 1;
                    _leftGrab = 0;
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
                _rb.gravityScale = _gravityStore;
            }
            
            _animator.SetBool("Grounded", isGrounded);
        }
        else
        {
            _wallJumpCounter -= Time.deltaTime;
        }


    }

    void JumpOffWall(ParticleSystem dustPs, ref bool isGrabbing)
    {
        _rb.gravityScale = 0f;
        _rb.velocity = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateDust(dustPs);
            _wallJumpCounter = wallJumpTime;
            _rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed/2, jumpForce);
            _rb.gravityScale = _gravityStore;
            isGrabbing = false;
        }
    }

    void CreateDust(ParticleSystem dust_)
    {
        dust_.Play();
    }
}
