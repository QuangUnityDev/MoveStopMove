using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private LevelController levelController;
    public List<Charecter> listAllTarget;
    public PlayerController playerPrefab;
    private PlayerController player;
    private Transform posStartPlayer;
    public FloatingJoystick floatingJoystick;
    public static int id = 0;
    float timeReload;

    private void Awake()
    {
        OnInit();
    }
    private void Start()
    {
        LoadLevelCurrent();
        for (int i = 0; i < levelController.levelData.numberPlayerOnMap; i++)
        {
            BotController more = ObjectsPooling.GetInstance().SpawnPlayerEnemy(SpawnRandom().position, transform);
            listAllTarget.Add(more);
        }
    }
    public void ResetPos()
    {
        player.gameObject.SetActive(true);
        player.transform.position = posStartPlayer.position;
    }
    private void OnInit()
    {
        player = Instantiate(playerPrefab, posStartPlayer);
        player.floatingJoystick = floatingJoystick;
        player.id = 0;
        listAllTarget.Add(player);
        CallSpawnAgain(() => ObjectsPooling.GetInstance().SpawnPlayerEnemy(SpawnRandom().position, transform));
    }
    public Action callSpawn;
    public void CallSpawnAgain(Action call)
    {
        callSpawn = call;
    }
    public void LoadLevelCurrent()
    {
        levelController = Instantiate(Resources.Load<LevelController>("Levels/Level" + GameManager.GetInstance().levelCurrent.ToString()));
        posStartPlayer = levelController.playerPosStart;
        ResetPos();
    }
    int pos;
    public Transform SpawnRandom()
    {       
        pos++;
        if (pos > levelController.posAppear.Length - 1) pos = 0;
        return levelController.posAppear[pos].transform;
    }
    public void NextLevel()
    {
        if (levelController != null)
        {
            Destroy(levelController.gameObject);
        }
        GameManager.GetInstance().levelCurrent++;
        GameManager.GetInstance().SaveData();
        Debug.LogError(GameManager.GetInstance().levelCurrent);
        levelController = Instantiate(Resources.Load<LevelController>("Levels/Level" + GameManager.GetInstance().levelCurrent.ToString()));
        posStartPlayer = levelController.playerPosStart;
        ResetPos();
    }
}
