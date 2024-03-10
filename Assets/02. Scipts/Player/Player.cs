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
        if (_playerMove.isInvincible || !_playerMove.isAlive)
        {
            Debug.Log("피했다"); // 무적 상태이거나 이미 사망했을 때 공격을 피했다는 메시지 출력
            return; // 무적 상태이거나 이미 사망한 경우 함수 종료
        }

        Health -= damage.Amount;
        Debug.Log($"Player: {Health}");
        if (Health <= 0)
        {
            Health = 0;
            HealthSliderUI.value = 0;
            _animator.SetTrigger("Death"); // 사망 애니메이션 트리거
            _playerMove.OnPlayerDeath(); // PlayerMove 클래스에서 이동 및 액션 처리 중지
            StartCoroutine(DeathWithDelay(5f)); // 사망 처리 지연
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
        _playerMove.isAlive = false; // 추가: PlayerMove 클래스의 isAlive 상태를 false로 설정
    }
}
