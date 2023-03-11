using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int idBulletPlayer;
    public Rigidbody rb;
    public Vector3 posStart;
    public float rangOfPlayer;
    public float shootForce;
    protected Action finishCallBack;
    protected bool isBack;
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
            CheckPlayer(other.gameObject);

        }
        //if (other.CompareTag(GlobalTag.playerEnemy))
        //{
        //    Debug.LogError("Die");
        //    if (other.gameObject.GetComponent<BotController>().id != idBulletPlayer)
        //    {
        //        CheckPlayer(other.gameObject);
        //    }

        //}
        //else if (other.CompareTag(GlobalTag.player))
        //{
        //    Debug.LogError("Die");
        //    if (other.gameObject.GetComponent<PlayerController>().id != idBulletPlayer)
        //    {
        //        CheckPlayer(other.gameObject);
        //    }

        //}
    }
    public void CheckPlayer( GameObject go)
    {
        GameManager.GetInstance().listTarget.Remove(go.gameObject);
        transform.gameObject.SetActive(false);
        go.gameObject.SetActive(false);
        GameManager.GetInstance().GetKill(idBulletPlayer);
    }
    public virtual  void OnDisable()
    {
        rb.velocity = Vector3.zero;
    }
    public void GetInfoPlayer(int idPlayer, Vector3 posThrow,float RangAttack)
    {
        idBulletPlayer = idPlayer;
        rangOfPlayer = RangAttack;
        posStart = posThrow;
    }
}
