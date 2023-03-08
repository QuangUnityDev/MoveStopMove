using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int idBulletPlayer;
    public Rigidbody rb;
    
    private void OnEnable()
    {
        StartCoroutine(OnDespawn());
    }
    private void OnTriggerEnter(Collider other)
    {     
        if (other.CompareTag("PlayerEnemy") || other.CompareTag("Player"))
        {
            Debug.LogError("Die");
            GameManager.GetInstance().listTarget.Remove(other.gameObject);
            transform.gameObject.SetActive(false);
            other.gameObject.SetActive(false);
            GameManager.GetInstance().GetKill(idBulletPlayer);
        }
    }
    IEnumerator OnDespawn()
    {
        yield return new WaitForSeconds(0.5f);
        transform.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
    }
}
