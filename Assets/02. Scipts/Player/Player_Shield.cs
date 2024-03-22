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
        // 해당 게임 오브젝트에 있는 Animator 컴포넌트 참조를 가져옴
        _animator = GetComponent<Animator>();
        _isDefending = false;
        ShieldEffect.gameObject.SetActive(false);
        DefenseEffect.gameObject.SetActive(false);
    }

    void Update()
    {
        // 마우스 오른쪽 버튼을 누르고 있으면 방패 들기 시작
        if (Input.GetMouseButtonDown(1) && playerMove._isRolling == false)
        {
            BeginShieldDefenc();
            _animator.SetBool("ShieldUP", true);
        }
        // 마우스 오른쪽 버튼에서 손을 떼면 방패 들기 중단
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
