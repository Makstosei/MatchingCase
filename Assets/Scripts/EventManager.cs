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


    public void GameStart()
    {
        onGameStart.Invoke();
    }

    public void UpdateItems()
    {
        onUpdateItems.Invoke();
    }

}
