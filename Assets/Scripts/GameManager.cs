using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //[SerializeField] private List<GameObject> levelData;
    public List<GameObject> listTarget;
    public int numberPlayerEnemy;
    public static int id = 0;
    private bool isSpawn;

    private void Awake()
    {
        GameManager.id = 0;
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
        if (idBullet == 0 && !listTarget[0].gameObject.activeSelf)
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
