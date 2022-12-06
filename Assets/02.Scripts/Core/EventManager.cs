using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    private static Dictionary<string, Action<object[]>> eventDictionary = new Dictionary<string, Action<object[]>>();

    public static void StartListening(string eventName, Action<object[]> listener) 
    {
        Action<object[]> thisEvent;
        {

            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent += listener;
                eventDictionary[eventName] = thisEvent;
            }

            else
            {
                eventDictionary.Add(eventName, listener);
            }
        }
    }

    public static void StopListening(string eventName, Action<object[]> listener)
    {
        Action<object[]> thisEvent;

        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            eventDictionary[eventName] = thisEvent;
        }

        else
        {
            eventDictionary.Remove(eventName);
        }
    }
    public static void TriggerEvent(string eventName, object[] param = null) 
    {
        Action<object[]> thisEvent;

        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent?.Invoke(param);
        }
    }

}


