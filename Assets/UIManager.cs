using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>,ISubcriber

{
    public CameraFollow cameraView;
   
    protected void Start()
    {
        GameManager.GetInstance().AddSubcriber(this);
    }
    public void CameraViewPrepare()
    {
        cameraView.posPrepareGame();
    }

    public void OpenShopSkin()
    {
        cameraView.posOpenSkinShop();      
        LevelManager.GetInstance().player.ChangeAnim("Dance");
    }
    public void OpenShopWeapon()
    {
        GameManager.GetInstance().deActivePlayer?.Invoke(false);     
    }
    public void GameOver()
    {
        ShowPopUp.ShowPopUps(StringNamePopup.PopupGameOver);
    }
    public void ShowPopUpHome()
    {      
        LevelManager.GetInstance().player.ChangeAnim("Idle");
        GameManager.GetInstance().deActivePlayer?.Invoke(true);
        ShowPopUp.ShowPopUps(StringNamePopup.PopupHome);
    }
    public void GamePrepare()
    {
        ShowPopUp.ShowPopUps(StringNamePopup.PopupHome);
    }

    public void GameStart()
    {
        ShowPopUp.ShowPopUps(StringNamePopup.PopupInGame);
    }

    public void GameRevival()
    {
        ShowPopUp.ShowPopUps(StringNamePopup.PopupRevive);
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
