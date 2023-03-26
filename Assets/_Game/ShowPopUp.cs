using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShowPopUp
{
    public static GameObject ShowPopUps(string name, Transform parent = null, Action callback = null)
    {
        if (parent == null)
            parent = GameObject.Find("Popups").transform;
        if (parent)
        {
            GameObject ret = GameObject.Instantiate(Resources.Load<GameObject>("Popups/" + name), parent);
            callback?.Invoke();
            return ret;
        }

        return null;
    }

    static IEnumerator IEShowPopup(string name, Transform parent = null, Action callback = null)
    {
        yield return null;
    }

    public static GameObject ShowPopUpWithCloseCallback<T>(string name, Transform parent = null, Action callback = null) where T : Singleton<T>
    {


        if (parent == null)
            parent = GameObject.Find("Popups").transform;
        if (parent)
        {
            GameObject res = Resources.Load<GameObject>("Popups/" + name);
            GameObject ret = GameObject.Instantiate(res, parent);
            return ret;
        }

        return null;
    }


    public static void ShowNeedMore(int price, Action<bool> action)
    {

    }
}
