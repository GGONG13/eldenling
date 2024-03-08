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
    public float MaxStamina = 100;    // 스태미나 최대량

    public float UseRollingStamina = 15;
    public float UseRuningStamina = 5f;


    private float moveSpeed = 2f; // 일반 속도
    private float runSpeed = 5f; // 뛰는 속도
    private float staminaRecoveryRate = 15f; // 스태미너 회복 속도

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
        _isRolling = false;
        Stamina = MaxStamina;
    }

    void Update()
    {
        UpdateStamina();

        float speed = _isRunning ? runSpeed : moveSpeed;
        _animator.SetBool("Walk", false);
        _animator.SetBool("Run", false);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(x: h, y: 0, z: v);
        Vector3 unNormalizedDir = dir;
        dir.Normalize();

        dir = Camera.main.transform.TransformDirection(dir);

        _isWalking = false;
        _isRunning = false;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            _isWalking = true;
            _animator.SetBool("Walk", true);
        }

        if (Input.GetKey(KeyCode.LeftShift) && Stamina > 0)
        {
            _isRunning = true;
            _animator.SetBool("Run", true);
            ReduceStamina(UseRuningStamina * Time.deltaTime); // 달리기 중 스태미너 감소
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_isRolling && Stamina >= UseRollingStamina)
        {
            _animator.SetTrigger("Roll");
            _isRolling = true;
            rollTimer = rollDuration;
            rollDirection = transform.forward;
            ReduceStamina(UseRollingStamina); // 롤링 시 스태미너 감소
        }

        if (_characterController.isGrounded)
        {
            _yVelocity = 0f;
        }

        dir.y = _yVelocity;

        if (_isRolling)
        {
            rollTimer -= Time.deltaTime;
            if (rollTimer <= 0)
            {
                _isRolling = false;
            }
            else
            {
                _characterController.Move(rollDirection * rollSpeed * Time.deltaTime);
            }
        }
        else
        {
            _characterController.Move(dir * speed * Time.deltaTime);
            if (Time.time > lastStaminaUseTime + staminaRecoveryDelay && Stamina < MaxStamina)
            {
                RestoreStamina(staminaRecoveryRate * Time.deltaTime); // 스태미너 회복 로직
            }
        }

        if (dir.magnitude > 0.1f)
        {
            Quaternion newRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * 10f);
        }

        _animator.SetFloat("Move", unNormalizedDir.magnitude);
    }

    private void UpdateStamina()
    {
        StaminaSliderUI.value = Stamina / MaxStamina;
    }

    public void ReduceStamina(float amount)
    {
        Stamina = Mathf.Clamp(Stamina - amount, 0f, MaxStamina);
        lastStaminaUseTime = Time.time; // 스태미너 사용 시간 갱신
    }

    public void RestoreStamina(float amount)
    {
        Stamina = Mathf.Clamp(Stamina + amount, 0f, MaxStamina);
    }
}
