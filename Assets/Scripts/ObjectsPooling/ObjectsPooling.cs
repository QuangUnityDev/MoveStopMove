using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPooling : Singleton<ObjectsPooling>
{

    [SerializeField] private List<Bullet> listBullets;
    [SerializeField] private List<BotController> listBotController;
    [SerializeField] private bool isCreateNew = true;
    [SerializeField] private List<Transform> contain;
    private bool _isHadObject = false;

    public Bullet SpawnBullet(Transform playerTransform )
    {
        for (int i = 0; i < listBullets.Count; i++)
        {
            if (!listBullets[i].gameObject.activeSelf)
            {
                listBullets[i].transform.SetPositionAndRotation(playerTransform.position, playerTransform.rotation);
                listBullets[i].gameObject.SetActive(true);       
                _isHadObject = true;
                return listBullets[i];
            }
            else
            {
                _isHadObject = false;
            }
        }
        if (isCreateNew)
        {
            if (!_isHadObject)
            {
                GameObject more = Instantiate(listBullets[0].gameObject, playerTransform.position, playerTransform.rotation, contain[0].transform);
                more.transform.SetPositionAndRotation(playerTransform.position, playerTransform.rotation);
                Bullet bullet = more.GetComponent<Bullet>();               
                bullet.gameObject.SetActive(true);
                listBullets.Add(bullet);
                return bullet;
            }
        }
        return null;

    }
    public BotController SpawnPlayerEnemy(Vector3 pos, Transform playerTransform)
    {
        for (int i = 0; i < listBotController.Count; i++)
        {
            if (!listBotController[i].gameObject.activeSelf)
            {
                GameManager.id++;
                listBotController[i].transform.SetLocalPositionAndRotation(pos, playerTransform.rotation);               
                listBotController[i].gameObject.SetActive(true);
                listBotController[i].id = GameManager.id;
                _isHadObject = true;
                return listBotController[i];
            }
            else
            {
                _isHadObject = false;
            }
        }
        if (isCreateNew)
        {
            if (!_isHadObject)
            {
                GameManager.id++;
                BotController more = Instantiate(listBotController[0], contain[1].transform);               
                more.id = GameManager.id;
                more.transform.SetLocalPositionAndRotation(pos, playerTransform.rotation);
                more.gameObject.SetActive(true);
                listBotController.Add(more);
                return more;
            }
        }
        return null;

    }
}
