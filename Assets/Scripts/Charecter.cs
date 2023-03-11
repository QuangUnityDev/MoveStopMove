using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charecter : MonoBehaviour
{
    [SerializeField]protected Rigidbody rb;
    public Animator anim;

    public TypeWeaapon typeWeaapon;
    public Transform target;
    public float speed;
    private string currentAnim;
   
    public float timeToAttack;
    public Transform throwPos;
    public float time;
    public bool isAttack;
    public int killed;
    public int id;
   
    public Transform tranformRange;
    private float rangeAttack;

    public enum TypeWeaapon
    {
        BULLET,
        SWORD,
        BOOMERANG,
    }

    public void ChangeAnim(string animName)
    {
        if (animName != currentAnim)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
    private void Update()
    {
        rangeAttack = tranformRange.GetComponent<SphereCollider>().radius;
        if (target != null)
        {
            dir = target.position - throwPos.position;
            if (!target.gameObject.activeSelf)
            {
                isAttack = false;
            }
        }      
    }
    public virtual void OnInit()
    {
        isAttack = false;
    }
    Vector3 dir;
    public virtual void Attack()
    {
        switch (typeWeaapon)
        {
            case TypeWeaapon.BULLET:
                Bullet more = ObjectsPooling.GetInstance().SpawnBullet(throwPos);             
                more.rb.AddForce(dir.normalized * more.shootForce, ForceMode.VelocityChange);
                more.GetInfoPlayer(id, throwPos.position, rangeAttack);
                break;
            case TypeWeaapon.BOOMERANG:
                Boomerang boome = ObjectsPooling.GetInstance().SpawnBoomerang(throwPos); 
                boome.rb.AddForce(dir.normalized * boome.shootForce, ForceMode.VelocityChange);
                boome.GetInfoPlayer(id, throwPos.position, rangeAttack);
                break;
            case TypeWeaapon.SWORD:
                break;
            default:
                break;
        }

    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerEnemy"))
        {
            isAttack = true;
            target = other.gameObject.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerEnemy"))
        {
            isAttack = false;
            target = null;
        }
    }
}
