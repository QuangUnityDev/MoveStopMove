using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private PlayerController player;
    private Transform Transform;
    private void Awake()
    {
        Transform = transform;
    }
    private void Start()
    {      
        player = GameObject.FindAnyObjectByType<PlayerController>();
    }
    void Update()
    {
        Transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 10);
    }
}
