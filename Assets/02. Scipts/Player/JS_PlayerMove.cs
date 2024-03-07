using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JS_PlayerMove : MonoBehaviour
{
    public float MoveSpeed = 5;     // �Ϲ� �ӵ�
    public float RunSpeed = 10;    // �ٴ� �ӵ�

    private CharacterController _characterController;
    private Animator _animator;

    public float speed;

    private float _gravity = -20;
    // - ������ �߷� ����: y�� �ӵ�
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
        Vector3 dir = new Vector3(h, 0, v);             // ���� ��ǥ�� (������ ��������) 
        dir.Normalize();
        dir = Camera.main.transform.TransformDirection(dir); // �۷ι� ��ǥ�� (������ ��������)
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
                // ������ ���� ó��
            }
            else
            {
                // ������ ���� �̵� ����
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
