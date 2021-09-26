using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public virtual void Interact() 
    {
        Debug.Log($"<size=22><color=orange>Interacted with ''{transform.name}''</color></size>");
    }
}
