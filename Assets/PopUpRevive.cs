using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpRevive : MonoBehaviour
{
    [SerializeField] private Text txtCountDown;
    private bool isCount;
    [SerializeField] private float timeCountDown;
    [SerializeField] private Button btReviveAds;
    private bool isBackGamePrepare;
    private void Awake()
    {
        btReviveAds.onClick.AddListener(Revive);
    }
    private void OnEnable()
    {
        isCount = true;
        timeCountDown = 5;
        isBackGamePrepare = true;
    }
    public void Revive()
    {
        LevelManager.GetInstance().ResetPos();
        gameObject.SetActive(false);
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
            transform.gameObject.SetActive(false);
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
