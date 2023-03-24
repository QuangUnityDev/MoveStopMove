using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int hpCurrent;
   
    private Charecter player;
    private void Awake()
    {
        player = transform.GetComponent<Charecter>();
    }
    private void Start()
    {
        hpCurrent = player.hp;
    }
   
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalTag.weapon))
        {
            Weapon go = other.GetComponent<Weapon>();
            if (go.idBulletPlayer != player.id)
            {              
               
            }  
        }
    }
   
}
