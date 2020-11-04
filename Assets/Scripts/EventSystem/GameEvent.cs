using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Miniclip/Events/GameEvent")]
public class GameEvent : ScriptableObject
{
    private List<EventListener> eventListeners = new List<EventListener>();

    public void Raise()
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised(this);
        }
    }

    public void AddListener(EventListener listener) => eventListeners.Add(listener);
    public void RemoveListener(EventListener listener) => eventListeners.Remove(listener);
    public void RemoveAllListeners() => eventListeners.Clear();

}