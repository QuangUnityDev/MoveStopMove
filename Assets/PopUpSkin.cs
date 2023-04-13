using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSkin : PopupUI<PopUpSkin>
{
    [SerializeField] private Button bt_HornSkin;
    [SerializeField] private Button bt_ShortsSkin;
    [SerializeField] private Button bt_ArmSkin;
    [SerializeField] private Button bt_SkinShop;

    [SerializeField] private GameObject containButtonBuy;
    [SerializeField] private Button bt_Buy;
    [SerializeField] private Button bt_BuyAds;
    [SerializeField] private Button btn_Close;
    [SerializeField] private Text txt_Gold;

    public Text txt_Buy;

    [SerializeField] private Image imageSkinShop;
    [SerializeField] private Image imageArmShop;
    [SerializeField] private Image imageHornShop;
    [SerializeField] private Image imageShortsShop;
    private Image imageCurrent;

    public List<ItemUIShop> containButtonCurrent;


    [SerializeField] private DataSkin data_HornSkin;
    [SerializeField] private DataSkin data_ShortsSkin;
    [SerializeField] private DataSkin data_ArmSkin;
    [SerializeField] private DataSkin data_Skin;

    public DataSkin data_SkinCurrent;

    [SerializeField] private GameObject cointainItem;

    [SerializeField] private bool isCreateNew = true;


    List<int> currentHornOwner;
    List<int> currentShortsSkinOwner;
    List<int> currentArmSkinOwner;
    List<int> currentSkinOwner;


    public int currentUsingSkin;
    public int priceCurrent = default;

    private TypeSkinShop currentPopUpSkin;
    void SelectingImageButton(Image image)
    {
        imageSkinShop.gameObject.SetActive(true);
        imageArmShop.gameObject.SetActive(true);
        imageHornShop.gameObject.SetActive(true);
        imageShortsShop.gameObject.SetActive(true);
        imageCurrent = image;
        imageCurrent.gameObject.SetActive(false);
    }
    private bool _isHadObject = false;
    protected override void Awake()
    {
        base.Awake();
        SetUp();       
    }
    private void SetUp()
    {
        txt_Gold.text = GameManager.GetInstance().dataPlayer.gold.ToString();
        btn_Close.onClick.AddListener(OnClickedButtonClose);
        bt_HornSkin.onClick.AddListener(GenHornSkinShop);
        bt_ShortsSkin.onClick.AddListener(GenShortsSkinShop);
        bt_SkinShop.onClick.AddListener(GenSkinShop);
        bt_ArmSkin.onClick.AddListener(GenArmShop);
        bt_Buy.onClick.AddListener(OnClickedButtonBuy);
        Data ower = GameManager.GetInstance().dataPlayer;
        currentHornOwner = ower.hornorsOwner;
        currentSkinOwner = ower.skinOwner;
        currentArmSkinOwner = ower.armOwner;
        currentShortsSkinOwner = ower.shortsOwner;
    }
    private void OnClickedButtonClose()
    {
        Close();
        ShowPopUp.ShowPopUps(StringNamePopup.PopupHome);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        currentPopUpSkin = TypeSkinShop.None;
        GenHornSkinShop();       
    }    
    
    public void GenHornSkinShop()
    {
        if (currentPopUpSkin == TypeSkinShop.HornSkin) return;
        currentPopUpSkin = TypeSkinShop.HornSkin;
        LoadPopUpWeapon(currentPopUpSkin);
    }
    public void GenShortsSkinShop()
    {
        if (currentPopUpSkin == TypeSkinShop.ShortsSkin) return;      
        currentPopUpSkin = TypeSkinShop.ShortsSkin;
        LoadPopUpWeapon(currentPopUpSkin);
    }
    public void GenArmShop()
    {
        if (currentPopUpSkin == TypeSkinShop.ArmSkin) return;
        currentPopUpSkin = TypeSkinShop.ArmSkin;
        LoadPopUpWeapon(currentPopUpSkin);
    }
    public void GenSkinShop()
    {
        if (currentPopUpSkin == TypeSkinShop.Skin) return;
        currentPopUpSkin = TypeSkinShop.Skin;
        LoadPopUpWeapon(currentPopUpSkin);
    }
    public void LoadPopUpWeapon(TypeSkinShop currentPopUpSkin)
    {       
        switch (currentPopUpSkin)
        {
            case TypeSkinShop.HornSkin:
                LoadDataPopUp(data_HornSkin, imageHornShop, currentHornOwner);
                break;
            case TypeSkinShop.ShortsSkin:
                LoadDataPopUp(data_ShortsSkin, imageShortsShop,currentShortsSkinOwner);
                break;
            case TypeSkinShop.ArmSkin:
                LoadDataPopUp(data_ArmSkin, imageArmShop,currentArmSkinOwner);
                break;
            case TypeSkinShop.Skin:
                LoadDataPopUp(data_Skin, imageSkinShop,currentSkinOwner);
                break;
            default:
                break;
        }     
    }
    public void CheckBought(bool isOwnered)
    {
        if (isOwnered)
        {
            bt_Buy.gameObject.SetActive(false);
            bt_BuyAds.gameObject.SetActive(false);

        }
        else
        {
            bt_Buy.gameObject.SetActive(true);
            bt_BuyAds.gameObject.SetActive(true);
        }
    }
    private void LoadDataPopUp(DataSkin dataSkin, Image imageSkin,List<int> skinOwner)
    {
        data_SkinCurrent = dataSkin;
        SelectingImageButton(imageSkin);
        bt_Buy.gameObject.SetActive(true);
        bt_BuyAds.gameObject.SetActive(true);
        bt_Buy.transform.SetParent(containButtonBuy.transform);
        bt_BuyAds.transform.SetParent(containButtonBuy.transform);
        ResetButton(dataSkin, skinOwner);
        containButtonCurrent[0].imageButton.color = Color.green;
        CheckBought(containButtonCurrent[0].isOwnered);
        IDataSkin[] idata = dataSkin.iDataSkin;
        LevelManager.GetInstance().player.GetComponent<ChangSkin>().ChangeSkin(idata[0].skin, idata[0].prefabWing, idata[0].prefabTail, idata[0].prefabHead, idata[0].prefabBow, idata[0].shorts);
        txt_Buy.text = dataSkin.iDataSkin[0].price.ToString();
    }
    public void SetDataButton(int _i, DataSkin _dataSkin, List<int> _skinOwner)
    {
        containButtonCurrent[_i].gameObject.SetActive(true);
        containButtonCurrent[_i].imageButton.color = Color.white;
        containButtonCurrent[_i].id = _dataSkin.iDataSkin[_i].idSkin;
        containButtonCurrent[_i].imageSkin.sprite = _dataSkin.iDataSkin[_i].spriteSkill;
        containButtonCurrent[_i].price = _dataSkin.iDataSkin[_i].price;
        for (int i = 0; i < _skinOwner.Count; i++)
        {
            if (containButtonCurrent[_i].id == _skinOwner[i])
            {
                containButtonCurrent[_i].isOwnered = true;
            }
        }
    }
    public void ResetButton(DataSkin _dataSkin, List<int> _skinOwner)
    {
        if (containButtonCurrent.Count <= _dataSkin.amountOfSkin)
        {
            for (int i = 0; i < containButtonCurrent.Count; i++)
            {
                SetDataButton(i, _dataSkin, _skinOwner);
            }
            for (int i = containButtonCurrent.Count; i < _dataSkin.amountOfSkin; i++)
            {
                ItemUIShop go = SpawnButtonUI(cointainItem);
                SetDataButton(i, _dataSkin, _skinOwner);
            }
        }
        else
        {
            for (int i = 0; i < _dataSkin.amountOfSkin; i++)
            {
                SetDataButton(i, _dataSkin, _skinOwner);
            }
            for (int i = _dataSkin.amountOfSkin; i < containButtonCurrent.Count; i++)
            {
                containButtonCurrent[i].gameObject.SetActive(false);
            }
        }
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
                containButtonCurrent[i].imageButton.color = Color.white;
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
        if(GameManager.GetInstance().dataPlayer.gold - priceCurrent < 0) return;
        GameManager.GetInstance().dataPlayer.gold -= priceCurrent;
        txt_Gold.text = GameManager.GetInstance().dataPlayer.gold.ToString();
        AddData(currentUsingSkin);
        GameManager.GetInstance().SaveData();
    }
    private void AddData(int skin)
    {
        switch (currentPopUpSkin)
        {
            case TypeSkinShop.HornSkin:
                GameManager.GetInstance().dataPlayer.hornorsOwner.Add(currentUsingSkin);
                break;
            case TypeSkinShop.ArmSkin:
                GameManager.GetInstance().dataPlayer.armOwner.Add(currentUsingSkin);
                break;
            case TypeSkinShop.ShortsSkin:
                GameManager.GetInstance().dataPlayer.shortsOwner.Add(currentUsingSkin);
                break;
            case TypeSkinShop.Skin:
                GameManager.GetInstance().dataPlayer.skinOwner.Add(currentUsingSkin);
                break;
            default:
                break;
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
}
