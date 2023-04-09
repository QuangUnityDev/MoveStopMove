using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    Transform _tranform;
    void Awake()
    {
        _tranform = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _tranform.Rotate(_tranform.rotation.x, _tranform.rotation.y + 1.5f, _tranform.rotation.z);
    }
}
