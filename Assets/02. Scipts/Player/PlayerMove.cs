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

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float speed = MoveSpeed;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(x: h, y: 0, z: v); // ���� ��ǥ�� (������ ��������)
        Vector3 unNormalizedDir = dir;
        dir.Normalize();

        dir = Camera.main.transform.TransformDirection(dir);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = RunSpeed;
        }

        if (_characterController.isGrounded)
        {
            _yVelocity = 0f;
        }

        dir.y = _yVelocity;

        // �̵��ϱ�
        _characterController.Move(dir * speed * Time.deltaTime);

        // �÷��̾� ĳ������ ȸ��
        if (dir.magnitude > 0.1f)
        {
            Quaternion newRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * 10f);
        }

        _animator.SetFloat("Move", unNormalizedDir.magnitude);
    }
}
