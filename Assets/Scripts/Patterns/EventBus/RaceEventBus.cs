using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

// Enum defining all possible race events
public enum RaceEventType
{
    COUNTDOWN, // Pre-race countdown
    START, // Race begins
    STOP, // Race stopped
    FINISH, // Race completed
    RESTART, // Reset race
    PAUSE, // Game paused
    QUIT // Exit to menu
}

// Static event bus - globally accessible singleton
public class RaceEventBus
{
    // Dictionary mapping event types to UnityEvents
    private static readonly Dictionary<RaceEventType, UnityEvent>
        _eventDictionary = new Dictionary<RaceEventType, UnityEvent>();
    
        // Subscribe to an event
        public static void Subscribe(RaceEventType eventType, UnityAction listener)
        {
            // If event doesn't exist in dictionary, create it
            if (!_eventDictionary.ContainsKey(eventType))
            {
                _eventDictionary[eventType] = new UnityEvent();
            }
            
            // Add listener to the event 
            _eventDictionary[eventType].AddListener(listener);
        }

        // Unsubscribe from an event
        public static void Unsubscribe(RaceEventType eventType, UnityAction listener)
        {
            // if event exists, remove the listener
            if (_eventDictionary.ContainsKey(eventType))
            {
                _eventDictionary[eventType].RemoveListener(listener);
            }
        }

        public static void Publish(RaceEventType eventType)
        {
            // If event exists, invoke all listeners
            if (_eventDictionary.ContainsKey(eventType))
            {
                _eventDictionary[eventType].Invoke();
            }
        }
        
        
}
