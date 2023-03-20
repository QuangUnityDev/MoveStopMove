using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjectsPooling : MonoBehaviour
{
    public enum ObjectType
    {
        Player,
        Bullet,
        Enemy
    }
    
    private Dictionary<ObjectType, List<GameObject>> A = new Dictionary<ObjectType, List<GameObject>>();
    GameObject GetGameObject(ObjectType type)
    {
        if (A.ContainsKey(type))
        {
            if (A[type].Count > 0)
            {

            }
            else
            {

            }
        }
        else
        {
            A.Add(type, new List<GameObject>());

        }
        return null;
    }
}
