using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : Singleton<GamePanel>,ISubcriber
{  
    public Button bt_Play;
    public GameObject popHome;
    protected void Awake()
    {
        bt_Play.onClick.AddListener(PlayGame);
    }
    protected void Start()
    {
        GameManager.GetInstance().AddSubcriber(this);
    }
    public void OffPopUP()
    {
        popHome.gameObject.SetActive(false);
    }
    public void PlayGame()
    {       
        GameManager.GetInstance().GameStart();    
    }

    public void GamePrepare()
    {
      
    }

    public void GameStart()
    {
        OffPopUP();
    }

    public void GameRevival()
    { 
    }

    public void GamePause()
    {
       
    }

    public void GameResume()
    {
      
    }

    public void GameOver()
    {
       
    }

    public void GameCompleted()
    {
       
    }
}
