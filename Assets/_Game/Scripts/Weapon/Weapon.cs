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
    private Action callDeath;
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
        Transform = transform;
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
            CheckPlayer(other.gameObject.GetComponent<Charecter>());
        }
    }
    public void CheckPlayer( Charecter go)
    {
        if (idBulletPlayer != go.id)
        {
            ObjectsPooling.GetInstance().SpawnEffect(go.transform);
            gameObject.SetActive(false);                
            GetDamage(go,player);
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
    public void GetKill(Charecter playerKilled)
    {
        playerKilled.killed++;
        UpSize(playerKilled);
    }
    public void UpSize(Charecter player)
    {
        player.transform.localScale = new Vector3(1 + player.killed * 0.2f, 1 + player.killed * 0.2f, 1 + player.killed * 0.2f);
        ChangeEquiment.GetInstance().ChangeWeapon(player.currentWeapon, player.colliderRange, player.spriteRange, player.typeWeaapon);
    }
    public void GetDamage(Charecter playerHeal, Charecter playerKill)
    {
        playerHeal.hp--;
        if (playerHeal.hp <= 0)
        {
            PlayerDeath( () =>
            {
                Dead(playerHeal, playerKill);
            }
                );
            callDeath?.Invoke();
            callDeath = null;
        }
    }
    public void Dead (Charecter playerHeal, Charecter playerKill)
    {
        playerHeal.OnDeath();
        GetKill(playerKill);
    }
    public void PlayerDeath(Action call = null)
    {
        callDeath += call;
    }
}
