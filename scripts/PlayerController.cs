using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    [SerializeField] private TMP_Text scoreText;
    
    private State _state = State.Idle;
    private Rigidbody2D _rb;
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float thrust = 30.0f;
    private int _lastMoveX;
    
    private AudioSource _audioSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landSound;

    [SerializeField] private LayerMask enemyLayerMask;
    
    private bool _isGrounded;
    private bool _jumpPressed;

    private int _health = 100;

    private int _score = 0;
    
    public Action<int> OnCollectibleCollected;
    
    private enum State
    {
        Idle,
        Walk,
    }
    
    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    
    public event Action<int> OnHealthChanged;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;

        OnCollectibleCollected += (points) => { 
            _score += points;
            scoreText.text = _score.ToString();
        };
        
    }

    // Update is called once per frame
    private void Update()
    {
        var x = Input.GetAxisRaw("Horizontal");
        
        switch (_state)
        {
            case State.Idle:
                animator.SetBool(IsWalking, false);
                if (x != 0)
                {
                    _state = State.Walk;
                }
                break;    
            case State.Walk:
                animator.SetBool(IsWalking, true);
                spriteRenderer.flipX = _lastMoveX > 0;
                _lastMoveX = x > 0 ? 1 : -1;
                if (x == 0)
                {
                    _state = State.Idle;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpPressed = true;
            animator.SetBool(IsJumping, true);
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            var enemy = Physics2D.OverlapCircle(transform.position, 0.5f, enemyLayerMask);
            if (enemy != null) {
                enemy.GetComponent<Enemy>().onHit.Invoke();
            }
        }

    }
    private void FixedUpdate()
    {
        var x = Input.GetAxisRaw("Horizontal");

        var velocity = new Vector2(x * speed * Time.fixedDeltaTime, _rb.linearVelocity.y);

        _rb.linearVelocity = velocity;
        
        CheckGrounded();
        
        if (_jumpPressed && _isGrounded)
        {
            _rb.AddForce(Vector2.up * thrust, ForceMode2D.Impulse);
            //_audioSource.PlayOneShot(jumpSound);
            _isGrounded = false;
            _jumpPressed = false;
        }
    }
    
    private void CheckGrounded()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.down, 0.15f);
        if (!_isGrounded && hit.collider is not null)
        {
           // _audioSource.PlayOneShot(landSound);
            _isGrounded = true;
            animator.SetBool(IsJumping, false);
        }
        
    }

    public void TakeDamage(int damage) {
        _health -= damage;
        OnHealthChanged.Invoke(_health);
    }
}