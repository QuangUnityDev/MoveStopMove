using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private LevelController levelController;
    public List<Charecter> listAllTarget;
    float timeReload;

    private void Start()
    {
        LoadLevel();
        for (int i = 0; i < levelController.levelData.numberPlayerOnMap; i++)
        {
            BotController more = ObjectsPooling.GetInstance().SpawnPlayerEnemy(SpawnRandom().position, transform);
            listAllTarget.Add(more);
        }
    }
    void Update()
    {     
        //timeReload++;
        //if (timeReload > 2)
        //    for (int i = 0; i < listAllTarget.Count; i++)
        //    {
        //        if (!listAllTarget[i].gameObject.activeSelf)
        //        {
        //            BotController more = ObjectsPooling.GetInstance().SpawnPlayerEnemy(SpawnRandom().position, transform);
        //            listAllTarget.Add(more);
        //            timeReload = 0;
        //            break;
        //        }
        //    }           
    }
    public void LoadLevel()
    {
        levelController = Instantiate(Resources.Load<LevelController>("Levels/Level" + GameManager.GetInstance().levelCurrent.ToString()));
    }
    public Transform SpawnRandom()
    {
        return levelController.posAppear[UnityEngine.Random.Range(0, levelController.posAppear.Length)].transform;
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
    }
    public void GetKill(int idBullet)
    {
        if (idBullet == 0)
        {
            UpSize(listAllTarget[0].GetComponent<PlayerController>());
        }
        else
        {
            for (int i = 1; i < listAllTarget.Count; i++)
            {
                if (listAllTarget[i].GetComponent<BotController>().id == idBullet)
                {
                    BotController go = listAllTarget[i].GetComponent<BotController>();
                    UpSize(go);
                }
            }
        }
    }
    public void UpSize(Charecter player)
    {
        player.killed++;
        player.transform.localScale = new Vector3(1 + player.killed * 0.2f, 1 + player.killed * 0.2f, 1 + player.killed * 0.2f);
        ChangeEquiment.GetInstance().ChangeWeapon(player.currentWeapon, player.colliderRange, player.spriteRange, player.typeWeaapon);
    }

}
