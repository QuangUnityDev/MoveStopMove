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

    [SerializeField] private List<ButtonShop> btSkinShop;
    [SerializeField] private List<ButtonShop> btSkinShopInHornSkin;
    [SerializeField] private List<ButtonShop> btSkinShopInShortsSkin;
    [SerializeField] private List<ButtonShop> btSkinShopInArmSkin;

    public List<ButtonShop> containButtonCurrent;


    [SerializeField] private DataSkin data_HornSkin;
    [SerializeField] private DataSkin data_ShortsSkin;
    [SerializeField] private DataSkin data_ArmSkin;
    [SerializeField] private DataSkin data_Skin;

    [SerializeField] private GameObject cointainHornSkin;
    [SerializeField] private GameObject cointainShortsSkin;
    [SerializeField] private GameObject cointainArmSkin;
    [SerializeField] private GameObject cointainSkin;
    [SerializeField] private bool isCreateNew = true;


    [SerializeField] private TypeSkinShop typeSkinShopCurrent;


    int[] currentSkinSelecting;

    int[] currentHornOwner;
    int[] currentShortsSkinOwner;
    int[] currentArmSkinOwner;
    int[] currentSkinOwner;


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
    }
    private void OnEnable()
    {
        GenHornSkinShop();
        currentPopUpSkin = 0;
    }
    void ClearAllContain()
    {
        btSkinShopInHornSkin.Clear();
        btSkinShopInShortsSkin.Clear();
    }
    public void ClearButton()
    {
        ClearAllContain();
        for (int i = 0; i < containButtonCurrent.Count; i++)
        {
            containButtonCurrent[i].gameObject.SetActive(false);

        }
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
                LoadDataPopUp(data_HornSkin, btSkinShopInHornSkin,imageHornShop);
                typeSkinShopCurrent = TypeSkinShop.HornSkin;
                break;
            case 1:

                LoadDataPopUp(data_ShortsSkin, btSkinShopInShortsSkin,imageShortsShop);
                typeSkinShopCurrent = TypeSkinShop.ShortsSkin;
                break;
            case 2:
                LoadDataPopUp(data_ArmSkin, btSkinShopInArmSkin, imageArmShop);
                typeSkinShopCurrent = TypeSkinShop.ArmSkin;
                break;
            case 3:

                LoadDataPopUp(data_Skin, btSkinShop , imageSkinShop);
                typeSkinShopCurrent = TypeSkinShop.Skin;
                break;
            default:
                break;
        }       
    }
    private void LoadDataPopUp(DataSkin dataSkin, List<ButtonShop> cointain, Image imageSkin)
    {        
        ClearButton();     
        SelectingImageButton(imageSkin);
        bt_Buy.gameObject.SetActive(true);
        bt_BuyAds.gameObject.SetActive(true);
        bt_Buy.transform.SetParent(containButtonBuy.transform);
        bt_BuyAds.transform.SetParent(containButtonBuy.transform);
        for (int i = 0; i < dataSkin.amountOfSkin; i++)
        {
            ButtonShop go = SpawnButtonUI(cointainArmSkin);
            go.id = dataSkin.iDataSkin[i].idSkin;
            go.imageSkin.sprite = dataSkin.iDataSkin[i].spriteSkill;
            go.price = dataSkin.iDataSkin[i].price;
            cointain.Add(go);
            containButtonCurrent.Add(go);
        }
        cointain[0].imageButton.color = Color.green;
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
    public enum TypeSkinShop
    {
        None,
        HornSkin,
        ShortsSkin,
        ArmSkin,
        Skin,

    }
}
