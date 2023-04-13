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

    /*[HideInInspector]*/ public int currentSelectingSkinWeapon;
    [HideInInspector] public TypeWeaapon currentSelectingWeapon;
    [HideInInspector] public int priceCurrent;

    private Data dataSkinWeapon;
    private void SetUpData()
    {
        skinWeaponAxe = GameObject.Find(StringNamePopup.WeaponAxe);
        skinWeaponBoomerang = GameObject.Find(StringNamePopup.WeaponBoomerang);
        skinWeaponCandy = GameObject.Find(StringNamePopup.WeaponCandy);
        dataSkinWeapon = GameManager.GetInstance().dataPlayer;

        currentSelectingWeapon = dataSkinWeapon.currentWeapon;    
        
        currentWeaponOwner = dataSkinWeapon.weaponOwner;

        currentSelectingSkinWeapon = dataSkinWeapon.currentUsingSkinWeapon;

        currentAxeSkinOwner = dataSkinWeapon.skinAxeOwer;
        currentBoomerangSkinOwner = dataSkinWeapon.skinBoomerangOwer;
        currentCandyTreeSkinOwner = dataSkinWeapon.skinCandyTreeOwer;

        LoadPopUpWeapon(currentSelectingWeapon); //Khoi tao khi mo popup
    }
    protected override void Awake()
    {
        base.Awake();
        SetUpAddListener();          
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
        ShowPopUp.ShowPopUps(StringNamePopup.PopupHome);
    }
    private void OnClickedButtonBuy()
    {
        GameManager.GetInstance().dataPlayer.weaponOwner.Add(currentSelectingSkinWeapon);        
        GameManager.GetInstance().SaveData();
    }
    public void Equip()
    {
        dataSkinWeapon.currentUsingSkinWeapon = currentSelectingSkinWeapon;
        dataSkinWeapon.currentWeapon = currentSelectingWeapon;
        GameManager.GetInstance().dataPlayer.currentUsingSkinWeapon = currentSelectingSkinWeapon;
        LevelManager.GetInstance().player.ChangeSkinWeapon(dataWeaponCurrent.iDataWeapon[currentSelectingSkinWeapon].materialSkin);
        OnClickedButtonClose();
        GameManager.GetInstance().SaveData();
    }
    void UnLockSkinWeapon()
    {
        bt_UnlockAds.gameObject.SetActive(false);
        currentSkinSelecting.Add(currentSelectingSkinWeapon); 
        containButton[currentSelectingSkinWeapon].isUnLock = true;
        containButton[currentSelectingSkinWeapon].imageLock.gameObject.SetActive(false);
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
    public void LoadDataWeapon(DataWeapon data,GameObject weapon)
    {
        dataWeaponCurrent = data;
       
        
       
        skinWeaponCandy.gameObject.SetActive(false);
        skinWeaponAxe.gameObject.SetActive(false);
        skinWeaponBoomerang.gameObject.SetActive(false);

        weapon.gameObject.SetActive(true);
        skinWeaponCurrent = weapon;   
        
        textHeadWeapon.text = data.nameWeapon;     
        textConditionUnlock.gameObject.SetActive(true);
        textCustom.gameObject.SetActive(false);

        bt_Buy.gameObject.SetActive(true);
        bt_Equip.gameObject.SetActive(false);

        textPrice.text = data.price.ToString();

        //Check Condition Open Weapon
        if ((int)currentSelectingWeapon == currentWeaponOwner.Count)
        {
            textConditionUnlock.text = "Lock";
        }
        else textConditionUnlock.text = "Unlock" + textCondition + "First";
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
                    if (j == GameManager.GetInstance().dataPlayer.currentUsingSkinWeapon)
                    {
                        containButton[j].imageButton.color = Color.red;
                        bt_UnlockAds.gameObject.SetActive(false);
                        bt_Buy.gameObject.SetActive(false);
                        bt_Equip.gameObject.SetActive(true);
                        bt.isUnLock = true;
                        bt_TextEquip.text = "Select";
                        if (GameManager.GetInstance().dataPlayer.currentWeapon == currentSelectingWeapon)
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
        if (currentSelectingWeapon == GameManager.GetInstance().dataPlayer.currentWeapon && currentSelectingSkinWeapon == GameManager.GetInstance().dataPlayer.currentUsingSkinWeapon)
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
        if ((int)currentSelectingWeapon == 2) return;
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
}
