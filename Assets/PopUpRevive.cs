using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpRevive : PopupUI<PopUpRevive>
{
    [SerializeField] private Text txtCountDown;
    private bool isCount;
    [SerializeField] private float timeCountDown;
    [SerializeField] private Button btReviveAds;
    [SerializeField] private Button btn_Close;
    private bool isBackGamePrepare;
    protected override void Awake()
    {
        SetUp();
    }
    private void OnClickedButtonClose()
    {
        Close();
        GameManager.GetInstance().GameOver();
    }
    private void SetUp()
    {
        btReviveAds.onClick.AddListener(Revive);
        btn_Close.onClick.AddListener(OnClickedButtonClose);
        isCount = true;
        timeCountDown = 5;
        isBackGamePrepare = true;
    }
    public void Revive()
    {
        LevelManager.GetInstance().ResetPos();
        Close();
    }
    void Update()
    {
        if(isCount && timeCountDown > 0)
        {
            StartCoroutine(CountDown());
        }   
        if(timeCountDown == 0 && isBackGamePrepare)
        {
            GameManager.GetInstance().GameOver();
            isBackGamePrepare = false;
            OnClickedButtonClose();
        }
    }
    IEnumerator CountDown()
    {
        isCount = false;
        txtCountDown.text = timeCountDown.ToString();
        yield return new WaitForSeconds(1);
        timeCountDown--;
        txtCountDown.text = timeCountDown.ToString();
        if (timeCountDown <= 0) timeCountDown = 0;
        isCount = true;

    }
}
