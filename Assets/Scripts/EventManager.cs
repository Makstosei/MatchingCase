using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    #region Singleton

    private static EventManager _instance;

    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<EventManager>();
            return _instance;
        }
    }
    #endregion

    public static Action onGameStart;
    public static Action onUpdateItems;
    public static Action<SpawnedItem, SpawnedItem, int,bool> onMoveItems;//(1:left - 2:right - 3: down - 4:up)

    public void GameStart()
    {
        onGameStart.Invoke();
    }

    public void UpdateItems()
    {
        onUpdateItems.Invoke();
    }

    public void MoveItems(SpawnedItem effecter,SpawnedItem effected,int moveDirection,bool isReverse)
    {
        onMoveItems.Invoke(effecter, effected, moveDirection,isReverse);
    }

}
