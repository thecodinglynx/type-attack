using System.Collections.Generic;
using UnityEngine;
using System;

/**
 * Custom event manager. Usage:
 * 
 * Producer:
 *      EventManager.TriggerEvent(EventManager.Event.ENEMY_DESTROYED, new Dictionary<string, object> { { "id", 1 } });
 * 
 * Consumer:
 *      EventManager.StartListening(EventManager.Event.ENEMY_DESTROYED, OnAddCoins);
 *      void OnAddCoins(Dictionary<string, object> message)
 *      {
 *          var id = (string)message["id"];
 *          ...
 *      }
 *
*/
public class EventManager : MonoBehaviour
{
    public enum Event
    {
        ENEMY_DESTROYED,
        SHIELD_DESTROYED,
        SPACESTATION_ATTACKED,
        GAME_STATE_CHANGED
    }

    private Dictionary<Enum, Action<Dictionary<string, object>>> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();

                    //  Sets this to not be destroyed when reloading scene
                    DontDestroyOnLoad(eventManager);
                }
            }
            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<Enum, Action<Dictionary<string, object>>>();
        }
    }

    public static void StartListening(Event eventName, Action<Dictionary<string, object>> listener)
    {
        Action<Dictionary<string, object>> thisEvent;

        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            instance.eventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(Enum eventName, Action<Dictionary<string, object>> listener)
    {
        if (eventManager == null) return;
        Action<Dictionary<string, object>> thisEvent;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            instance.eventDictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(Enum eventName, Dictionary<string, object> message)
    {
        Action<Dictionary<string, object>> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(message);
        }
    }
}