using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPlayer : MonoBehaviour
{
    [SerializeField] private RectTransform go;
    [SerializeField] private Text textLevelPlayer;
    [SerializeField] private Text textNamePlayer;
    void Update()
    {
        go.rotation = Quaternion.Euler(45, Quaternion.identity.y, Quaternion.identity.z);
    }
    public void SetTextLevel(int killed)
    {
        textLevelPlayer.text = killed.ToString();
    }
    public void SetTextNamePlayer(int playerInt)
    {
        textNamePlayer.text = "player " + playerInt.ToString();
        if(playerInt == 0)
        {
            textNamePlayer.text = "You";
        }
    }
    public void OffInfoPlayer()
    {
        go.gameObject.SetActive(false);
    }
    public void OnInfoPlayer()
    {
        go.gameObject.SetActive(true);
    }
}
