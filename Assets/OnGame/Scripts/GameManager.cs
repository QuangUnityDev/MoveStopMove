using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<GameObject> listTarget;
    public int numberPlayerEnemy;
    public static int id = 0;
    private bool isSpawned;
    [SerializeField] public PlayerController player;
    [SerializeField] private Data data;
    public int levelCurrent;
    public int gold;

    private void Awake()
    {
        gameSubcribers = new ArrayList();
        levelCurrent = 1;
        SaveLoadData.GetInstance().SaveToFile();
        GameManager.id = 0;
    }
    public void SaveData()
    {
        data.currentWeapon = (int)GameManager.GetInstance().player.typeWeaapon;
        data.gold = GameManager.GetInstance().gold;
        data.levelID = GameManager.GetInstance().levelCurrent;
        SaveLoadData.GetInstance().SaveToFile();
    }
    public void GetData()
    {
        SaveLoadData.GetInstance().LoadFromFile();
        data.currentWeapon = ChangeEquiment.GetInstance().currentWeapon;
        data.gold = gold;
        data.levelID = levelCurrent;
    }
    private void Start()
    {
        isLoaded = false;
        for (int i = 0; i < numberPlayerEnemy; i++)
        {
            BotController more = ObjectsPooling.GetInstance().SpawnPlayerEnemy(SpawnRandom(), transform);
            listTarget.Add(more.gameObject);
        }
    }
    void Update()
    {
        if (listTarget.Count < numberPlayerEnemy)
        {
            BotController more = ObjectsPooling.GetInstance().SpawnPlayerEnemy(SpawnRandom(), transform);
            listTarget.Add(more.gameObject);
        }
    }
    IEnumerator WaitSpawn()
    {
        isSpawned = false;
        yield return new WaitForSeconds(3);
        BotController more = ObjectsPooling.GetInstance().SpawnPlayerEnemy(SpawnRandom(), transform);
        listTarget.Add(more.gameObject);
        isSpawned = false;
    }
    public Vector3 SpawnRandom()
    {
        Vector3 posRandom = new Vector3(Random.Range(-28, 28), -0.55f, Random.Range(-28, 28));
        for (int i = 0; i < listTarget.Count ; i++)
        {
            if(Vector3.Distance(posRandom,listTarget[i].transform.position) < 5)
            {
                posRandom = new Vector3(Random.Range(-28, 28), -0.55f, Random.Range(-28, 28));
            }
        }
        return posRandom;
    }
    public void GetKill(int idBullet)
    {
        if (idBullet == listTarget[0].GetComponent<PlayerController>().id)
        {
            PlayerController go = listTarget[0].GetComponent<PlayerController>();
            go.killed++;
            go.transform.localScale = new Vector3(1 + go.killed * 0.2f, 1 + go.killed * 0.2f, 1 + go.killed * 0.2f);
        }
        else
        {
            for (int i = 1; i < listTarget.Count; i++)
            {
                if(listTarget[i].GetComponent<BotController>().id == idBullet)
                {
                    BotController go = listTarget[i].GetComponent<BotController>();
                    go.killed++;
                    go.transform.localScale = new Vector3(1 + go.killed * 0.2f, 1 + go.killed * 0.2f, 1 + go.killed * 0.2f);

                }
            }          
        }
    }
    #region Game Subcribers
    public ArrayList gameSubcribers;
    public delegate void EventCall(IGameSubscriber s);
    public void AddSubcriber(IGameSubscriber s)
    {
        gameSubcribers.Add(s);
    }
    public void RemoveSubcriber(IGameSubscriber s)
    {
        gameSubcribers.Remove(s);
    }
    public void CallEvent(EventCall call)
    {
        foreach (IGameSubscriber s in gameSubcribers)
            call(s);
    }

    public void GamePrepare()
    {
        //  Debug.LogError("GamePrepare");
        //CurrencyOnGame = 0;
        //TopUI.Instance.SetCurrencyInGame(CurrencyOnGame);      
        game_State = GAME_STATE.GAME_PREPARE;
        StartCoroutine(IEGamePrepare());
        isLoaded = true;
        // }
    }

    public void GameStart()
    {     
        game_State = GAME_STATE.GAME_PLAY;
        CallEvent((s) =>
        {
            s.GameStart();
        });
    }
    public void GameRevival()
    {
        if (game_State != GAME_STATE.GAME_REVIVAL)
        {
            game_State = GAME_STATE.GAME_REVIVAL;
            CallEvent((s) => { s.GameRevival(); });
        }

    }
    public void GamePause()
    {
        if (game_State != GAME_STATE.GAME_PAUSE)
        {
            game_State = GAME_STATE.GAME_PAUSE;
            CallEvent((s) => { s.GamePause(); });
            Time.timeScale = 0;
        }
    }
    public void GameResume()
    {
        if (game_State != GAME_STATE.GAME_PLAY)
        {
            game_State = GAME_STATE.GAME_PLAY;
            CallEvent((s) => { s.GameResume(); });
            Time.timeScale = 1;
        }

    }
    public void GameOver()
    {
        if (game_State != GAME_STATE.GAME_OVER && game_State != GAME_STATE.GAME_COMPLETE)
        {
            game_State = GAME_STATE.GAME_OVER;
            CallEvent((s) => { s.GameOver(); });
            //TopUI.Instance.SetCurrencyEndGame();
            Debug.Log("Game over you lose");
        }

    }
    public void GameCompleted()
    {
        if (game_State != GAME_STATE.GAME_OVER && game_State != GAME_STATE.GAME_COMPLETE)
        {         
            CallEvent((s) => { s.GameCompleted(); });
        }
    }
    #endregion Game Subcribers


    public GAME_STATE game_State = GAME_STATE.GAME_COMPLETE;

    public bool isLoaded = false;
    IEnumerator IEGamePrepare()
    {
        yield return new WaitUntil(() => isLoaded);
        CallEvent((s) => { s.GamePrepare(); });
        yield return new WaitForFixedUpdate();
    }

    public bool IsPlaying
    {
        get
        {
            return game_State == GAME_STATE.GAME_PLAY;
        }
    }
    public bool IsPreparing
    {
        get
        {
            return game_State == GAME_STATE.GAME_PREPARE;
        }
    }
    public bool IsPausing
    {
        get
        {
            return game_State == GAME_STATE.GAME_PAUSE;
        }
    }
}

public enum GAME_STATE
{
    GAME_PREPARE,
    GAME_PLAY,
    GAME_PAUSE,
    GAME_REVIVAL,
    GAME_COMPLETE,
    GAME_OVER
}
}
