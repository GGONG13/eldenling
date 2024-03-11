using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;

    [Header("스태미나 슬라이더 UI")]
    public Slider StaminaSliderUI;

    public float Stamina = 100;             // 스태미나
    public float MaxStamina = 100;          // 스태미나 최대량
    public bool isAttacking = false;        // 공격 중인지 나타내는 변수
    public bool isInvincible = false;       // 구르기 중 무적 상태인지 나타내는 변수
    public bool isAlive = true;             // 플레이어의 생존 상태

    public float UseRollingStamina = 15;
    public float UseRunningStamina = 5f;

    private float moveSpeed = 2f;           // 일반 속도
    private float runSpeed = 5f;            // 뛰는 속도
    private float staminaRecoveryRate = 60f;// 스태미너 회복 속도

    private bool _isWalking;
    private bool _isRunning;
    private bool _isRolling;
    public float rollSpeed = 5f;
    public float rollDuration = 1f;
    private float rollTimer;

    private Vector3 rollDirection;
    private float lastStaminaUseTime = -1f; // 마지막 스태미너 사용 시간 기록
    private float staminaRecoveryDelay = 1f; // 스태미너 회복 지연 시간 (초)

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        Stamina = MaxStamina;
    }

    void Update()
    {
        if (!isAlive) // 생존 상태가 아니면 모든 이동 및 액션 처리 중지
        {
            return;
        }

        UpdateStamina();
        if (!isAttacking)
        {
            HandleMovement();
            HandleRolling();
        }
    }

    private void HandleMovement()
    {
        if (_isRolling || isInvincible || !isAlive) return; // isAlive 조건 추가

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(h, 0, v);
        float movementMagnitude = direction.magnitude;

        _animator.SetFloat("Move", Mathf.Clamp01(movementMagnitude));

        if (movementMagnitude > 0.1f)
        {
            _isWalking = true;
            _animator.SetBool("Walk", true);

            Vector3 forward = Camera.main.transform.forward;
            forward.y = 0;
            direction = forward.normalized * v + Camera.main.transform.right * h;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

            _characterController.Move(direction.normalized * (_isRunning ? runSpeed : moveSpeed) * Time.deltaTime);
        }
        else
        {
            _isWalking = false;
            _animator.SetBool("Walk", _isWalking);
        }

        if (Input.GetKey(KeyCode.LeftShift) && Stamina > UseRunningStamina)
        {
            _isRunning = true;
            _animator.SetBool("Run", true);
            ReduceStamina(UseRunningStamina * Time.deltaTime);
        }
        else
        {
            _isRunning = false;
            _animator.SetBool("Run", _isRunning);
        }
    }

    private void UpdateStamina()
    {
        if (!isAttacking && Time.time > lastStaminaUseTime + staminaRecoveryDelay && Stamina < MaxStamina)
        {
            Stamina += staminaRecoveryRate * Time.deltaTime;
            Stamina = Mathf.Clamp(Stamina, 0, MaxStamina);
        }
        StaminaSliderUI.value = Stamina / MaxStamina;
    }

    private void HandleRolling()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isRolling && Stamina >= UseRollingStamina && isAlive) // isAlive 조건 추가
        {
            StartRolling();
        }

        if (_isRolling)
        {
            ContinueRolling();
        }
    }

    private void StartRolling()
    {
        _animator.SetTrigger("Roll");
        _isRolling = true;
        isInvincible = true;
        rollTimer = rollDuration;
        rollDirection = transform.forward;
        ReduceStamina(UseRollingStamina);
    }

    private void ContinueRolling()
    {
        rollTimer -= Time.deltaTime;
        if (rollTimer <= 0)
        {
            _isRolling = false;
            isInvincible = false;
        }
        else
        {
            _characterController.Move(rollDirection * rollSpeed * Time.deltaTime);
        }
    }

    public void ReduceStamina(float amount)
    {
        Stamina = Mathf.Clamp(Stamina - amount, 0, MaxStamina);
        lastStaminaUseTime = Time.time;
    }

    public void RestoreStamina(float amount)
    {
        Stamina = Mathf.Clamp(Stamina + amount, 0, MaxStamina);
    }

    // 플레이어 사망 처리 메서드
    public void OnPlayerDeath()
    {
        isAlive = false;
        _animator.SetTrigger("Die"); // 사망 애니메이션 트리거
    }
}
