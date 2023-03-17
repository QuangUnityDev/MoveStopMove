using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameSubscriber
{
    void GamePrepare();
    void GameStart();
    void GameRevival();
    void GamePause();
    void GameResume();
    void GameOver();
    void GameCompleted();
}