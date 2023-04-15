using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUISkin : MonoBehaviour
{
    [SerializeField] private List<int> currentSkinOwner;
    [SerializeField] private TypeSkinShop typeSkin;
    [SerializeField] private DataSkin data_TypeSkin;
    public static bool isHide;
    private void Awake()
    {
        Button btnSelf = transform.GetComponent<Button>();
        SetDataSkinOwnered();
        if (typeSkin == TypeSkinShop.HornSkin) OnClickedButtonSelf();
        btnSelf.onClick.AddListener(OnClickedButtonSelf);
    }
    private void OnClickedButtonSelf()
    {
        PopUpSkin.instance.typeSkin = typeSkin;
        PopUpSkin.instance.SkinUsing();
        PopUpSkin.instance.CheckSelectButtonSkin(typeSkin);
        PopUpSkin.instance.ResetButton(data_TypeSkin, currentSkinOwner);
        PopUpSkin.instance.currentSkinSelectOwner = currentSkinOwner;
    }
    private void SetDataSkinOwnered()
    {
        switch (typeSkin)
        {
            case TypeSkinShop.None:
                break;
            case TypeSkinShop.HornSkin:
                currentSkinOwner = GameManager.GetInstance().dataPlayer.hornorsOwner;
                break;
            case TypeSkinShop.ShortsSkin:
                currentSkinOwner = GameManager.GetInstance().dataPlayer.shortsOwner;
                break;
            case TypeSkinShop.ArmSkin:
                currentSkinOwner = GameManager.GetInstance().dataPlayer.armOwner;
                break;
            case TypeSkinShop.Skin:
                currentSkinOwner = GameManager.GetInstance().dataPlayer.skinOwner;
                break;
            default:
                break;
        }        
    }
}
