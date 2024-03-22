using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shield : MonoBehaviour
{
    private PlayerMove playerMove;
    public bool _isDefending ;
    public bool _isParrying;
    public Animator _animator;

    public GameObject ShieldEffect;
    public GameObject DefenseEffect;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        // �ش� ���� ������Ʈ�� �ִ� Animator ������Ʈ ������ ������
        _animator = GetComponent<Animator>();
        _isDefending = false;
        ShieldEffect.gameObject.SetActive(false);
        DefenseEffect.gameObject.SetActive(false);
    }

    void Update()
    {
        // ���콺 ������ ��ư�� ������ ������ ���� ��� ����
        if (Input.GetMouseButtonDown(1) && playerMove._isRolling == false)
        {
            BeginShieldDefenc();
            _animator.SetBool("ShieldUP", true);
        }
        // ���콺 ������ ��ư���� ���� ���� ���� ��� �ߴ�
        else if (Input.GetMouseButtonUp(1))
        {
            EndShieldDefenc();
            _animator.SetBool("ShieldUP", false);
        }

        if(_isDefending == true)
        {
            DefenseEffect.SetActive(true);
        }
        else if (_isDefending == false)
        {
            DefenseEffect.SetActive(false);
        }
    }

    public void BeginShieldDefenc()
    {
        _isDefending = true;
        
        if (Input.GetMouseButton(1))
        {
            _animator.SetBool("IsDefending", true);
            

        }


    }

    public void EndShieldDefenc()
    {
        
        _isDefending = false;
        _animator.SetBool("IsDefending", false);

    }

    public void BeginShieldParrying()
    {
        ShieldEffect.SetActive(true);
        _isParrying = true;
        if (Input.GetMouseButton(1))
        {
            _animator.SetBool("IsDefending", true);

        }
      
    }
    public void EndParrying()
    {
        _isParrying = false;
        _animator.SetTrigger("CancelDefending");
    }

    public void RealEneParrying()
    {
        ShieldEffect.SetActive(false);
        _animator.ResetTrigger("CancelDefending");
    }
    public void ParryingSuccess()
    {
        _animator.SetTrigger("ParryingEnd ");
    }
 }
