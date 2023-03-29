using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRotate : MonoBehaviour
{
    private Transform Transform;
    [SerializeField]private float speedRotate;
    private void Awake()
    {
        Transform = transform;
    }
    void Update()
    {
        Transform.Rotate(0, 0, Transform.rotation.z + speedRotate);
    }
}
