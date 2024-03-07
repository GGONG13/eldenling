using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JS_PlayerMove : MonoBehaviour
{
    public float MoveSpeed = 5;     // 일반 속도
    public float RunSpeed = 10;    // 뛰는 속도

    [Header("플레이어 체력 슬라이더 UI")]
    public int Health;
    public int MaxHealth = 100;

    private CharacterController _characterController;
    private Animator _animator;

    public float speed;

    private float _gravity = -20;
    // - 누적할 중력 변수: y축 속도
    public float _yVelocity = 0f;

    private bool isRolling = false;
    public float rollSpeed = 5f;
    public float rollDuration = 1f;
    private float rollTimer;
    private Vector3 rollDirection;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        isRolling = false;
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. '캐릭터가 바라보는 방향'을 기준으로 방향구하기
        Vector3 dir = new Vector3(h, 0, v);             // 로컬 좌표계 (나만의 동서남북) 
        dir.Normalize();
        // Transforms direction from local space to world space.
        dir = Camera.main.transform.TransformDirection(dir); // 글로벌 좌표계 (세상의 동서남북)

        // 3-1. 중력 적용
        // 1. 중력 가속도가 누적된다.
        _yVelocity += _gravity * Time.deltaTime;

        // 2. 플레이어에게 y축에 있어 중력을 적용한다.

        dir.y = _yVelocity;

        // 3-2. 이동하기
        //transform.position += speed * dir * Time.deltaTime;
        _characterController.Move(dir * MoveSpeed * Time.deltaTime);
        _animator.SetFloat("Move", dir.magnitude);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = RunSpeed;
            _animator.SetTrigger("Run");
        }

        if (Input.GetKey(KeyCode.LeftControl) && !isRolling)
        {
            isRolling = true;
            rollTimer = rollDuration;
            rollDirection = transform.forward;
            _animator.SetTrigger("Roll");
        }

        if (isRolling)
        {
            rollTimer -= Time.deltaTime;
            if (rollTimer <= 0)
            {
                isRolling = false;
                // 구르기 종료 처리
            }
            else
            {
                // 구르기 동안 이동 실행
                _characterController.Move(rollDirection * rollSpeed * Time.deltaTime);
            }
        }
    }
}
