using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Animator _animator;
    private PlayerMove _playerMove; // PlayerMove 클래스에 대한 참조

    [Header("체력 슬라이더 UI")]
    public Slider HealthSliderUI;

    public int Health;
    public int MaxHealth = 100;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerMove = GetComponent<PlayerMove>(); // PlayerMove 클래스에 대한 참조 초기화
        Health = MaxHealth;
    }

    private void Update()
    {
        HealthSliderUI.value = Health / (float)MaxHealth;
    }

    public void Hit(DamageInfo damage)
    {
        if (_playerMove.isInvincible)
        {
            Debug.Log("피했다"); // 무적 상태일 때 공격을 피했다는 메시지 출력
            return; // 무적 상태일 경우 여기서 함수 종료
        }

        // 무적 상태가 아닐 때의 피해 처리 로직
        Health -= damage.Amount;
        Debug.Log($"Player: {Health}");
        if (Health <= 0)
        {
            HealthSliderUI.value = 0f;
            Health = 0;
            _animator.SetTrigger("Death");
            StartCoroutine(DeathWithDelay(5f));
        }
    }

    private IEnumerator DeathWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Death();
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }
}
