using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;

    public float MoveSpeed = 2; // 일반 속도
    public float RunSpeed = 5; // 뛰는 속도

    public float Stamina = 100; // 스테미나
    public float MaxStamina = 100;    // 스태미나 최대량
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = MoveSpeed;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // 2. 방향구하기
        Vector3 dir = new Vector3(x: h, y: 0, z: v);          // 로컬 좌표계 (나만의 동서남북)
        Vector3 unNormalizedDir = dir;
        dir.Normalize();

        // 2. '캐릭터가 바라보는 방향'을 기준으로 방향구하기
        dir = Camera.main.transform.TransformDirection(dir); // 글로벌 좌표계 (세상의 동서남북)


        _characterController.Move(dir * speed * Time.deltaTime);
        _animator.SetFloat("Move", unNormalizedDir.magnitude);
    }
}
