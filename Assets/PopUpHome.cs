using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpHome : PopupUI<PopUpHome>
{
    public Button bt_Play;
    public Button bt_OpenShopSkin;
    public Button bt_OpenShopWeapom;
    public Text txt_Gold;
    protected override void Awake()
    {
        base.Awake();
        SetUp();
    }

    private void SetUp()
    {
        bt_Play.onClick.AddListener(OnClickedButtonPlayGame);
        bt_OpenShopSkin.onClick.AddListener(OnClickedButtonShopSkin);
        bt_OpenShopWeapom.onClick.AddListener(OnClickedButtonShopWeapon);
        txt_Gold.text = GameManager.GetInstance().dataPlayer.gold.ToString();
    }
    private void OnClickedButtonPlayGame()
    {
        Close();
        GameManager.GetInstance().GameStart();
    }
    private void OnClickedButtonShopWeapon()
    {
        ShowPopUp.ShowPopUps(StringNamePopup.PopupShopWeapon);
        Close();
    }
    private void OnClickedButtonShopSkin()
    {
        ShowPopUp.ShowPopUps(StringNamePopup.PopupShopSkin);
        Close();
    }
}
