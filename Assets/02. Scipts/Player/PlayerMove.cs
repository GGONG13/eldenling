using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;

    public float MoveSpeed = 2f; // 일반 속도
    public float RunSpeed = 5f; // 뛰는 속도

    private float _yVelocity = 0f; // 중력

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

        Vector3 dir = new Vector3(x: h, y: 0, z: v); // 로컬 좌표계 (나만의 동서남북)
        Vector3 unNormalizedDir = dir;
        dir.Normalize();

        dir = Camera.main.transform.TransformDirection(dir);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = RunSpeed;
            _animator.SetTrigger("Run");
        }


        if (_characterController.isGrounded)
        {
            _yVelocity = 0f;
        }

        dir.y = _yVelocity;

        // 이동하기
        _characterController.Move(dir * speed * Time.deltaTime);

        // 플레이어 캐릭터의 회전
        if (dir.magnitude > 0.1f)
        {
            Quaternion newRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * 10f);
        }

        _animator.SetFloat("Move", unNormalizedDir.magnitude);
    }
}
