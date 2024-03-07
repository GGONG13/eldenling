using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;

    public float MoveSpeed = 2f; // �Ϲ� �ӵ�
    public float RunSpeed = 5f; // �ٴ� �ӵ�


    private float _yVelocity = 0f; // �߷�

    private bool _isWalking;
    private bool _isRuning;

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

        Vector3 dir = new Vector3(x: h, y: 0, z: v); // ���� ��ǥ�� (������ ��������)
        Vector3 unNormalizedDir = dir;
        dir.Normalize();

        dir = Camera.main.transform.TransformDirection(dir);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            _isWalking = true;
            _animator.SetBool("Walk", true);
            Debug.Log("Walk");
        }
            if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = RunSpeed;
            _isRuning = true;
            _animator.SetBool("Run",true);
            Debug.Log("Run");

        }
       

        if (Input.GetKeyDown(KeyCode.LeftControl) && !_isRolling)
        {
            _animator.SetTrigger("Roll");
            _isRolling = true;
            rollTimer = rollDuration;
            rollDirection = transform.forward;
            //PlayerStateManager.Instance.SetCurrentState(PlayerState.Roll);
        }

        if (_characterController.isGrounded)
        {
            _yVelocity = 0f;
        }

        dir.y = _yVelocity;

        if (_isRolling)
        {
            rollTimer -= Time.deltaTime;
            if (rollTimer <= 0)
            {
                _isRolling = false;
                // ������ ���� ó��
            }
            else
            {
                // ������ ���� �̵� ����
                _characterController.Move(rollDirection * rollSpeed * Time.deltaTime);
            }
        }
        // �̵��ϱ�



        // �÷��̾� ĳ������ ȸ��
        if (dir.magnitude > 0.1f)
        {
            Quaternion newRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * 10f);
        }
        if (!_isRolling)
        {
            _characterController.Move(dir * speed * Time.deltaTime);
        }
        _animator.SetFloat("Move", unNormalizedDir.magnitude);
        
        //  PlayerStateManager.Instance.SetCurrentState(PlayerState.Walk);
    }
}