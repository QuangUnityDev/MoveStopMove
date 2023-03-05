using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charecter : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public Animator anim;
    private string currentAnim;
    public Transform target;
    public float timeToAttack;
    public Transform throwPos;
    public float time;
    public bool isAttack;
    public int killed;
    public int id;
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
        Bullet more = ObjectsPooling.GetInstance().SpawnBullet(throwPos);
        more.idBulletPlayer = id;
        Debug.LogError("Ban dan");
        more.rb.AddForce(dir.normalized * 15f,ForceMode.Impulse);
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
        }
    }   
}
