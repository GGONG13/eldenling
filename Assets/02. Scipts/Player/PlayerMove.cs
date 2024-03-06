using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;

    public float MoveSpeed = 2; // �Ϲ� �ӵ�
    public float RunSpeed = 5; // �ٴ� �ӵ�

    public float Stamina = 100; // ���׹̳�
    public float MaxStamina = 100;    // ���¹̳� �ִ뷮
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
        // 2. ���ⱸ�ϱ�
        Vector3 dir = new Vector3(x: h, y: 0, z: v);          // ���� ��ǥ�� (������ ��������)
        Vector3 unNormalizedDir = dir;
        dir.Normalize();

        // 2. 'ĳ���Ͱ� �ٶ󺸴� ����'�� �������� ���ⱸ�ϱ�
        dir = Camera.main.transform.TransformDirection(dir); // �۷ι� ��ǥ�� (������ ��������)


        _characterController.Move(dir * speed * Time.deltaTime);
        _animator.SetFloat("Move", unNormalizedDir.magnitude);
    }
}
