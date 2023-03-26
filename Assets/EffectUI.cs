using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EffectUI : MonoBehaviour
{
    public void Update()
    {
       

    }
    public void LeftUIMove()
    {
        transform.DOMoveX(transform.position.x - 500, 0.3f);
    }
}
