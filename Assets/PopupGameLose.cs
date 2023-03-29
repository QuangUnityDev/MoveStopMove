using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupGameLose : MonoBehaviour
{
    [SerializeField] Button tapTapRetry;
    private void Awake()
    {
        tapTapRetry.onClick.AddListener(Retry);
    }

    void Update()
    {
        
    }
    void Retry()
    {
        GameManager.GetInstance().GamePrepare();
    }
}
