using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    LevelController currentLevel;
    private void Start()
    {
        LoadLevel();
    }
    public void LoadLevel()
    {
        currentLevel = Instantiate(Resources.Load<LevelController>("Levels/Level" + GameManager.GetInstance().levelCurrent.ToString()));
    }
    public void NextLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        GameManager.GetInstance().levelCurrent++;
        GameManager.GetInstance().SaveData();
        Debug.LogError(GameManager.GetInstance().levelCurrent);
        currentLevel = Instantiate(Resources.Load<LevelController>("Levels/Level" + GameManager.GetInstance().levelCurrent.ToString()));
    }
}
