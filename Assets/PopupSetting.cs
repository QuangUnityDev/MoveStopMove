using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSetting : PopupUI<PopupSetting>
{
    [SerializeField] private Button btn_Continue;
    [SerializeField] private Button btn_Home;
    protected override void Awake()
    {
        base.Awake();
        SetUp();
    }
    private void SetUp()
    {
        btn_Home.onClick.AddListener(OnCLickedButtonHome);
        btn_Continue.onClick.AddListener(OnClickedButtonContinue);
    }
    private void OnCLickedButtonHome()
    {
        GameManager.GetInstance().GamePrepare();
        Close();
    }
    private void OnClickedButtonContinue()
    {
        Close();
        UIManager.GetInstance().ShowPopUpInGame();
    }
}
