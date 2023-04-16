using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpInGame : PopupUI<PopUpInGame>,ISubcriber
{
    [SerializeField] private Text txtPlayerAlive;
    [SerializeField] private Button btn_Setting;
    protected override void Awake()
    {
        SetUp();
        SetUpTextNumberPlayerAlive(LevelManager.GetInstance().playerAlive) ;
    }
    private void Start()
    {
        GameManager.GetInstance().AddSubcriber(this);
    }
    private void SetUp()
    {
        btn_Setting.onClick.AddListener(OnClickedButtonSetting);
    }
    public void SetUpTextNumberPlayerAlive(int number)
    {
        txtPlayerAlive.text = "Alive: " + number.ToString();
    }
    protected void OnClickedButtonSetting()
    {
        ShowPopUp.ShowPopUps(StringNamePopup.PopupSetting);
        UIManager.GetInstance().NotShowPopUpInGame();
    }
    private void LateUpdate() //De tam thoi . Luc nao goi moi can // TOFIXME
    {
        SetUpTextNumberPlayerAlive(LevelManager.GetInstance().playerAlive);
    }

    public void GamePrepare()
    {
        //Close();
    }

    public void GameStart()
    {

    }

    public void GameRevival()
    {           
        
    }

    public void GamePause()
    {
        throw new System.NotImplementedException();
    }

    public void GameResume()
    {
        throw new System.NotImplementedException();
    }

    public void GameOver()
    {
        Close();
    }

    public void GameCompleted()
    {
        throw new System.NotImplementedException();
    }
}
