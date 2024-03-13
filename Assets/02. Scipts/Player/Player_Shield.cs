using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shield : MonoBehaviour
{
    public bool _isDefenc;
    public Animator _animator;

    private void Awake()
    {
        // �ش� ���� ������Ʈ�� �ִ� Animator ������Ʈ ������ ������
        _animator = GetComponent<Animator>();
        _isDefenc = false;
    }

    void Update()
    {
        // ���콺 ������ ��ư�� ������ ������ ���� ��� ����
        if (Input.GetMouseButtonDown(1))
        {
            BeginShieldDefenc();
        }
        // ���콺 ������ ��ư���� ���� ���� ���� ��� �ߴ�
        else if (Input.GetMouseButtonUp(1))
        {
            EndShieldDefenc();
        }
    }

    public void BeginShieldDefenc()
    {
        _isDefenc = true;
        _animator.SetBool("IsDefending", true); // �ִϸ����Ϳ� ���� ��� ���� ����
    }

    public void EndShieldDefenc()
    {
        _isDefenc = false;
        _animator.SetBool("IsDefending", false); // �ִϸ����Ϳ� ���� ��� ���� ���� ����
    }
}
