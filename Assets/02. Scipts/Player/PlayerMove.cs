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

    public float UseRollingStamina = 15;
    public float UseRunningStamina = 5f;

    private float moveSpeed = 2f;           // 일반 속도
    private float runSpeed = 5f;            // 뛰는 속도
    private float staminaRecoveryRate = 40f;// 스태미너 회복 속도

    private float _yVelocity = 0f;

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
        UpdateStamina();

        if (isAttacking)
        {
            return;
        }

        HandleMovement();
        HandleRolling();
    }

    private void HandleMovement()
    {
        if (_isRolling || isInvincible) return;

        float speed = _isRunning ? runSpeed : moveSpeed;
        _animator.SetBool("Walk", false);
        _animator.SetBool("Run", false);

        Vector3 direction = Vector3.zero;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        _animator.SetFloat("Move", direction.magnitude);
        if (h != 0 || v != 0)
        {
            direction = new Vector3(h, 0, v);
            _isWalking = true;
            _animator.SetBool("Walk", true);

            Vector3 forward = Camera.main.transform.forward;
            forward.y = 0; // Y축 고정
            direction = forward.normalized * v + Camera.main.transform.right * h;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
        else
        {
            _isWalking = false;
        }

        if (Input.GetKey(KeyCode.LeftShift) && Stamina > UseRunningStamina)
        {
            _isRunning = true;
            _animator.SetBool("Run", true);
            ReduceStamina(UseRunningStamina * Time.deltaTime);
        }

        Vector3 move = direction * speed * Time.deltaTime;
        _characterController.Move(move);
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
        if (Input.GetKeyDown(KeyCode.Space) && !_isRolling && Stamina >= UseRollingStamina)
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
}
