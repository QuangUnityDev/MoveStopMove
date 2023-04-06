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
    public float ShootForce { set { shootForce = value; } get { return shootForce; }}
    protected Action finishCallBack;
    protected bool isBack;
    public Charecter player;    
    protected Transform Transform;
    private Action callDeath;
    public MeshRenderer mesh;
    private void Awake()
    {
        Transform = transform;
        OnInit();
    }

    public virtual void OnEnable()
    {
        OnInit();
    }
    public virtual void OnInit()
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
        if (shootForce <= 0) return;
        if (other.CompareTag(GlobalTag.playerEnemy) || other.CompareTag(GlobalTag.player))
        {            
            CheckPlayer(other.gameObject.GetComponent<Charecter>());
        }
    }
    public void CheckPlayer( Charecter go)
    {
        if (idBulletPlayer != go.id)
        {
            if (ShootForce <= 0) return;
            ObjectsPooling.GetInstance().SpawnEffect(go.transform);
            gameObject.SetActive(false);                
            GetDamage(go,player);
        }
    }
    public virtual void OnDisable()
    {
        Transform.localScale = new Vector3(1,1,1);
        if(rb != null)
        rb.velocity = Vector3.zero;
    }
    public void GetInfoPlayer(int idPlayer, Vector3 posThrow)
    {
        idBulletPlayer = idPlayer;      
        posStart = posThrow;
        shootForce = 0;
    }
    public void GetKill(Charecter playerKilled)
    {
        playerKilled.killed++;
        UpSize(playerKilled);
    }
    public void UpSize(Charecter player)
    {
        player.transform.localScale = new Vector3(1 + player.killed * 0.2f, 1 + player.killed * 0.2f, 1 + player.killed * 0.2f);
        ChangeEquiment.GetInstance().ResetAtributeWeapon(player.currentWeapon, player.colliderRange, player.spriteRange, player);
    }
    public void GetDamage(Charecter playerGetDamage, Charecter playerKill)
    {
        playerGetDamage.hp--;
        if (playerGetDamage.hp <= 0)
        {
            PlayerDeath( () =>
            {
                Dead(playerGetDamage, playerKill);
            }
                );
            callDeath?.Invoke();
            callDeath = null;
        }
    }
    public void Dead (Charecter playerGetDamage, Charecter playerKill)
    {
        playerGetDamage.OnDeath();
        GetKill(playerKill);
    }
    public void PlayerDeath(Action call = null)
    {
        callDeath += call;
    }
}
