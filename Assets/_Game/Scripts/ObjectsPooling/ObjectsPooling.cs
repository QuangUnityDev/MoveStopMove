using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPooling : Singleton<ObjectsPooling>
{

    [SerializeField] private List<Axe> listBullets;
    [SerializeField] private List<Boomerang> listBoomerangs;
    [SerializeField] private List<BotController> listBotController;
    [SerializeField] private bool isCreateNew = true;
    [SerializeField] private List<Transform> contain;
    private bool _isHadObject = false;

    public Axe SpawnBullet(Transform playerTransform )
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
                Axe bullet = more.GetComponent<Axe>();               
                bullet.gameObject.SetActive(true);
                listBullets.Add(bullet);
                return bullet;
            }
        }
        return null;

    }
    public Boomerang SpawnBoomerang(Transform playerTransform)
    {
        for (int i = 0; i < listBoomerangs.Count; i++)
        {
            if (!listBoomerangs[i].gameObject.activeSelf)
            {
                listBoomerangs[i].transform.SetPositionAndRotation(playerTransform.position, Quaternion.identity);
                listBoomerangs[i].gameObject.SetActive(true);
                _isHadObject = true;
                return listBoomerangs[i];
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
                GameObject more = Instantiate(listBoomerangs[0].gameObject, playerTransform.position, Quaternion.identity, contain[2].transform);
                more.transform.SetPositionAndRotation(playerTransform.position, playerTransform.rotation);
                Boomerang bullet = more.GetComponent<Boomerang>();
                bullet.gameObject.SetActive(true);
                listBoomerangs.Add(bullet);
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
