using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Data dataPlayer;
    //public int levelCurrent;
    //public int gold;
    //public int currentWeapon;
    public static int numberOfReviveInOneTimesPlay;
    //public int[] weaponOwner;
    //public int[] skinOwner;
    //public int[] shortsOwner;
    //public int[] hornorsOwner;

    //public int[] skinAxeOwer;

    //public int currentSkinWeapon;
    private void Awake()
    {
        //SaveData();
        gameSubcribers = new ArrayList();
        LoadData();
            //base.Awake();
            Input.multiTouchEnabled = false;
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            int maxScreenHeight = 1280;
            float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
            if (Screen.currentResolution.height > maxScreenHeight)
            {
                Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
            }

            //csv.OnInit();
            //userData?.OnInitData();

            //ChangeState(GameState.MainMenu);

            //UIManager.Ins.OpenUI<MianMenu>();
    }
    private void Start()
    {
        OnInit();
    }
    void OnInit()
    {
        GamePrepare();
    }
    public void SaveData()
    {
        SaveLoadData.GetInstance().SaveToFile(this);
    }
    public void LoadData()
    {
        Data data = SaveLoadData.GetInstance().LoadFromFile();
        dataPlayer.currentWeapon = data.currentWeapon;
        dataPlayer.gold = data.gold;
        dataPlayer.levelID = data.levelID;
        dataPlayer.weaponOwner = data.weaponOwner;
        dataPlayer.skinOwner = data.skinOwner;
        dataPlayer.shortsOwner = data.shortsOwner;
        dataPlayer.hornorsOwner = data.hornorsOwner;
        dataPlayer.armOwner = data.armOwner;
        dataPlayer.currentSkin = data.currentSkin;
        dataPlayer.skinAxeOwer = data.skinAxeOwer;
        dataPlayer.currentUsingSkinWeapon = data.currentUsingSkinWeapon;
        dataPlayer.skinBoomerangOwer = data.skinBoomerangOwer;
        dataPlayer.skinCandyTreeOwer = data.skinCandyTreeOwer;
}
    #region Game Subcribers
    public ArrayList gameSubcribers;
    public delegate void EventCall(ISubcriber s);
    float startTime = 0;
    //public int gameMoneyEarned;
    public void AddSubcriber(ISubcriber s)
    {
        //Debug.LogError(s);
        gameSubcribers.Add(s);
    }
    public void RemoveSubcriber(ISubcriber s)
    {
        gameSubcribers.Remove(s);
    }
    public void CallEvent(EventCall call)
    {
        foreach (ISubcriber s in gameSubcribers)
            call(s);
    }
    private Action<bool> callShowRangePlayer;
    public Action<bool> deActivePlayer;
    public void ShowRangePlayer(Action<bool> call,Action <bool> offActive = null)
    {
        callShowRangePlayer = call;
        deActivePlayer = offActive;
    }
    public void GamePrepare()
    {
        callShowRangePlayer?.Invoke(false);
        game_State = GAME_STATE.GAME_PREPARE;
        CallEvent((s) => { s.GamePrepare(); });
        numberOfReviveInOneTimesPlay = 1;
    }
    public void GameStart()
    {
        callShowRangePlayer?.Invoke(true);
        startTime = Time.time;
        game_State = GAME_STATE.GAME_PLAY;
        //gameMoneyEarned = 0;
        CallEvent((s) =>
        {
            s.GameStart();
        });
    }
    public void GameRevival()
    {
        if (game_State != GAME_STATE.GAME_REVIVAL)
        {
            game_State = GAME_STATE.GAME_REVIVAL;
            CallEvent((s) => { s.GameRevival(); });
        }

    }
    public void GamePause()
    {
        if (game_State != GAME_STATE.GAME_PAUSE)
        {
            game_State = GAME_STATE.GAME_PAUSE;
            CallEvent((s) => { s.GamePause(); });
            Time.timeScale = 0;
        }
    }
    public void GameResume()
    {
        if (game_State != GAME_STATE.GAME_PLAY)
        {
            game_State = GAME_STATE.GAME_PLAY;
            CallEvent((s) => { s.GameResume(); });
            Time.timeScale = 1;
        }

    }
    public void GameOver()
    {
        if (game_State != GAME_STATE.GAME_OVER && game_State != GAME_STATE.GAME_COMPLETE)
        {
            game_State = GAME_STATE.GAME_OVER;
            int duration = Mathf.RoundToInt(Time.time - startTime);
            callShowRangePlayer?.Invoke(false);
            //  float timer = (Time.time - durationPlayTime);
            //AnalyticHelper.OnGame.LogGameOver(duration);
            CallEvent((s) => { s.GameOver(); });
            //TopUI.Instance.SetCurrencyEndGame();
            Debug.Log("Game over you lose");
            //popUpLost.SetActive(true);
        }

    }
    public void GameCompleted()
    {
        if (game_State != GAME_STATE.GAME_OVER && game_State != GAME_STATE.GAME_COMPLETE)
        {
            int duration = Mathf.RoundToInt(Time.time - startTime);
            //AnalyticHelper.OnGame.LogGameClear(duration);
            CallEvent((s) => { s.GameCompleted(); });
            //popUpWin.SetActive(true);
            //TopUI.Instance.SetCurrencyEndGame();
        }
    }
    public GAME_STATE game_State = GAME_STATE.GAME_COMPLETE;
    #endregion Game Subcribers
    public bool IsPlaying
    {
        get
        {
            return game_State == GAME_STATE.GAME_PLAY;
        }
    }
    public bool IsPreparing
    {
        get
        {
            return game_State == GAME_STATE.GAME_PREPARE;
        }
    }
    public enum GAME_STATE
    {
        GAME_PREPARE,
        GAME_PLAY,
        GAME_PAUSE,
        GAME_REVIVAL,
        GAME_COMPLETE,
        GAME_OVER
    }
}
