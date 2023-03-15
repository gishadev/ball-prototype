using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gisha.BallGame.Core
{
    public class EventManager : MonoBehaviour
    {
        private Dictionary<string, Action<Dictionary<string, object>>> _eventDictionary;
        private static EventManager _eventManager;

        public static EventManager Instance
        {
            get
            {
                if (!_eventManager)
                {
                    _eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                    if (!_eventManager)
                        Debug.LogError("No one active EventManager script in your scene.");
                    else
                    {
                        _eventManager.Init();
                        DontDestroyOnLoad(_eventManager);
                    }
                }

                return _eventManager;
            }
        }

        private void Init()
        {
            if (_eventDictionary == null)
                _eventDictionary = new Dictionary<string, Action<Dictionary<string, object>>>();
        }

        public static void StartListening(string eventName, Action<Dictionary<string, object>> listener)
        {
            Action<Dictionary<string, object>> thisEvent;

            if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent += listener;
                Instance._eventDictionary[eventName] = thisEvent;
            }
            else
            {
                thisEvent += listener;
                Instance._eventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void StopListening(string eventName, Action<Dictionary<string, object>> listener)
        {
            if (_eventManager == null) return;
            Action<Dictionary<string, object>> thisEvent;
            if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent -= listener;
                Instance._eventDictionary[eventName] = thisEvent;
            }
        }

        public static void TriggerEvent(string eventName, Dictionary<string, object> message)
        {
            Action<Dictionary<string, object>> thisEvent = null;
            if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(message);
            }
        }
    }
}