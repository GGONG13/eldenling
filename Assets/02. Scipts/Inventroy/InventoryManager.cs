using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryUI;

    void Start()
    {
        inventoryUI.SetActive(false);
    }

    public void Update()
    {

    }

    public void OnWeaponAButtonClicked()
    {
        PlayerWeapon.Instance.WeaponSwitch(PlayerWeaponState.Sword);
    }
    public void OnWeaponBButtonClicked() 
    {
        PlayerWeapon.Instance.WeaponSwitch(PlayerWeaponState.MagicWand);
    }
}
