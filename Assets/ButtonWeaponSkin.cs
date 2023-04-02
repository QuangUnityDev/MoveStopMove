using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWeaponSkin : MonoBehaviour
{
    public int idSkinWeapon;
    public Sprite spriteButton;
    public Image imageButton;
    public int price;
    PopUpWeapon popUpWeapon;
    private void Awake()
    {
        transform.GetComponent<Button>().onClick.AddListener(OnClickButton);
        popUpWeapon = PopUpWeapon.GetInstance();
    }
    public void OnClickButton()
    {
        for (int i = 0; i < popUpWeapon.containButton.Count; i++)
        {
            popUpWeapon.containButton[i].imageButton.color = Color.white;
        }
        popUpWeapon.currentSelectingSkinWeapon = idSkinWeapon;
        popUpWeapon.skinCurrentImage.sprite = spriteButton;
        popUpWeapon.textPrice.text = price.ToString();
        popUpWeapon.priceCurrent = price;
        imageButton.color = Color.red;
        popUpWeapon.CheckEquip();
        popUpWeapon.CheckBuy();
    }
}
