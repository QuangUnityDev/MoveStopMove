using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ButtonWeaponSkin : MonoBehaviour
{
    public int idSkinWeapon;
    public Sprite spriteButton;
    public Image imageButton;
    public int price;
    PopUpWeapon popUpWeapon;
    public Image imageLock;
    public bool isUnLock;
    private void Awake()
    {
        transform.GetComponent<Button>().onClick.AddListener(OnClickButton);
        popUpWeapon = PopUpWeapon.instance;
        isUnLock = false;
    }
    public void OnClickButton()
    {
        for (int i = 0; i < popUpWeapon.containButton.Count; i++)
        {
            popUpWeapon.containButton[i].imageButton.color = Color.white;
        }
        popUpWeapon.currentSelectingSkinWeapon = idSkinWeapon;
        Transform go = popUpWeapon.skinWeaponCurrent.transform;
        go.DOMoveX(-idSkinWeapon * 2, 1);
        popUpWeapon.textPrice.text = price.ToString();
        popUpWeapon.priceCurrent = price;
        imageButton.color = Color.red;
        popUpWeapon.CheckEquip();
        popUpWeapon.CheckBuy();
        if (isUnLock) 
        {
            imageLock.gameObject.SetActive(false);
            popUpWeapon.bt_UnlockAds.gameObject.SetActive(false); }
        else popUpWeapon.bt_UnlockAds.gameObject.SetActive(true);
    }
}
