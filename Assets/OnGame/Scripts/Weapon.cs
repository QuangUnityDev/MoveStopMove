using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int idBulletPlayer;
    public Rigidbody rb;
    public Vector3 posStart;
    public float rangWeapon;
    public int killPlayer;
    public float shootForce;
    protected Action finishCallBack;
    protected bool isBack;
<<<<<<<< HEAD:Assets/OnGame/Scripts/Weapon/Weapon.cs
    public Transform player;
    public Transform backPos;
    public bool isModel;
========
>>>>>>>> 2269ca4b1d3238cae9bb3625c0d16147b67ca18e:Assets/OnGame/Scripts/Weapon.cs
    public virtual void OnEnable()
    {
        isBack = false;
    }
    public virtual void FixedUpdate()
    {            
       
    }
    public void SetTarGet(Action callBack)
    {
        finishCallBack = callBack;   
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalTag.playerEnemy) || other.CompareTag(GlobalTag.player))
        {
            Debug.LogError("Die");      
            if(idBulletPlayer != other.GetComponent<Charecter>().id)
            CheckPlayer(other.gameObject);

        }
    }
    public void CheckPlayer( GameObject go)
    {
        GameManager.GetInstance().listTarget.Remove(go.gameObject);
        transform.gameObject.SetActive(false);
        go.gameObject.SetActive(false);
        GameManager.GetInstance().GetKill(idBulletPlayer);
    }
    public virtual void OnDisable()
    {
        if(rb != null)
        rb.velocity = Vector3.zero;
    }
    public void GetInfoPlayer(int idPlayer, Vector3 posThrow, int killPlayer)
    {
        idBulletPlayer = idPlayer;      
        posStart = posThrow;
        this.killPlayer = killPlayer;
    }
}
