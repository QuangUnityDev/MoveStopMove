using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerWeapon : Singleton<ManagerWeapon>
{
    [SerializeField] DataWeapon[] dataWeapons;

    public void ChangeSkinWeapon(Weapon weaponPlayer)
    {
        weaponPlayer.ChangSkinWeapon(dataWeapons[(int)GameManager.GetInstance().dataPlayer.equipedWeapon].iDataWeapon[GameManager.GetInstance().dataPlayer.equipedSkinWeapon].materialSkin);
    }
}
