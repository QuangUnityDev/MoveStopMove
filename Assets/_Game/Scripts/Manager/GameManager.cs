using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Data dataPlayer;
    public static int numberOfReviveInOneTimesPlay;
    private void Awake()
    {
        SaveData();
        InitFirstPlay();
        gameSubcribers = new ArrayList();            
        OnInit();
    }
    private void Start()
    {
        OnInit();
    }
    void OnInit()
    {
        GamePrepare();
    }
    private void InitFirstPlay()
    {
        LoadData();
        if (!dataPlayer.isFirst)
        {
            dataPlayer.weaponOwner.Clear();
            dataPlayer.skinOwner.Clear();
            dataPlayer.shortsOwner.Clear();
            dataPlayer.hornorsOwner.Clear();
            dataPlayer.armOwner.Clear();

            dataPlayer.skinAxeOwer.Clear();
            dataPlayer.skinBoomerangOwer.Clear();
            dataPlayer.skinCandyTreeOwer.Clear();

            dataPlayer.isFirst = true;
            dataPlayer.weaponOwner.Add(0);
            dataPlayer.equipedWeapon = TypeWeaapon.AXE;
            dataPlayer.gold = 1000;
            dataPlayer.levelID = 1;
            dataPlayer.currentSkin = 0;
            dataPlayer.skinAxeOwer.Add(0);
            dataPlayer.skinBoomerangOwer.Add(0);
            dataPlayer.skinCandyTreeOwer.Add(0);
            dataPlayer.equipedSkinWeapon = 0;
            SaveData();
        }
    }
    public void SaveData()
    {
        SaveLoadData.GetInstance().SaveToFile(this);
    }
    public void LoadData()
    {
        //Data Game
        Data data = SaveLoadData.GetInstance().LoadFromFile();
        dataPlayer.equipedWeapon = data.equipedWeapon;
        dataPlayer.gold = data.gold;
        dataPlayer.levelID = data.levelID;

        //Skin
        dataPlayer.weaponOwner = data.weaponOwner;
        dataPlayer.skinOwner = data.skinOwner;
        dataPlayer.shortsOwner = data.shortsOwner;
        dataPlayer.hornorsOwner = data.hornorsOwner;
        dataPlayer.armOwner = data.armOwner;
        dataPlayer.currentSkin = data.currentSkin;
        dataPlayer.currentItemSkin = data.currentItemSkin;

        //WeaponSkin
        dataPlayer.skinAxeOwer = data.skinAxeOwer;      
        dataPlayer.skinBoomerangOwer = data.skinBoomerangOwer;
        dataPlayer.skinCandyTreeOwer = data.skinCandyTreeOwer;

        dataPlayer.equipedSkinWeapon = data.equipedSkinWeapon;

        dataPlayer.isFirst = data.isFirst;
}
    #region Game Subcribers
    public ArrayList gameSubcribers;
    public delegate void EventCall(ISubcriber s);
    float startTime = 0;
    public void AddSubcriber(ISubcriber s)
    {
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
        //ShowPopUp.ShowPopUps(StringNamePopup.PopupHome);
        UIManager.GetInstance().ShowPopUpHome(true);
        numberOfReviveInOneTimesPlay = 1;
    }
    public void GameStart()
    {
        callShowRangePlayer?.Invoke(true);
        startTime = Time.time;
        game_State = GAME_STATE.GAME_PLAY;
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
            CallEvent((s) => { s.GameOver(); });
            Debug.Log("Game over you lose");
        }

    }
    public void GameCompleted()
    {
        if (game_State != GAME_STATE.GAME_OVER && game_State != GAME_STATE.GAME_COMPLETE)
        {
            int duration = Mathf.RoundToInt(Time.time - startTime);
            CallEvent((s) => { s.GameCompleted(); });
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
    public bool IsOverGame
    {
        get
        {
            return game_State == GAME_STATE.GAME_OVER;
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
