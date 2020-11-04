using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    public List<EventAndResponse> eventAndResponses = new List<EventAndResponse>();

    private void OnEnable()
    {

        if (eventAndResponses.Count >= 1)
        {
            foreach (EventAndResponse eAndR in eventAndResponses)
            {
                eAndR.gameEvent.AddListener(this);
            }
        }
    }

    private void OnDisable()
    {
        if (eventAndResponses.Count >= 1)
        {
            foreach (EventAndResponse eAndR in eventAndResponses)
            {
                eAndR.gameEvent.RemoveListener(this);
            }
        }
    }

    public void OnEventRaised(GameEvent passedEvent)
    {
        for (int i = eventAndResponses.Count - 1; i >= 0; i--)
        {
            if (passedEvent == eventAndResponses[i].gameEvent)
            {
                eventAndResponses[i].EventRaised();
            }
        }
    }
}


[System.Serializable]
public class EventAndResponse
{
    public string name;
    public GameEvent gameEvent;
    public UnityEvent response;
    

    public void EventRaised()
    {
        response?.Invoke();
    }
}