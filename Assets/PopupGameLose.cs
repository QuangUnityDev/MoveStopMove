using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupGameLose : PopupUI<PopupGameLose>
{
    [SerializeField] Button tapTapRetry;
    protected override void Awake()
    {
        base.Awake();
        tapTapRetry.onClick.AddListener(Retry);
    }
    private void Retry()
    {
        GameManager.GetInstance().GamePrepare();
        Close();
    }
}
