using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;

    [Header("���¹̳� �����̴� UI")]
    public Slider StaminaSliderUI;

    public float Stamina = 100;             // ���¹̳�
    public float MaxStamina = 100;    // ���¹̳� �ִ뷮

    public float UseRollingStamina = 15;
    public float UseRuningStamina = 5f;


    private float moveSpeed = 2f; // �Ϲ� �ӵ�
    private float runSpeed = 5f; // �ٴ� �ӵ�
    private float staminaRecoveryRate = 15f; // ���¹̳� ȸ�� �ӵ�

    private float _yVelocity = 0f;

    private bool _isWalking;
    private bool _isRunning;

    private bool _isRolling;
    public float rollSpeed = 5f;
    public float rollDuration = 1f;
    private float rollTimer;

    private Vector3 rollDirection;
    private float lastStaminaUseTime = -1f; // ������ ���¹̳� ��� �ð� ���
    private float staminaRecoveryDelay = 1f; // ���¹̳� ȸ�� ���� �ð� (��)

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
            ReduceStamina(UseRuningStamina * Time.deltaTime); // �޸��� �� ���¹̳� ����
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_isRolling && Stamina >= UseRollingStamina)
        {
            _animator.SetTrigger("Roll");
            _isRolling = true;
            rollTimer = rollDuration;
            rollDirection = transform.forward;
            ReduceStamina(UseRollingStamina); // �Ѹ� �� ���¹̳� ����
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
                RestoreStamina(staminaRecoveryRate * Time.deltaTime); // ���¹̳� ȸ�� ����
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
        lastStaminaUseTime = Time.time; // ���¹̳� ��� �ð� ����
    }

    public void RestoreStamina(float amount)
    {
        Stamina = Mathf.Clamp(Stamina + amount, 0f, MaxStamina);
    }
}
