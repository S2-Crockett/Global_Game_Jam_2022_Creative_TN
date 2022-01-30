using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;

    public ParticleSystem dust;
    public ParticleSystem dust1;
    public ParticleSystem dust2;
    
    
    public bool isGrounded, isGrabGrounded, onLeftWall, onRightWall;

    [SerializeField] private int jumpForce;
    [SerializeField] private int moveSpeed;
    

    private float _moveInput;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _spriteShadowRenderer;
    
    [SerializeField] private bool _isGrabbingLeft, _isGrabbingRight, _isGrabbing;
    private float _gravityStore, wallJumpTime = 0.4f, _wallJumpCounter;
    [SerializeField]private int _leftGrab, _rightGrab;
    private Animator[] _animator;
    private float _horizontalMove;

    private int _layermask;


    private bool test = false;

    public bool isMoving = false;
    public AudioClip jump;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteShadowRenderer = GameObject.FindGameObjectWithTag("Player Two").GetComponent<SpriteRenderer>();
        _gravityStore = _rb.gravityScale;
        _animator = new Animator[2];
        _animator[0] = GetComponent<Animator>();
        _animator[1] = GameObject.FindGameObjectWithTag("Player Two").GetComponent<Animator>();

        _layermask = LayerMask.GetMask("Ground");
        SoundManager._Instance.pMovement = this;
    }

    private void FixedUpdate()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        foreach (var anim in _animator)
        {
            anim.SetFloat("Speed", Mathf.Abs(_horizontalMove));
        }
        
        if (_moveInput > 0)
        {
            _spriteRenderer.flipX = false;
            _spriteShadowRenderer.flipX = false;
            //true
        }
        else if(_moveInput < 0)
        {
            _spriteRenderer.flipX = true;
            _spriteShadowRenderer.flipX = true;
            //false
        }
    }
    
    

    private void Update()
    {
        if(_moveInput > 0 || _moveInput < 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (_wallJumpCounter <= 0)
        {
            _moveInput = Input.GetAxis("Horizontal");
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.75f);
            isGrabGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.5f);
            onLeftWall = Physics2D.Raycast(transform.position, Vector2.left, 0.75f, _layermask);
            onRightWall = Physics2D.Raycast(transform.position, Vector2.right, 0.75f, _layermask);
            
            
            if (isGrounded)
            {
                _rb.velocity = new Vector2(_moveInput * moveSpeed, _rb.velocity.y);
                _leftGrab = 0;
                _rightGrab = 0;
            }
            else
            {
                _rb.velocity = new Vector2(_moveInput * moveSpeed / 1.5f, _rb.velocity.y);
            }
            

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                CreateDust(dust);
                _rb.velocity = Vector2.up * jumpForce;
                SoundManager._Instance.JumpSounds(jump);
            }
            

            
            if (onLeftWall && !isGrabGrounded && _leftGrab == 0)
            {
                if (_moveInput < 0 && Input.GetKeyDown(KeyCode.W))
                {
                    _isGrabbingLeft = true;
                    _leftGrab += 1;
                    _rightGrab = 0;
                }
            }
            else if (onRightWall && !isGrabGrounded && _rightGrab == 0)
            {
                if (_moveInput > 0 && Input.GetKeyDown(KeyCode.W))
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

            if (_isGrabbingLeft || _isGrabbingRight)
            {
                _isGrabbing = true;
            }
            else if (!_isGrabbingLeft && !_isGrabbingRight)
            {
                _isGrabbing = false;
            }

            foreach (var anim in _animator)
            {
                anim.SetBool("Grabbing", _isGrabbing);
                anim.SetBool("Grounded", isGrounded);
            }
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
            SoundManager._Instance.JumpSounds(jump);
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
