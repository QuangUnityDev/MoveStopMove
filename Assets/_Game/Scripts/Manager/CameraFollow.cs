using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour, ISubcriber
{
    private PlayerController player;
    private Transform _transform;
    public Camera cameraMain;

    private void Awake()
    {       
        _transform = cameraMain.transform;
    }
    private void Start()
    {
        GameManager.GetInstance().AddSubcriber(this);
        player = GameObject.FindAnyObjectByType<PlayerController>();
    }
    void LateUpdate()
    {
        if (GameManager.GetInstance().IsPreparing) return;
        _transform.position = new Vector3(player.transform.position.x, player.killed + 15, player.transform.position.z - 5);
    }
    public void GameCompleted()
    {
    }

    public void GameOver()
    {
      
    }

    public void GamePause()
    {
    }

    public void GamePrepare()
    {
        posPrepareGame();
    }

    public void GameResume()
    {
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
        _transform.position = new Vector3(0, 1, -12);
        _transform.rotation = Quaternion.Euler(9, 0, 0);
    }
    public void posStartGame()
    {
        cameraMain.fieldOfView = 90;
        _transform.position = new Vector3(0, 13, -5);
        _transform.rotation = Quaternion.Euler(70, 0, 0);
        _transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 10);
    }
    public void posOpenSkinShop()
    {
        cameraMain.fieldOfView = 50;
        _transform.position = new Vector3(0, -1, -12);
        _transform.rotation = Quaternion.Euler(9, 0, 0);
    }
}
