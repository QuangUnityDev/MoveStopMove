using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpWeapon : PopupUI<PopUpWeapon>
{
    [Header("Button")]
    [SerializeField] Button bt_NextWeapon;
    [SerializeField] Button bt_BackWeapon;
    [SerializeField] Button bt_Buy;
    [SerializeField] Button bt_Equip;
    [SerializeField] public Button bt_UnlockAds;
    [SerializeField] Button btn_Close;
    [Header("Data")]
    [SerializeField] DataWeapon dataWeaponAxe;
    [SerializeField] DataWeapon dataWeaponBoomerang;
    [SerializeField] DataWeapon dataWeaponCandyTree;

    [SerializeField] DataWeapon dataWeaponCurrent;

    [Header("Text")]
    [SerializeField] Text textConditionUnlock;
    [SerializeField] Text textHeadWeapon;
    [SerializeField] Text textCustom;
    [SerializeField] Text bt_TextEquip;

    public Text textPrice;

    [SerializeField] private GameObject skinWeaponAxe;
    [SerializeField] private GameObject skinWeaponBoomerang;
    [SerializeField] private GameObject skinWeaponCandy;

    [HideInInspector] public GameObject skinWeaponCurrent;

    [SerializeField] private ButtonWeaponSkin btSkinWeapon;
    [SerializeField] private GameObject ContainBtSkinWeapon;
    public List<ButtonWeaponSkin> containButton;

    string textCondition;
   
    List<int> currentWeaponOwner;

    List<int> currentAxeSkinOwner;
    List<int> currentBoomerangSkinOwner;
    List<int> currentCandyTreeSkinOwner;

    [HideInInspector] public List<int> currentSkinSelecting;

    public int equipedSkinWeapon;
    public TypeWeaapon currentWeponEquiped;

    [HideInInspector] public int priceCurrent;

    private Data data;

    private void SetUpData()
    {
        skinWeaponAxe = GameObject.Find(StringNamePopup.WeaponAxe);
        skinWeaponBoomerang = GameObject.Find(StringNamePopup.WeaponBoomerang);
        skinWeaponCandy = GameObject.Find(StringNamePopup.WeaponCandy);

        data = GameManager.GetInstance().dataPlayer;

        currentWeponEquiped = data.equipedWeapon;                 
        equipedSkinWeapon = data.equipedSkinWeapon;


        currentWeaponOwner = data.weaponOwner;

        currentAxeSkinOwner = data.skinAxeOwer;
        currentBoomerangSkinOwner = data.skinBoomerangOwer;
        currentCandyTreeSkinOwner = data.skinCandyTreeOwer;

        LoadPopUpWeapon(currentWeponEquiped); //Khoi tao khi mo popup
    }
    protected override void Awake()
    {
        base.Awake();
        SetUpAddListener();          
    }
    protected override void WillShowContent()
    {
        base.WillShowContent();
        UIManager.GetInstance().OpenShopWeapon();
    }
    private void SetUpAddListener()
    {
        bt_NextWeapon.onClick.AddListener(NextWeapon);
        bt_BackWeapon.onClick.AddListener(BackWeapon);
        bt_Buy.onClick.AddListener(OnClickedButtonBuy);
        bt_Equip.onClick.AddListener(Equip);
        bt_UnlockAds.onClick.AddListener(UnLockSkinWeapon);
        btn_Close.onClick.AddListener(OnClickedButtonClose);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        DestroyButtonLoadNewWeapon();
        SetUpData();
    }
    private void OnClickedButtonClose()
    {
        Close();
        skinWeaponCandy.gameObject.SetActive(true);
        skinWeaponAxe.gameObject.SetActive(true);
        skinWeaponBoomerang.gameObject.SetActive(true);
        UIManager.GetInstance().ShowPopUpHome();
    }
    private void WeaponOwnered()
    {
        bt_Buy.gameObject.SetActive(false);
        bt_UnlockAds.gameObject.SetActive(false);
        bt_Equip.gameObject.SetActive(true);
    }
    private void WeaponNotOwnered()
    {
        bt_Buy.gameObject.SetActive(true);
        bt_UnlockAds.gameObject.SetActive(false);
        bt_Equip.gameObject.SetActive(false);

        textConditionUnlock.gameObject.SetActive(true);
        textCustom.gameObject.SetActive(false);
    }
    private void ItemSkinOwnered()
    {
        bt_Buy.gameObject.SetActive(false);
        bt_UnlockAds.gameObject.SetActive(false);
        bt_Equip.gameObject.SetActive(true);
        
    }
    //Khi Nhan Mua Weapon
    private void OnClickedButtonBuy()
    {
       if(GameManager.GetInstance().dataPlayer.gold - priceCurrent < 0) return;

        GameManager.GetInstance().dataPlayer.weaponOwner.Add((int)currentWeponEquiped);
      

        GameManager.GetInstance().SaveData();

        LoadPopUpWeapon(currentWeponEquiped);

        WeaponOwnered();
    }
    public void Equip()
    {
        data.equipedSkinWeapon = equipedSkinWeapon;
        data.equipedWeapon = currentWeponEquiped;

        GameManager.GetInstance().dataPlayer.equipedSkinWeapon = equipedSkinWeapon;
        data.equipedWeapon = currentWeponEquiped;
       
        OnClickedButtonClose();        
        GameManager.GetInstance().SaveData();
        ManagerWeapon.GetInstance().ChangeSkinWeapon(LevelManager.GetInstance().player.currentWeaponEquiped);
    }
    void UnLockSkinWeapon()
    {
        bt_UnlockAds.gameObject.SetActive(false);
        currentSkinSelecting.Add(equipedSkinWeapon);
        containButton[equipedSkinWeapon].imageLock.gameObject.SetActive(false);
        GameManager.GetInstance().SaveData();
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
    public void LoadPopUpWeapon(TypeWeaapon weaponUsing)
    {
        switch (weaponUsing)
        {
            case TypeWeaapon.AXE:
                currentSkinSelecting = currentAxeSkinOwner;
                LoadDataWeapon(dataWeaponAxe, skinWeaponAxe);
                break;
            case TypeWeaapon.BOOMERANG:
                currentSkinSelecting = currentBoomerangSkinOwner;
                textCondition = dataWeaponAxe.nameWeapon;
                LoadDataWeapon(dataWeaponBoomerang, skinWeaponBoomerang);
                break;
            case TypeWeaapon.CANDYTREE:
                currentSkinSelecting = currentCandyTreeSkinOwner;
                textCondition = dataWeaponBoomerang.nameWeapon;
                LoadDataWeapon(dataWeaponCandyTree, skinWeaponCandy);
                break;
            default:
                break;
        }
    }
    private void CheckTextCondition()
    {
        if ((int)currentWeponEquiped == currentWeaponOwner.Count)
        {
            textConditionUnlock.text = "Lock";
        }
        else textConditionUnlock.text = "Unlock" + textCondition + "First";
    }
    private void OffHideWeaponGo()
    {
        skinWeaponCandy.gameObject.SetActive(false);
        skinWeaponAxe.gameObject.SetActive(false);
        skinWeaponBoomerang.gameObject.SetActive(false);
    }
    public void LoadDataWeapon(DataWeapon data,GameObject weapon)
    {
        dataWeaponCurrent = data;
        OffHideWeaponGo();
        weapon.gameObject.SetActive(true);
        skinWeaponCurrent = weapon;        
        textHeadWeapon.text = data.nameWeapon;     

        WeaponNotOwnered();

        textPrice.text = data.price.ToString();

        //Check Condition Open Weapon
        CheckTextCondition();
        for (int i = 0; i < currentWeaponOwner.Count; i++)
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
                    for (int k = 0; k < currentSkinSelecting.Count; k++)
                    {
                        if(bt.idSkinWeapon == currentSkinSelecting[k])
                        {
                            bt.imageLock.gameObject.SetActive(false);
                            bt.isUnLock = true;
                        }
                    }
                   containButton.Add(bt);
                    if (j == GameManager.GetInstance().dataPlayer.equipedSkinWeapon)
                    {
                        containButton[j].imageButton.color = Color.red;
                        ItemSkinOwnered();
                        if (GameManager.GetInstance().dataPlayer.equipedWeapon == currentWeponEquiped)
                        {
                            bt_TextEquip.text = "Equiped";
                            bt.OnClickButton();
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
        if (currentWeponEquiped == GameManager.GetInstance().dataPlayer.equipedWeapon && equipedSkinWeapon == GameManager.GetInstance().dataPlayer.equipedSkinWeapon)
        {
            bt_TextEquip.text = "Equiped";
        }
        else
            bt_TextEquip.text = "Select";       
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
        if ((int)currentWeponEquiped == 2) return;
        currentWeponEquiped++;
        DestroyButtonLoadNewWeapon();
        LoadPopUpWeapon(currentWeponEquiped);
    }
    void BackWeapon()
    {          
        if (currentWeponEquiped == 0) return;
        currentWeponEquiped--;
        DestroyButtonLoadNewWeapon();
        LoadPopUpWeapon(currentWeponEquiped);
    }
}
