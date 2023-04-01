using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>,ISubcriber

{
    public Button bt_Play;
    public Button bt_OpenShopSkin;
    public Button bt_OpenShopWeapom;
    public GameObject popHome;
    public GameObject popRevive;
    public GameObject popGameOver;
    public GameObject popShopSkin;
    public GameObject popShopWeapon;
    public GameObject popUpCurrent;
    public CameraFollow cameraView;
    protected void Awake()
    {
        SetUp();
    }
    private void SetUp()
    {
        bt_Play.onClick.AddListener(PlayGame);
        bt_OpenShopSkin.onClick.AddListener(OpenShopSkin);
        bt_OpenShopWeapom.onClick.AddListener(OpenShopWeapon);
    }
    protected void Start()
    {
        GameManager.GetInstance().AddSubcriber(this);
    }
    public void ClosePopUp()
    {
        GameManager.GetInstance().deActivePlayer?.Invoke(true);
        popUpCurrent.SetActive(false);
        popHome.SetActive(true);
        cameraView.posPrepareGame();
    }

    public void OpenShopSkin()
    {
        cameraView.posOpenSkinShop();
        popUpCurrent = popShopSkin;
        popShopSkin.SetActive(true);
        OffPopUpHome();
    }
    public void OpenShopWeapon()
    {
        GameManager.GetInstance().deActivePlayer?.Invoke(false);
        popUpCurrent = popShopWeapon;
        popShopWeapon.SetActive(true);
        OffPopUpHome();
    }
    public void ShowPopUpRevive()
    {
        popUpCurrent = popRevive;
        popRevive.SetActive(true);
    }
    public void GameOver()
    {     
        popGameOver.SetActive(true);
    }
    public void OffPopUpHome()
    {
        popHome.SetActive(false);
    }
    public void PlayGame()
    {
        GameManager.GetInstance().GameStart();
    }
    public void GamePrepare()
    {
        popGameOver.SetActive(false);
        popHome.SetActive(true);
        popRevive.SetActive(false);
    }

    public void GameStart()
    {
        OffPopUpHome();
    }

    public void GameRevival()
    {
        popRevive.SetActive(true);
    }
    public void GamePause()
    {

    }

    public void GameResume()
    {

    }

    public void GameCompleted()
    {

    }
}
