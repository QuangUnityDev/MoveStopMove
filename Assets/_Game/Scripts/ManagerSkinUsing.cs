using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSkinUsing : Singleton<ManagerSkinUsing>
{
   [SerializeField]  DataSkin[] dataskin;
    private void Awake()
    {
    }
    public void ChangeSkinUsing()
    {
        IDataSkin idata = dataskin[GameManager.GetInstance().dataPlayer.currentSkin].iDataSkin[GameManager.GetInstance().dataPlayer.currentItemSkin];
        LevelManager.GetInstance().player.GetComponent<ChangSkin>().ChangeSkin(idata.skin, idata.prefabWing, idata.prefabTail, idata.prefabHead, idata.prefabBow, idata.shorts);
    }
}
