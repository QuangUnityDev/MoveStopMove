using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T GetInstance()
    {
        instance = GameObject.FindObjectOfType<T>();
        if (instance == null)
        {
            GameObject go = new GameObject();
            instance = go.AddComponent<T>();
            DontDestroyOnLoad(go);
        }

        return instance;
    }
}

