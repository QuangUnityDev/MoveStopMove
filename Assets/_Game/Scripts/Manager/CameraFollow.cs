using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour, ISubcriber
{
    private PlayerController player;
    private Transform Transform;
    public Camera cameraMain;

    private void Awake()
    {
        Transform = cameraMain.transform;

    }
    private void Start()
    {       
        player = GameObject.FindAnyObjectByType<PlayerController>();
        GameManager.GetInstance().AddSubcriber(this);

    }
    void Update()
    {
        if (GameManager.GetInstance().IsPreparing) return;
        Transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 5);
    }
    public void GameCompleted()
    {
        throw new System.NotImplementedException();
    }

    public void GameOver()
    {
        throw new System.NotImplementedException();
    }

    public void GamePause()
    {
        throw new System.NotImplementedException();
    }

    public void GamePrepare()
    {
        cameraMain.fieldOfView = 50;
        Transform.position = new Vector3(0, 1, -12);
        Transform.rotation = Quaternion.Euler(9, 0, 0);
    }

    public void GameResume()
    {
        throw new System.NotImplementedException();
    }

    public void GameRevival()
    {
        throw new System.NotImplementedException();
    }

    public void GameStart()
    {
        cameraMain.fieldOfView = 90;
        Transform.position = new Vector3(0, 10, -5);
        Transform.rotation = Quaternion.Euler(70, 0, 0);
        Transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 10);
    }
}
