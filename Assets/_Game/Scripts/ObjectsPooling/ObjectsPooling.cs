using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPooling : Singleton<ObjectsPooling>
{

    [SerializeField] private List<Axe> listAxes;
    [SerializeField] private List<CandyTree> listCandyTree;
    [SerializeField] private List<Boomerang> listBoomerangs;
    [SerializeField] private List<BotController> listBotController;
    [SerializeField] private bool isCreateNew = true;
    [SerializeField] private List<Transform> contain;
    [SerializeField] private List<ParticleSystem> effectDie;
    private bool _isHadObject = false;

    public Axe SpawnBullet(Transform playerTransform )
    {
        for (int i = 0; i < listAxes.Count; i++)
        {
            if (!listAxes[i].gameObject.activeSelf)
            {
                listAxes[i].transform.SetPositionAndRotation(playerTransform.position, playerTransform.rotation);
                listAxes[i].gameObject.SetActive(true);       
                _isHadObject = true;
                return listAxes[i];
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
                Axe more = Instantiate(listAxes[0], playerTransform.position, playerTransform.rotation);
                more.transform.SetPositionAndRotation(playerTransform.position, playerTransform.rotation);
                more.gameObject.SetActive(true);
                listAxes.Add(more);
                return more;
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
                listBoomerangs[i].transform.SetPositionAndRotation(playerTransform.position, Quaternion.Euler(0,0,0));
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
                Boomerang more = Instantiate(listBoomerangs[0], playerTransform.position, Quaternion.Euler(0, 0, 0));
                more.transform.SetPositionAndRotation(playerTransform.position, playerTransform.rotation);
                more.gameObject.SetActive(true);
                listBoomerangs.Add(more);
                return more;
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
                LevelManager.id++;
                listBotController[i].transform.SetLocalPositionAndRotation(pos, Quaternion.Euler(0, 180, 0));               
                listBotController[i].gameObject.SetActive(true);
                listBotController[i].id = LevelManager.id;
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
                LevelManager.id++;
                BotController more = Instantiate(listBotController[0], contain[1].transform);               
                more.id = LevelManager.id;
                more.transform.SetLocalPositionAndRotation(pos, Quaternion.Euler(0, 180, 0));
                more.gameObject.SetActive(true);
                listBotController.Add(more);
                return more;
            }
        }
        return null;

    }
    public ParticleSystem SpawnEffect(Transform playerTransform)
    {
        for (int i = 0; i < effectDie.Count; i++)
        {
            if (!effectDie[i].gameObject.activeSelf)
            {
                effectDie[i].transform.SetPositionAndRotation(playerTransform.position, playerTransform.rotation);
                effectDie[i].gameObject.SetActive(true);
                _isHadObject = true;
                return effectDie[i];
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
                ParticleSystem more = Instantiate(effectDie[0], playerTransform.position, playerTransform.rotation, contain[3].transform);
                more.transform.SetPositionAndRotation(playerTransform.position, playerTransform.rotation);
                more.gameObject.SetActive(true);
                effectDie.Add(more);
                return more;
            }
        }
        return null;

    }
    public CandyTree SpawnCandyTree(Transform playerTransform)
    {
        for (int i = 0; i < listAxes.Count; i++)
        {
            if (!listCandyTree[i].gameObject.activeSelf)
            {
                listCandyTree[i].transform.SetPositionAndRotation(playerTransform.position, playerTransform.rotation);
                listCandyTree[i].gameObject.SetActive(true);
                _isHadObject = true;
                return listCandyTree[i];
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
                CandyTree more = Instantiate(listCandyTree[0], playerTransform.position, playerTransform.rotation,contain[4]);
                more.transform.SetPositionAndRotation(playerTransform.position, playerTransform.rotation);
                more.gameObject.SetActive(true);
                listCandyTree.Add(more);
                return more;
            }
        }
        return null;

    }
}
