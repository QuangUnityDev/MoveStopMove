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
    [SerializeField]private float shootForce;
    public float ShootForce { private set { shootForce = value; } get { return shootForce; }}
    protected Action finishCallBack;
    protected bool isBack;
    public Charecter player;    
    protected Transform Transform;
    private void Awake()
    {
        OnInit();
    }

    public virtual void OnEnable()
    {
        OnInit();
    }
    public virtual void OnInit()
    {
        isBack = false;
        Transform = transform;       
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
            CheckPlayer(other.gameObject.GetComponent<Charecter>());
        }
    }
    public void CheckPlayer( Charecter go)
    {
        if (idBulletPlayer != go.GetComponent<Charecter>().id)
        {
            GameManager.GetInstance().listTarget.Remove(go);
            this.gameObject.SetActive(false);
            go.gameObject.SetActive(false);
            player.listTargetInRange.Remove(go);
            player.removeTarget?.Invoke();
            LevelManager.GetInstance().GetKill(idBulletPlayer);
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
