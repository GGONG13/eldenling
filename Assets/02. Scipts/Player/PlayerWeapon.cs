using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerWeaponState
{
    Sword,
    MagicWand,
}

public class PlayerWeapon : MonoBehaviour
{
    public static PlayerWeapon Instance { get; private set; }
    private PlayerWeaponState _currentState = PlayerWeaponState.Sword;


    public GameObject Sword;
    public GameObject MagicWand;
    public Vector3 _target;
    public GameObject inventoryUI;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ������: �� ��ȯ �� �ı����� �ʵ��� ��
            inventoryUI.SetActive(false);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("Tab key pressed.");
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    public void WeaponSwitch(PlayerWeaponState playerWeaponState)
    {
        _currentState = playerWeaponState;
        switch (_currentState) 
        {
            case PlayerWeaponState.Sword:
                Sword.gameObject.SetActive(true);
                MagicWand.gameObject.SetActive(false);
            break;
            case PlayerWeaponState.MagicWand:
                MagicWand.gameObject.SetActive(true);
                Sword.gameObject.SetActive(false);
            break;
        }
        transform.position = _target;
    }
  
}
