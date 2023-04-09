using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSkin : Singleton<PopUpSkin>
{
    [SerializeField] private Button bt_HornSkin;
    [SerializeField] private Button bt_ShortsSkin;
    [SerializeField] private Button bt_ArmSkin;
    [SerializeField] private Button bt_SkinShop;

    [SerializeField] private GameObject containButtonBuy;
    [SerializeField] private Button bt_Buy;
    [SerializeField] private Button bt_BuyAds;

    public Text txt_Buy;

    [SerializeField] private Image imageSkinShop;
    [SerializeField] private Image imageArmShop;
    [SerializeField] private Image imageHornShop;
    [SerializeField] private Image imageShortsShop;
    private Image imageCurrent;

    public List<ButtonShop> containButtonCurrent;


    [SerializeField] private DataSkin data_HornSkin;
    [SerializeField] private DataSkin data_ShortsSkin;
    [SerializeField] private DataSkin data_ArmSkin;
    [SerializeField] private DataSkin data_Skin;

    public DataSkin data_SkinCurrent;

    [SerializeField] private GameObject cointainItem;

    [SerializeField] private bool isCreateNew = true;


    [SerializeField] private TypeSkinShop typeSkinShopCurrent;


    List<int> currentSkinSelecting;

    List<int> currentHornOwner;
    List<int> currentShortsSkinOwner;
    List<int> currentArmSkinOwner;
    List<int> currentSkinOwner;


    public int currentUsingSkin;
    public int priceCurrent;

    private int currentPopUpSkin;
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
    private void Awake()
    {
        bt_HornSkin.onClick.AddListener(GenHornSkinShop);
        bt_ShortsSkin.onClick.AddListener(GenShortsSkinShop);
        bt_SkinShop.onClick.AddListener(GenSkinShop);
        bt_ArmSkin.onClick.AddListener(GenArmShop);
        bt_Buy.onClick.AddListener(OnClickedBuy);
    }
    private void OnEnable()
    {
        GenHornSkinShop();
        currentPopUpSkin = 0;
    }
    public void ResetButton(DataSkin _dataSkin)
    {
        if (containButtonCurrent.Count <= _dataSkin.amountOfSkin)
        {
            for (int i = 0; i < containButtonCurrent.Count; i++)
            {
                SetDatButton(i, _dataSkin);
            }
            for (int i = containButtonCurrent.Count ; i < _dataSkin.amountOfSkin; i++)
            {
                ButtonShop go = SpawnButtonUI(cointainItem);
                go.id = _dataSkin.iDataSkin[i].idSkin;
                go.imageSkin.sprite = _dataSkin.iDataSkin[i].spriteSkill;
                go.price = _dataSkin.iDataSkin[i].price;             
            }
        }
        else
        {
            for (int i = 0; i < _dataSkin.amountOfSkin; i++)
            {
                SetDatButton(i, _dataSkin);
            }
            for (int i = _dataSkin.amountOfSkin; i < containButtonCurrent.Count; i++)
            {
                containButtonCurrent[i].gameObject.SetActive(false);
            }
        }
        cointainItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(241.2885f, 0);
    }
    public void GetButtonBuy()
    {

    }
    public void SetDatButton(int _i , DataSkin _dataSkin)
    {
        containButtonCurrent[_i].gameObject.SetActive(true);
        containButtonCurrent[_i].imageButton.color = Color.white;
        containButtonCurrent[_i].id = _dataSkin.iDataSkin[_i].idSkin;
        containButtonCurrent[_i].imageSkin.sprite = _dataSkin.iDataSkin[_i].spriteSkill;
        containButtonCurrent[_i].price = _dataSkin.iDataSkin[_i].price;
    }
    public void GenHornSkinShop()
    {
        if (typeSkinShopCurrent == TypeSkinShop.HornSkin) return;
        currentPopUpSkin = 0;
        LoadPopUpWeapon(currentPopUpSkin);
    }
    public void GenShortsSkinShop()
    {
        if (typeSkinShopCurrent == TypeSkinShop.ShortsSkin) return;      
        currentPopUpSkin = 1;
        LoadPopUpWeapon(currentPopUpSkin);
    }
    public void GenArmShop()
    {
        if (typeSkinShopCurrent == TypeSkinShop.ArmSkin) return;
        currentPopUpSkin = 2;
        LoadPopUpWeapon(currentPopUpSkin);
    }
    public void GenSkinShop()
    {
        if (typeSkinShopCurrent == TypeSkinShop.Skin) return;
        currentPopUpSkin = 3;
        LoadPopUpWeapon(currentPopUpSkin);
    }
    public void LoadPopUpWeapon(int currentPopUpSkin)
    {       
        switch (currentPopUpSkin)
        {
            case 0:
                data_SkinCurrent = data_HornSkin;
                LoadDataPopUp(data_HornSkin, imageHornShop);
                typeSkinShopCurrent = TypeSkinShop.HornSkin;
                break;
            case 1:

                LoadDataPopUp(data_ShortsSkin, imageShortsShop);
                typeSkinShopCurrent = TypeSkinShop.ShortsSkin;
                data_SkinCurrent = data_ShortsSkin;
                break;
            case 2:
                LoadDataPopUp(data_ArmSkin, imageArmShop);
                typeSkinShopCurrent = TypeSkinShop.ArmSkin;
                data_SkinCurrent = data_ArmSkin;
                break;
            case 3:

                LoadDataPopUp(data_Skin, imageSkinShop);
                typeSkinShopCurrent = TypeSkinShop.Skin;
                data_SkinCurrent = data_Skin;
                break;
            default:
                break;
        }     
    }
    private void LoadDataPopUp(DataSkin dataSkin, Image imageSkin)
    {
        SelectingImageButton(imageSkin);
        bt_Buy.gameObject.SetActive(true);
        bt_BuyAds.gameObject.SetActive(true);
        bt_Buy.transform.SetParent(containButtonBuy.transform);
        bt_BuyAds.transform.SetParent(containButtonBuy.transform);
        ResetButton(dataSkin);
        containButtonCurrent[0].imageButton.color = Color.green;
        IDataSkin[] idata = dataSkin.iDataSkin;
        LevelManager.GetInstance().player.GetComponent<ChangSkin>().ChangeSkin(idata[0].skin, idata[0].prefabWing, idata[0].prefabTail, idata[0].prefabHead, idata[0].prefabBow, idata[0].shorts);
        txt_Buy.text = dataSkin.iDataSkin[0].price.ToString();
    }

    public ButtonShop SpawnButtonUI(GameObject contain)
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
                ButtonShop more = Instantiate(containButtonCurrent[0], contain.transform);
                more.gameObject.SetActive(true);
                containButtonCurrent.Add(more);
                return more;
            }
        }
        return null;

    }
    public void OnClickedBuy()
    {
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
