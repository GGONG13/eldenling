using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;
    private Player_Shield playerShield;


    [Header("스태미나 슬라이더 UI")]
    public Slider StaminaSliderUI;

    public float Stamina = 100;             // 스태미나
    public float MaxStamina = 100;          // 스태미나 최대량
    public bool isAttacking = false;        // 공격 중인지 나타내는 변수
    public bool isInvincible = false;       // 구르기 중 무적 상태인지 나타내는 변수
    public bool isAlive = true;             // 플레이어의 생존 상태

    public float UseRollingStamina = 15;
    public float UseRunningStamina = 5f;

    private float moveSpeed = 3f;           // 일반 속도
    private float runSpeed = 8f;            // 뛰는 속도
    private float staminaRecoveryRate = 60f;// 스태미너 회복 속도

    private bool _isWalking;
    private bool _isRunning;
    public bool _isRolling;
    public bool _isDefending;
    
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
        playerShield = GetComponent<Player_Shield>();
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
        HandleMovement();
        UpdateStamina();
        CheckMovementState();
        if (!isAttacking )
        {
            
            HandleRolling();
            
        }
        if (!_characterController.isGrounded)
        {
            Vector3 gravityMove = Vector3.down * 9.82f * Time.deltaTime;
            _characterController.Move(gravityMove);
        }
    }
    void CheckMovementState()
    {
        // 달리기 상태일 때
        if (_isRunning)
        {
            // 예: 달리기 상태에서는 회전 속도를 1.5배로 증가
            SendMessage("SetRotationSpeed", 2f, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            // 걷기 상태에서는 기본 회전 속도 사용
            SendMessage("SetRotationSpeed", 1f, SendMessageOptions.DontRequireReceiver);
        }
    }
    private void HandleMovement()
    {
        if (_isRolling || isInvincible || !isAlive || playerShield._isDefending ==true) return; // isAlive 조건 추가

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
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Walk);
        }
        else
        {
            _isWalking = false;
            _animator.SetBool("Walk", _isWalking);
            AudioManager.instance.StopSfx(AudioManager.Sfx.Walk);
        }

        if (Input.GetKey(KeyCode.LeftShift) && Stamina > UseRunningStamina)
        {
            _isRunning = true;
            _animator.SetBool("Run", true);
            ReduceStamina(UseRunningStamina * Time.deltaTime);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Run);
        }
        else
        {
            _isRunning = false;
            _animator.SetBool("Run", _isRunning);
            AudioManager.instance.StopSfx(AudioManager.Sfx.Run);
        }
    }

    private void UpdateStamina()
    {
        if ((!isAttacking || _isRolling == false )&& Time.time > lastStaminaUseTime + staminaRecoveryDelay && Stamina < MaxStamina)
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
        _animator.SetBool("IsDefending", false);
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
