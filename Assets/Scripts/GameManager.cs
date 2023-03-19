using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<GameObject> listTarget;
    public int numberPlayerEnemy;
    public static int id = 0;
    [SerializeField] public PlayerController player;
    [SerializeField] private Data data;
    public int levelCurrent;
    public int gold;
    float timeReload;
    public float valueScare;

    private void Awake()
    {
        levelCurrent = 1;
        id = 0;
        OnInit();
    }
    void OnInit()
    {
        valueScare = 0.2f;
    }
    public void SaveData()
    {
        data.currentWeapon = (int)player.typeWeaapon;
        data.gold = gold;
        data.levelID = levelCurrent;
        SaveLoadData.GetInstance().SaveToFile();
    }
    public void GetData()
    {
        SaveLoadData.GetInstance().LoadFromFile();
        player.currentWeapon = data.currentWeapon;
        gold  = data.gold;
        Debug.LogError(gold);
        levelCurrent = data.levelID ;
    }
    private void Start()
    {
        for (int i = 0; i < numberPlayerEnemy; i++)
        {
            BotController more = ObjectsPooling.GetInstance().SpawnPlayerEnemy(SpawnRandom(), transform);
            listTarget.Add(more.gameObject);
        }
    }
    void Update()
    {
        timeReload++;
        if(timeReload > 10)
        if (listTarget.Count < numberPlayerEnemy)
        {
            BotController more = ObjectsPooling.GetInstance().SpawnPlayerEnemy(SpawnRandom(), transform);
            listTarget.Add(more.gameObject);
        }
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
        if (idBullet == player.id)
        {
            UpSize(player);
        }
        else
        {
            for (int i = 1; i < listTarget.Count; i++)
            {
                if(listTarget[i].GetComponent<BotController>().id == idBullet)
                {
                    BotController go = listTarget[i].GetComponent<BotController>();
                    UpSize(go);
                }
            }          
        }
    }
    public void UpSize(Charecter player)
    {
        player.target = null;
        player.killed++;
        player.transform.localScale = new Vector3(1 + player.killed * valueScare, 1 + player.killed * valueScare, 1 + player.killed * valueScare);
        ChangeEquiment.GetInstance().ChangeWeapon(player.currentWeapon, player.colliderRange, player.spriteRange,player.typeWeaapon);
    }
}
