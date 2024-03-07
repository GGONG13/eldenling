using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Walk,
    Run,
    Jump,
    Roll,
    WeaponIdle,
    WeaponWalk,
    WeaponRun,
    WeaponJump,
    WeaponRoll
}

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager Instance { get; private set; }

    private PlayerState _currentState = PlayerState.Idle;

    private CharacterController _characterController;
    private Animator _animator;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
        }
        else
        {
            Destroy(gameObject);
        }
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }
    public PlayerState GetCurrentState()
    {
        return _currentState;
    }

    public void SetCurrentState(PlayerState newState)
    {
        _currentState = newState;
    }

    private void Update()
    {
        CheckPlayerState();
    }

    void CheckPlayerState()
    {
        switch (_currentState)
        {
            case PlayerState.Idle:
                
                break;
            case PlayerState.Walk:
                
                break;
            case PlayerState.Run:
                
                break;
            case PlayerState.Jump:
                
                break;
            case PlayerState.Roll:
                
                break;
            case PlayerState.WeaponIdle:
                
                break;
            case PlayerState.WeaponWalk:
                
                break;
            case PlayerState.WeaponRun:
                
                break;
            case PlayerState.WeaponJump:
               
                break;
            case PlayerState.WeaponRoll:
                
                break;
            default:
                
                break;
        }
    }
}
