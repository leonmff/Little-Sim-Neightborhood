using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DisplayInventory : MonoBehaviour
{
    public void CreateSlots()
    {

    }

    void AddEvent(GameObject pObject, EventTriggerType pType, UnityAction<BaseEventData> pAction)
    {
        EventTrigger t_eventTrigger = pObject.GetComponent<EventTrigger>();
        EventTrigger.Entry t_eventTriggerEntry = new EventTrigger.Entry();

        t_eventTriggerEntry.eventID = pType;
        t_eventTriggerEntry.callback.AddListener(pAction);

        t_eventTrigger.triggers.Add(t_eventTriggerEntry);
    }

    public void OnEnter(GameObject pObject)
    {
        
    }
}
