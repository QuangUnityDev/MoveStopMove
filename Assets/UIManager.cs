using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>,ISubcriber

{
    public CameraFollow cameraView;
    public PopUpInGame popUpInGame;
    public PopUpHome popUpHome;

    protected void Start()
    {
        GameManager.GetInstance().AddSubcriber(this);
    }
    public void CameraViewPrepare()
    {
        cameraView.posPrepareGame();
    }

    public void ShowPopUpHome(bool isShow)
    {
        if (isShow)
        {
            CameraViewPrepare();
            LevelManager.GetInstance().player.ChangeAnim(GlobalTag.playerAnimIdle);
            GameManager.GetInstance().deActivePlayer?.Invoke(true);
        }
       
        
        popUpHome.gameObject.SetActive(isShow);
      
    }
    public void OpenShopSkin()
    {
        cameraView.posOpenSkinShop();      
        LevelManager.GetInstance().player.ChangeAnim(GlobalTag.playerAnimDance);
    }
    public void OpenShopWeapon()
    {
        GameManager.GetInstance().deActivePlayer?.Invoke(false);     
    }
    public void GameOver()
    {
        NotShowPopUpInGame();
        ShowPopUp.ShowPopUps(StringNamePopup.PopupGameOver);
    }
    //public void ShowPopUpHome()
    //{
    //    CameraViewPrepare();
    //    LevelManager.GetInstance().player.ChangeAnim("Idle");
    //    GameManager.GetInstance().deActivePlayer?.Invoke(true);
    //    ShowPopUp.ShowPopUps(StringNamePopup.PopupHome);
    //}
    public void GamePrepare()
    {
      
    }
    public void ShowPopUpInGame()
    {
        popUpInGame.gameObject.SetActive(true);
    }
    public void NotShowPopUpInGame()
    {
        popUpInGame.gameObject.SetActive(false);
    }
    public void GameStart()
    {
        ShowPopUpInGame();
    }

    public void GameRevival()
    {
        ShowPopUp.ShowPopUps(StringNamePopup.PopupRevive);
        NotShowPopUpInGame();
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
