using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;

    public float MoveSpeed = 2f; // 일반 속도
    public float RunSpeed = 5f; // 뛰는 속도

    private float _yVelocity = 0f;
   // private float _gravity = -20;// 중력

    private bool _isWalking;
    private bool _isRunning;

    private bool _isRolling;
    public float rollSpeed = 5f;
    public float rollDuration = 1f;
    private float rollTimer;
    private Vector3 rollDirection;

    

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _isRolling = false;
    }

    void Update()
    {
        float speed = MoveSpeed;
        _animator.SetBool("Walk", false);
        _animator.SetBool("Run", false);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(x: h, y: 0, z: v); // 로컬 좌표계 (나만의 동서남북)
        Vector3 unNormalizedDir = dir;
        dir.Normalize();

        dir = Camera.main.transform.TransformDirection(dir);

        _isWalking = false;
        _isRunning = false;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            _isWalking = true;
            _animator.SetBool("Walk", true);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = RunSpeed;
            _isRunning = true;
            _animator.SetBool("Run", true);
        }

        

        if (Input.GetKeyDown(KeyCode.Space) && !_isRolling)
        {
            _animator.SetTrigger("Roll");
            _isRolling = true;
            rollTimer = rollDuration;
            rollDirection = transform.forward;
        }

        if (_characterController.isGrounded)
        {
            _yVelocity = 0f;
            
        }

        //_yVelocity += _gravity * Time.deltaTime;

        dir.y = _yVelocity;

        if (_isRolling)
        {
            rollTimer -= Time.deltaTime;
            if (rollTimer <= 0)
            {
                _isRolling = false;
            }
            else
            {
                _characterController.Move(rollDirection * rollSpeed * Time.deltaTime);
            }
        }

        if (!_isRolling)
        {
            _characterController.Move(dir * speed * Time.deltaTime);
        }

        if (dir.magnitude > 0.1f)
        {
            Quaternion newRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * 10f);
        }

        _animator.SetFloat("Move", unNormalizedDir.magnitude);
    }
}