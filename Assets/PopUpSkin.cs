using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSkin : PopupUI<PopUpSkin>
{
    [SerializeField] private Button bt_Buy;
    [SerializeField] private Button bt_BuyAds;
    [SerializeField] private Button btn_Close;
    [SerializeField] private Button btn_Equip;

    [SerializeField] private Text txt_Gold;
    [SerializeField] private Text txt_Buy;
    [SerializeField] private Text txt_Equip;

    [SerializeField] private Image imageHideHorner;
    [SerializeField] private Image imageHideArm;
    [SerializeField] private Image imageHideShorts;
    [SerializeField] private Image imageHideSkin;

    public List<int> currentSkinSelectOwner;

    public List<ItemUIShop> containButtonCurrent;
    public ItemUIShop buttonItemCurrent;
    
    [SerializeField] private GameObject cointainItem;

    public TypeSkinShop typeSkin;

    public int priceCurrent = default;

    public int skinUsing = default;
    public int buttonItemIDCurrent;

    [SerializeField] private bool isCreateNew = true;
    private bool _isHadObject = false;
    protected override void Awake()
    {
        base.Awake();
        SetUp();
    }
    protected override void WillShowContent()
    {
        base.WillShowContent();
        UIManager.GetInstance().OpenShopSkin();
    }
    private void SetUp()
    {
        txt_Gold.text = GameManager.GetInstance().dataPlayer.gold.ToString();
        btn_Close.onClick.AddListener(OnClickedButtonClose);
        bt_Buy.onClick.AddListener(OnClickedButtonBuy);
        btn_Equip.onClick.AddListener(OnClickedButtonEquip);
        Data ower = GameManager.GetInstance().dataPlayer;
    }
    private void OnAllImageHide()
    {
        imageHideHorner.gameObject.SetActive(true);
        imageHideShorts.gameObject.SetActive(true);
        imageHideArm.gameObject.SetActive(true);
        imageHideSkin.gameObject.SetActive(true);
    }
    public void CheckSelectButtonSkin(TypeSkinShop typeSkinShop)
    {
        OnAllImageHide();
        switch (typeSkinShop)
        {
            case TypeSkinShop.HornSkin:
                imageHideHorner.gameObject.SetActive(false);
                break;
            case TypeSkinShop.ArmSkin:
                imageHideArm.gameObject.SetActive(false);
                break;
            case TypeSkinShop.ShortsSkin:
                imageHideShorts.gameObject.SetActive(false);
                break;
            case TypeSkinShop.Skin:
                imageHideSkin.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
    public void UnSelectButtonItem()
    {
        for (int i = 0; i < containButtonCurrent.Count; i++)
        {
            containButtonCurrent[i].UnSelectButton();
        }

    }
    public void SelectFirstButton()
    {
        UnSelectButtonItem();
        containButtonCurrent[0].SelectButton();
        if (containButtonCurrent[0].isOwnered)
        {
            OnOwner();
        }
    }

    private void OnClickedButtonEquip()
    {
        GameManager.GetInstance().dataPlayer.currentSkin = skinUsing;
        GameManager.GetInstance().dataPlayer.currentItemSkin = buttonItemIDCurrent;
        Close();
        //UIManager.GetInstance().ShowPopUpHome();
        UIManager.GetInstance().ShowPopUpHome(true);
    }
    private void OnClickedButtonClose()
    {
        Close();
        ManagerSkinUsing.GetInstance().ChangeSkinUsing();
        //UIManager.GetInstance().ShowPopUpHome();
        UIManager.GetInstance().ShowPopUpHome(true);
    }

    public void OnNotOwner(IDataSkin data)
    {
        bt_Buy.gameObject.SetActive(true);
        bt_BuyAds.gameObject.SetActive(true);
        btn_Equip.gameObject.SetActive(false);
        txt_Buy.text = data.price.ToString();
    }
    public void OnOwner()
    {
        bt_Buy.gameObject.SetActive(false);
        bt_BuyAds.gameObject.SetActive(false);
        btn_Equip.gameObject.SetActive(true);
        if (buttonItemIDCurrent == GameManager.GetInstance().dataPlayer.currentItemSkin && skinUsing == (int)GameManager.GetInstance().dataPlayer.currentItemSkin)
        {
            txt_Equip.text = "Equiped";
        }
        else txt_Equip.text = "Equip";
        buttonItemCurrent.OwneredItem();
    }
    public void ResetButton(DataSkin _dataSkin, List<int> _skinOwner)
    {
        if (containButtonCurrent.Count <= _dataSkin.iDataSkin.Length)
        {
            for (int i = 0; i < containButtonCurrent.Count; i++)
            {
                containButtonCurrent[i].SetDataButton(_skinOwner, _dataSkin.iDataSkin[i]);
            }
            for (int i = containButtonCurrent.Count; i < _dataSkin.iDataSkin.Length; i++)
            {
                SpawnButtonUI(cointainItem);
                containButtonCurrent[i].SetDataButton(_skinOwner, _dataSkin.iDataSkin[i]);
            }
        }
        else
        {
            for (int i = 0; i < _dataSkin.iDataSkin.Length; i++)
            {
                containButtonCurrent[i].SetDataButton(_skinOwner, _dataSkin.iDataSkin[i]);
            }
            for (int i = _dataSkin.iDataSkin.Length; i < containButtonCurrent.Count; i++)
            {
                containButtonCurrent[i].gameObject.SetActive(false);
            }
        }
        UnSelectButtonItem();
        SelectFirstButton();
        cointainItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(241.2885f, 0);
    }
    public ItemUIShop SpawnButtonUI(GameObject contain)
    {
        for (int i = 0; i < containButtonCurrent.Count; i++)
        {
            if (!containButtonCurrent[i].gameObject.activeSelf)
            {
                containButtonCurrent[i].gameObject.transform.SetParent(contain.transform);
                containButtonCurrent[i].gameObject.SetActive(true);
                containButtonCurrent[i].UnSelectButton();
                _isHadObject = true;
                return containButtonCurrent[i];
            }
            else
            {
                _isHadObject = false;
            }
        }
        if (isCreateNew)
        {
            if (!_isHadObject)
            {
                ItemUIShop more = Instantiate(containButtonCurrent[0], contain.transform);
                more.gameObject.SetActive(true);
                containButtonCurrent.Add(more);
                return more;
            }
        }
        return null;

    }
    public void OnClickedButtonBuy()
    {
        if (GameManager.GetInstance().dataPlayer.gold - priceCurrent < 0) return;
        GameManager.GetInstance().dataPlayer.gold -= priceCurrent;
        txt_Gold.text = GameManager.GetInstance().dataPlayer.gold.ToString();
        SetupData();
        GameManager.GetInstance().SaveData();
        OnOwner();
    }
    private void SetupData()
    {
        switch (typeSkin)
        {
            case TypeSkinShop.HornSkin:
                GameManager.GetInstance().dataPlayer.hornorsOwner.Add(buttonItemIDCurrent);
                break;
            case TypeSkinShop.ArmSkin:
                GameManager.GetInstance().dataPlayer.armOwner.Add(buttonItemIDCurrent);
                break;
            case TypeSkinShop.ShortsSkin:
                GameManager.GetInstance().dataPlayer.shortsOwner.Add(buttonItemIDCurrent);
                break;
            case TypeSkinShop.Skin:
                GameManager.GetInstance().dataPlayer.skinOwner.Add(buttonItemIDCurrent);
                break;
            default:
                break;
        }
    }
    public void SkinUsing()
    {
        switch (typeSkin)
        {
            case TypeSkinShop.HornSkin:
                skinUsing = 0;
                break;
            case TypeSkinShop.ArmSkin:
                skinUsing = 1;
                break;
            case TypeSkinShop.ShortsSkin:
                skinUsing = 2;
                break;
            case TypeSkinShop.Skin:
                skinUsing = 3;
                break;
            default:
                break;
        }
    }

}
public enum TypeSkinShop
{
    None,
    HornSkin,
    ShortsSkin,
    ArmSkin,
    Skin,
}
