using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shield : MonoBehaviour
{
    public bool _isDefenc;
    public Animator _animator;

    private void Awake()
    {
        // 해당 게임 오브젝트에 있는 Animator 컴포넌트 참조를 가져옴
        _animator = GetComponent<Animator>();
        _isDefenc = false;
    }

    void Update()
    {
        // 마우스 오른쪽 버튼을 누르고 있으면 방패 들기 시작
        if (Input.GetMouseButtonDown(1))
        {
            BeginShieldDefenc();
        }
        // 마우스 오른쪽 버튼에서 손을 떼면 방패 들기 중단
        else if (Input.GetMouseButtonUp(1))
        {
            EndShieldDefenc();
        }
    }

    public void BeginShieldDefenc()
    {
        _isDefenc = true;
        _animator.SetBool("IsDefending", true); // 애니메이터에 방패 들기 상태 전달
    }

    public void EndShieldDefenc()
    {
        _isDefenc = false;
        _animator.SetBool("IsDefending", false); // 애니메이터에 방패 들기 해제 상태 전달
    }
}
