using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public int idBulletPlayer;
    public Rigidbody rb;
    public float timeBack;
    public Transform target;
    bool isForce;
    private void OnEnable()
    {
        StartCoroutine(OnDespawn());
        timeBack = 0;
        isForce = false;
    }
    private void FixedUpdate()
    {
        timeBack += Time.deltaTime;
        if(timeBack > 1 && target != null && isForce == false)
        {
            rb.velocity = Vector3.zero; 
            rb.AddForce((target.transform.position - transform.position).normalized * 15, ForceMode.Impulse);
            isForce = true;
        }
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
        yield return new WaitForSeconds(3);
        transform.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
    }
}
