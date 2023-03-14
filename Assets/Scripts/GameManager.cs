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
}
