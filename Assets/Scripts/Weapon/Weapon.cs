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
    public Transform player;
    public Transform backPos;
    public bool isModel;
    public virtual void OnEnable()
    {
        isBack = false;
        //transform.localScale = new Vector3(1 + killPlayer * 0.2f, 1 + killPlayer * 0.2f, 1 + killPlayer * 0.2f);
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
            if(idBulletPlayer != other.GetComponent<Charecter>().id)
            {
                //Debug.LogError("Die");
                CheckPlayer(other.gameObject.GetComponent<Charecter>());
            }
         
        }
    }
    public void CheckPlayer( Charecter go)
    {
        if (idBulletPlayer != go.GetComponent<Charecter>().id)
        {
            GameManager.GetInstance().listTarget.Remove(go.gameObject);
            this.gameObject.SetActive(false);
            go.gameObject.SetActive(false);
            GameManager.GetInstance().GetKill(idBulletPlayer);
        }
    }
    public virtual void OnDisable()
    {
        if(rb != null)
        rb.velocity = Vector3.zero;
    }
    public void GetInfoPlayer(int idPlayer, Vector3 posThrow)
    {
        idBulletPlayer = idPlayer;      
        posStart = posThrow;
    }
}
