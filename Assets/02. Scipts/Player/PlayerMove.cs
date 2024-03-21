using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;
    private Player_Shield playerShield;


    [Header("���¹̳� �����̴� UI")]
    public Slider StaminaSliderUI;

    public float Stamina = 100;             // ���¹̳�
    public float MaxStamina = 100;          // ���¹̳� �ִ뷮
    public bool isAttacking = false;        // ���� ������ ��Ÿ���� ����
    public bool isInvincible = false;       // ������ �� ���� �������� ��Ÿ���� ����
    public bool isAlive = true;             // �÷��̾��� ���� ����

    public float UseRollingStamina = 15;
    public float UseRunningStamina = 5f;

    private float moveSpeed = 3f;           // �Ϲ� �ӵ�
    private float runSpeed = 8f;            // �ٴ� �ӵ�
    private float staminaRecoveryRate = 60f;// ���¹̳� ȸ�� �ӵ�

    private bool _isWalking;
    private bool _isRunning;
    public bool _isRolling;
    public bool _isDefending;
    
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
        playerShield = GetComponent<Player_Shield>();
    }

    private void Start()
    {
        Stamina = MaxStamina;
    }

    void Update()
    {
        
        if (!isAlive) // ���� ���°� �ƴϸ� ��� �̵� �� �׼� ó�� ����
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
        // �޸��� ������ ��
        if (_isRunning)
        {
            // ��: �޸��� ���¿����� ȸ�� �ӵ��� 1.5��� ����
            SendMessage("SetRotationSpeed", 2f, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            // �ȱ� ���¿����� �⺻ ȸ�� �ӵ� ���
            SendMessage("SetRotationSpeed", 1f, SendMessageOptions.DontRequireReceiver);
        }
    }
    private void HandleMovement()
    {
        if (_isRolling || isInvincible || !isAlive || playerShield._isDefending ==true) return; // isAlive ���� �߰�

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
        if (Input.GetKeyDown(KeyCode.Space) && !_isRolling && Stamina >= UseRollingStamina && isAlive) // isAlive ���� �߰�
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

    // �÷��̾� ��� ó�� �޼���
    public void OnPlayerDeath()
    {
        isAlive = false;
        _animator.SetTrigger("Die"); // ��� �ִϸ��̼� Ʈ����
    }
}
