using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]
public class Character_Controller : MonoBehaviour
{
    [SerializeField] private float scale_x = 4.339475f;
    [SerializeField] private float scale_y = 5.216864f;
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _attackDelay = 0.25f;

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _coyoteTime = 0.15f;
    [SerializeField] private float _jumpBufferTime = 0.15f;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask _groundLayer;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    private PlayerCombatSystem _pcs;
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _attackAction;


    private Vector2 _moveInput = Vector2.zero;
    private bool _isGrounded = false;
    private float _lastGroundedTime = -10f;
    private float _lastJumpPressedTime = -10f;
    private bool _canMove = true;
    private float _lastAttackTime = -10f;
    private bool _hasAttacked = false;

    private bool _isFacingRight = true;
    private void Reset()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _pcs = GetComponent<PlayerCombatSystem>();
    }
    private void Awake()
    {
        _rb = _rb ? _rb : GetComponent<Rigidbody2D>();
        _playerInput = _playerInput ? _playerInput : GetComponent<PlayerInput>();
        _animator = _animator ? _animator : GetComponent<Animator>();
        _spriteRenderer = _spriteRenderer ? _spriteRenderer : GetComponent<SpriteRenderer>();
        _pcs = _pcs ? _pcs : GetComponent<PlayerCombatSystem>();
        if (_playerInput == null)
        {
            Debug.LogError("Player Input привяжи нормально");
        }
    }
    private void OnEnable()
    {
        if (_playerInput != null && _playerInput.actions != null)
        {
            _moveAction = _playerInput.actions["Move"];
            _jumpAction = _playerInput.actions["Jump"];
            _attackAction = _playerInput.actions["Attack"];
            if (_moveAction != null)
            {
                _moveAction.Enable();
            }
            if (_jumpAction != null)
            {
                _jumpAction.Enable();
                _jumpAction.performed += OnJumpPerformed;
            }
            if (_attackAction != null)
            {
                _attackAction.Enable();
                _attackAction.performed += OnAttackPerformed;
            }
        }
    }



    private void OnDisable()
    {
        if (_moveAction != null)
        {
            _moveAction.Disable();
        }
        if (_jumpAction != null)
        {
            _jumpAction.Disable();
            _jumpAction.performed -= OnJumpPerformed;
        }
        if (_attackAction != null)
        {
            _attackAction.Disable();
            _attackAction.performed -= OnAttackPerformed;

        }
    }
    private void Update() 
    {
        if (_moveAction != null)
        {
            _moveInput = _moveAction.ReadValue<Vector2>();
        }
        else
        {
            _moveInput = Vector2.zero;
        }
        if (_groundCheck != null)
        {
            bool wasGrounded = _isGrounded;
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
            if (_isGrounded)
            {
                _lastGroundedTime = Time.time;
            }
        }
        if (_hasAttacked && Time.time - _lastAttackTime > _attackDelay)
        {
            _pcs.DeactivateSword();
            _hasAttacked = false;
            _canMove = true;
        }

        _animator.SetBool("IsRunning", _moveInput.x != 0);
        _animator.SetFloat("YVelocity", _rb.linearVelocityY);


        if (_moveInput.x > 0)
            _isFacingRight = true;
        else if (_moveInput.x < 0)
            _isFacingRight = false;
        if (_isFacingRight)
            transform.localScale = new Vector3(scale_x, scale_y, 1);
        else
            transform.localScale = new Vector3(-scale_x, scale_y, 1);

        /*        if (_moveInput != Vector2.zero)
                {
                    _animator.SetBool("IsRunning", true);
                }
                else
                {
                    _animator.SetBool("IsRunning", false);
                }
        */

        Debug.Log(_moveInput);
    }
    private void FixedUpdate()
    {
        if (!_canMove) return;

        Vector2 linearVelocity = _rb.linearVelocity;
        linearVelocity.x = _moveInput.x * _moveSpeed;
        _rb.linearVelocity = linearVelocity;

        bool canUseCoyote = (Time.time - _lastGroundedTime) <= _coyoteTime;
        bool hasBufferedJump = (Time.time - _lastJumpPressedTime) <= _jumpBufferTime;

        if (canUseCoyote && hasBufferedJump)
        {
            DoJump();
            _lastJumpPressedTime = -10f;
        }
    }
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
        _lastJumpPressedTime = Time.time;
    }
    private void DoJump()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

    }
    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        if (_isGrounded && !_hasAttacked)
        {
            _pcs.ActivateSword();
            _hasAttacked = true;
            _canMove = false;
            _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
            _lastAttackTime = Time.time;
            _animator.SetTrigger("AttackTrigger");
        }

    }
}

