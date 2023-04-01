using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>,ISubcriber
{
    [SerializeField] private LevelController levelController;
    public List<Charecter> listAllTarget;
    public PlayerController playerPrefab;
    private PlayerController player;
    public FloatingJoystick floatingJoystick;
    public static int id = 0;
    float timeReload;

    private void Awake()
    {
        OnInit();
    }
    private void Start()
    {          
        GameManager.GetInstance().AddSubcriber(this);
    }
    public void ResetPos()
    {
        player.transform.position = new Vector3(0, -0.44f, 0);
        player.transform.rotation = Quaternion.Euler(0, 180, 0);
        player.gameObject.SetActive(true);     
    }
   
    private void OnInit()
    {
        player = Instantiate(playerPrefab);
        player.ResetPlayer();
        ResetPos();
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
        ResetPlayer();
    }
    public void ResetPlayer()
    {
        player.ResetPlayer();
        ResetPos();
    }
    public void GamePrepare()
    {      
        for (int i = 0; i < listAllTarget.Count; i++)
        {
            listAllTarget[i].ResetPlayer();
            listAllTarget[i].gameObject.SetActive(false);
        }
        listAllTarget.Clear();
        LoadLevelCurrent();
        listAllTarget.Add(player);
        for (int i = 0; i < levelController.levelData.numberPlayerOnMap; i++)
        {
            BotController more = ObjectsPooling.GetInstance().SpawnPlayerEnemy(SpawnRandom().position, transform);
            listAllTarget.Add(more);
        }
        ResetPlayer();
    }

    public void GameStart()
    {
       
    }

    public void GameRevival()
    {
       
    }

    public void GamePause()
    {
        throw new NotImplementedException();
    }

    public void GameResume()
    {
        throw new NotImplementedException();
    }

    public void GameOver()
    {
    }

    public void GameCompleted()
    {
        throw new NotImplementedException();
    }
}
