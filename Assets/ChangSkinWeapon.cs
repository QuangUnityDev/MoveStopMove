using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangSkinWeapon : MonoBehaviour
{
    public GameObject[] skinWeapon;
    public void ChangeSkin(int typeSkin)
    {
        for (int i = 0; i < skinWeapon.Length; i++)
        {
            skinWeapon[i].gameObject.SetActive(false);
        }
        skinWeapon[typeSkin].gameObject.SetActive(true);
    }
}
