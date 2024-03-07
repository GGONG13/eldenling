using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JS_PlayerMove : MonoBehaviour
{
    public float MoveSpeed = 5;     // 일반 속도
    public float RunSpeed = 10;    // 뛰는 속도

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

    private bool SwordON = false;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        isRolling = false;
        SwordON = false;
    }

    private void Update()
    {


        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);             // 로컬 좌표계 (나만의 동서남북) 
        dir.Normalize();
        dir = Camera.main.transform.TransformDirection(dir); // 글로벌 좌표계 (세상의 동서남북)
        if (_characterController.isGrounded)
        {
            dir.y = _yVelocity;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = RunSpeed;
                _animator.SetTrigger("Run");
            }
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
        // _yVelocity += (_gravity * Time.deltaTime);
        // dir.y = _yVelocity;
        _characterController.Move(dir * MoveSpeed * Time.deltaTime);
        _animator.SetFloat("Move", dir.magnitude);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(Sword_Coroutine());
        }

        
    }
    public IEnumerator Sword_Coroutine()
    {
        SwordON = true;
        _animator.SetLayerWeight(2, 1f);
        _animator.SetTrigger("SwordMotion");
        UpdateSwordAnimation(1);
        _animator.SetFloat("Sword_", 1);
        yield return new WaitForSeconds(0.4f);
        _animator.SetLayerWeight(3, 1f);
    }
    public void UpdateSwordAnimation(float value)
    {
        _animator.SetFloat("Sword_", value);
    }
}
