using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Interactable : MonoBehaviour
{
    public bool useEvents;
    [SerializeField]
    //message displayed to player when looking at an interactable.
    public string promptMessage;
    // this function will be called from our player.
    public virtual string OnLook()
    {
        return promptMessage;
    }
    public void Baselnteract()
    {
        if(useEvents) 
        {
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        }
        Interact();
    }
    protected virtual void Interact()
    {
        //we wont have any code written in this function
        // this is a template function to be overridden by our subclasses
    }

}