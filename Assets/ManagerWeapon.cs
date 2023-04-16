using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerWeapon : Singleton<ManagerWeapon>
{
    [SerializeField] DataWeapon[] dataWeapons;
    public Material[] materials;
    public Shader shader1;
    public void ChangeSkinWeapon(Weapon weaponPlayer)
    {
        weaponPlayer.GetComponent<ChangSkinWeapon>().ChangeSkin(GameManager.GetInstance().dataPlayer.equipedSkinWeapon);
    }
    public void ChangeSkinWeaponRandom(Weapon weaponPlayer)
    {
        int randomSkin = Random.Range(0, 5);
        weaponPlayer.GetComponent<ChangSkinWeapon>().ChangeSkin(randomSkin);
    }
}
