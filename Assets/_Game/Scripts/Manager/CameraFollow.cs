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
        GameManager.GetInstance().AddSubcriber(this);
        player = GameObject.FindAnyObjectByType<PlayerController>();
    }
    void LateUpdate()
    {
        if (GameManager.GetInstance().IsPreparing) return;
        Transform.position = new Vector3(player.transform.position.x, player.killed * 0.2f + 15, player.transform.position.z - 5);
    }
    public void GameCompleted()
    {
        throw new System.NotImplementedException();
    }

    public void GameOver()
    {
      
    }

    public void GamePause()
    {
        throw new System.NotImplementedException();
    }

    public void GamePrepare()
    {
        posPrepareGame();
    }

    public void GameResume()
    {
        throw new System.NotImplementedException();
    }

    public void GameRevival()
    {
    }

    public void GameStart()
    {
        posStartGame();
    }
    public void posPrepareGame()
    {
        cameraMain.fieldOfView = 50;
        Transform.position = new Vector3(0, 1, -12);
        Transform.rotation = Quaternion.Euler(9, 0, 0);
    }
    public void posStartGame()
    {
        cameraMain.fieldOfView = 90;
        Transform.position = new Vector3(0, 15, -5);
        Transform.rotation = Quaternion.Euler(70, 0, 0);
        Transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 10);
    }
    public void posOpenSkinShop()
    {
        cameraMain.fieldOfView = 50;
        Transform.position = new Vector3(0, -1, -12);
        Transform.rotation = Quaternion.Euler(9, 0, 0);
    }
}
