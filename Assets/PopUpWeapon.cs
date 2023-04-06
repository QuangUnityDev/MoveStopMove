using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpWeapon : Singleton<PopUpWeapon>
{
    [SerializeField] Button bt_NextWeapon;
    [SerializeField] Button bt_BackWeapon;
    [SerializeField] Button bt_Buy;
    [SerializeField] Button bt_Equip;
    [SerializeField] Button bt_UnlockAds;

    [SerializeField] DataWeapon dataWeaponAxe;
    [SerializeField] DataWeapon dataWeaponBoomerang;
    [SerializeField] DataWeapon dataWeaponCandyTree;

    [SerializeField] Text textConditionUnlock;
    [SerializeField] Text textHeadWeapon;
    [SerializeField] Text textCustom;
    [SerializeField] Text bt_TextEquip;
    //[SerializeField] Text textAtributeSkin;
    public Text textPrice;

    public Image skinCurrentImage;

    [SerializeField] GameObject popupSkinWeapon;

    [SerializeField] private ButtonWeaponSkin btSkinWeapon;
    [SerializeField] private GameObject ContainBtSkinWeapon;
    [SerializeField] public List<ButtonWeaponSkin> containButton;

    string textCondition;
   
    int[] currentWeaponOwner;

    int[] currentSkinSelecting;
    int[] currentAxeSkinOwner;
    int[] currentBoomerangSkinOwner;
    int[] currentCandyTreeSkinOwner;


    public int currentSelectingSkinWeapon;
    public int currentSelectingWeapon;
    public int priceCurrent;

    private Data dataSkinWeapon;

    private void OnEnable()
    {
        DestroyButtonLoadNewWeapon();
        SetUpData();
    }
    void SetUpData()
    {
        dataSkinWeapon = GameManager.GetInstance().dataPlayer;
        currentSelectingWeapon = dataSkinWeapon.currentWeapon;      
        currentWeaponOwner = dataSkinWeapon.weaponOwner;
        currentAxeSkinOwner = dataSkinWeapon.skinAxeOwer;
        currentSelectingSkinWeapon = dataSkinWeapon.currentUsingSkinWeapon;
        currentBoomerangSkinOwner = dataSkinWeapon.skinBoomerangOwer;
        currentCandyTreeSkinOwner = dataSkinWeapon.skinCandyTreeOwer;
        LoadPopUpWeapon(currentSelectingWeapon);
    }
    private void Awake()
    {
        bt_NextWeapon.onClick.AddListener(NextWeapon);
        bt_BackWeapon.onClick.AddListener(BackWeapon);
        bt_Buy.onClick.AddListener(Buy);
        bt_Equip.onClick.AddListener(Equip);    
    }
    private void Buy()
    {
       
    }
    public void Equip()
    {
        dataSkinWeapon.currentUsingSkinWeapon = currentSelectingSkinWeapon;
        dataSkinWeapon.currentWeapon = currentSelectingWeapon;
        gameObject.SetActive(false);      
        UIManager.GetInstance().ShowPopUpHome();
        callChange?.Invoke();
        callChange = null;
    }
    public void CheckBuy()
    {
        if (GameManager.GetInstance().dataPlayer.gold > priceCurrent)
        {
            bt_Buy.GetComponent<Image>().color = Color.green;
        }
        else
            bt_Buy.GetComponent<Image>().color = Color.gray;
    }
    public void LoadPopUpWeapon(int weaponUsing)
    {
        switch (weaponUsing)
        {
            case 0:
                currentSkinSelecting = currentAxeSkinOwner;
                LoadDataWeapon(dataWeaponAxe);
                break;
            case 1:
                currentSkinSelecting = currentBoomerangSkinOwner;
                LoadDataWeapon(dataWeaponBoomerang);
                break;
            case 2:
                currentSkinSelecting = currentCandyTreeSkinOwner;
                LoadDataWeapon(dataWeaponCandyTree);
                break;
            default:
                break;
        }
    }
   
    public string SetTextCondition()
    {
        switch (currentSelectingWeapon)
        {
            case 2:
                textCondition = dataWeaponBoomerang.nameWeapon;
                break;           
            default:
                break;
        }
        return textCondition;
    }
    public Action callChange;
    public void ChangSkinWeaponForPlayer(Action call)
    {
        callChange = call;
    }
    public void LoadDataWeapon(DataWeapon data)
    {
        ChangSkinWeaponForPlayer(() => LevelManager.GetInstance().player.ChangeSkinWeapon(data.iDataWeapon[1].materialSkin));     
        textHeadWeapon.text = data.nameWeapon;     
        textConditionUnlock.gameObject.SetActive(true);    
        textCustom.gameObject.SetActive(false);
        bt_Buy.gameObject.SetActive(true);
        bt_Equip.gameObject.SetActive(false);
        skinCurrentImage.sprite = data.iDataWeapon[0].spriteSkin;
        textPrice.text = data.price.ToString();
        if (currentSelectingWeapon == currentWeaponOwner.Length)
        {
            textConditionUnlock.text = "Lock";
        }
        else textConditionUnlock.text = "Unlock" + SetTextCondition() + "First";
        for (int i = 0; i < currentWeaponOwner.Length; i++)
        {
            if(data.idWeapon == currentWeaponOwner[i])
            {               
                textConditionUnlock.gameObject.SetActive(false);
                textCustom.gameObject.SetActive(true);              
                for (int j = 0; j < data.numberOfSkin; j++) //Spawn so luong nut theo data
                {
                   ButtonWeaponSkin bt = Instantiate(btSkinWeapon, ContainBtSkinWeapon.transform);
                    bt.gameObject.SetActive(true);
                   bt.idSkinWeapon = data.iDataWeapon[j].id;
                   bt.spriteButton = data.iDataWeapon[j].spriteSkin;
                   bt.imageButton.color = Color.white;
                    containButton.Add(bt);
                    if (j == GameManager.GetInstance().dataPlayer.currentUsingSkinWeapon)
                    {
                        containButton[j].imageButton.color = Color.red;
                        skinCurrentImage.sprite = data.iDataWeapon[j].spriteSkin;
                        bt_UnlockAds.gameObject.SetActive(false);
                        bt_Buy.gameObject.SetActive(false);
                        bt_Equip.gameObject.SetActive(true);
                        bt_TextEquip.text = "Select";
                        if (GameManager.GetInstance().dataPlayer.currentWeapon == currentSelectingWeapon)
                        {
                            bt_TextEquip.text = "Equiped";
                        }
                        else bt_TextEquip.text = "Select";
                    }                  
                }
                break;
            }
        }
    }
    public void CheckEquip()
    {
        if (currentSelectingWeapon == GameManager.GetInstance().dataPlayer.currentWeapon && currentSelectingSkinWeapon == GameManager.GetInstance().dataPlayer.currentUsingSkinWeapon)
        {
            bt_TextEquip.text = "Equiped";
        }
        else bt_TextEquip.text = "Select";
    }
    private void DestroyButtonLoadNewWeapon()
    {
        for (int j = 0; j < containButton.Count; j++)
        {
            Destroy(containButton[j].gameObject);          
        }
        containButton.Clear();
    }
    void NextWeapon()
    {           
        if (currentSelectingWeapon == 2) return;
        currentSelectingWeapon++;
        DestroyButtonLoadNewWeapon();
        LoadPopUpWeapon(currentSelectingWeapon);
    }
    void BackWeapon()
    {          
        if (currentSelectingWeapon == 0) return;
        currentSelectingWeapon--;
        DestroyButtonLoadNewWeapon();
        LoadPopUpWeapon(currentSelectingWeapon);
    }
    void BuySkinWeapon()
    {

    }
}
