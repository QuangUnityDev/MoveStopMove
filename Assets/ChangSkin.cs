using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangSkin : MonoBehaviour
{
    [SerializeField] Transform posBow;
    [SerializeField] Transform posWing;
    [SerializeField] Transform posTail;
    [SerializeField] Transform posHead;
    [SerializeField] SkinnedMeshRenderer meshPlayer;
    [SerializeField] SkinnedMeshRenderer meshShortsPlayer;
    GameObject _wing;
    GameObject _tail;
    GameObject _head;
    GameObject _bow;

    private void Start()
    {
        ManagerSkinUsing.GetInstance().ChangeSkinUsing();
    }
    public void ChangeShorts(Material shorts)
    {
        meshShortsPlayer.gameObject.SetActive(true);
        meshShortsPlayer.material = shorts;
    }
    public void ChangeSkin(Material skin = null,GameObject wing = null,GameObject tail = null, GameObject head = null, GameObject bow = null ,Material _short = null)
    {
        meshPlayer.material = LevelManager.GetInstance().player.dataPlayer.materials[GameManager.GetInstance().dataPlayer.currentSkin];
        if (_wing) 
        { 
            Destroy(_wing);
        }
        if (wing)
        {
            _wing = Instantiate(wing, posWing);
            _wing.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0,0,0));
        }

        if (_tail)
        {
            Destroy(_tail);
        }
        if (tail) { 
            _tail = Instantiate(tail, posTail);
            _tail.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, 0));
        }

        if (_head)
        {
            Destroy(_head);
        }
        if (head)
        {
            _head = Instantiate(head, posHead);
            _head.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, 0));
        }

            if (_bow)
        {
            Destroy(_bow); 
        }
        if (bow) 
        {           
            _bow = Instantiate(bow, posBow);
            _bow.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, 0));
        }

        if (skin) { meshPlayer.material = skin; }
       
        if (_short)
        {
            meshShortsPlayer.gameObject.SetActive(true);
            meshShortsPlayer.material = _short;
        }
        else meshShortsPlayer.gameObject.SetActive(false); 
    }
}
