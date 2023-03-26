using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> listIndiactor;
    [SerializeField] private bool isCreateNew = true;
    private bool _isHadObject = false;

    public GameObject SpawnBullet(Transform playerTransform)
    {
        for (int i = 0; i < listIndiactor.Count; i++)
        {
            if (!listIndiactor[i].gameObject.activeSelf)
            {
                listIndiactor[i].transform.SetPositionAndRotation(playerTransform.position, playerTransform.rotation);
                listIndiactor[i].gameObject.SetActive(true);
                _isHadObject = true;
                return listIndiactor[i];
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
                GameObject more = Instantiate(listIndiactor[0].gameObject, playerTransform.position, playerTransform.rotation,transform);
                more.transform.SetPositionAndRotation(playerTransform.position, playerTransform.rotation);
                more.gameObject.SetActive(true);
                listIndiactor.Add(more);
                return more;
            }
        }
        return null;

    }
}
