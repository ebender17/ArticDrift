using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages world interactions between player and other non-combative objects.
/// </summary>
public enum InteractionType {  None = 0, PickUp, Talk }

public class Interaction
{
    public InteractionType type;
    public GameObject interactableObject;

    public Interaction(InteractionType iType, GameObject obj)
    {
        type = iType;
        interactableObject = obj;
    }
}


public class InteractionManager : MonoBehaviour
{
    [HideInInspector] public InteractionType currentInteractionType;
    [SerializeField] private InputReader _inputReader = default;

    private LinkedList<Interaction> _potentialInteractions = new LinkedList<Interaction>();

    [Header("Broadcasting on channels")]
    //UI event 
    [SerializeField] private InteractionUIEventChannelSO _toggleInteractionUI = default;

    //TODO: Broadcast interaction on event channel instead of using get component below
    //Don't need to store gameobejct in interaction?
    //[SerializeField] private VoidEventChannelSO _startTalking = default;


    [Header("Listening to channels")]
    [SerializeField] private VoidEventChannelSO _onInteractionEnded = default;

    private void OnEnable()
    {
        _inputReader.interactionEvent += OnInteractionButtonPress;
        _onInteractionEnded.OnEventRaised += OnInteractionEnd;
    }

    private void OnDisable()
    {
        _inputReader.interactionEvent -= OnInteractionButtonPress;
        _onInteractionEnded.OnEventRaised -= OnInteractionEnd;
    }

    private void OnInteractionButtonPress()
    {
        if (_potentialInteractions.Count == 0)
            return;

        currentInteractionType = _potentialInteractions.First.Value.type;

        if(currentInteractionType == InteractionType.Talk)
        {
            //TODO: Use Event SO Channel instead of grabbing component
            _potentialInteractions.First.Value.interactableObject.GetComponent<NPCDialogueController>().InteractWithCharacter();
            //_inputReader.EnableDialogueInput();
        }
    }

  
    /// <summary>
    /// Called by the Event on the trigger collider on child GameObject named "InteractionDetector"
    /// Example Event <see cref="ZoneTriggerController"/>
    /// </summary>
    /// <param name="isWithin"></param>
    /// <param name="obj"></param>
    public void OnTriggerChangeDetected(bool isWithin, GameObject obj)
    {
        if (isWithin)
            AddPotentialInteraction(obj);
        else
            RemovePotentialInteraction(obj);
            
    }

    private void AddPotentialInteraction(GameObject obj)
    {
        Interaction newPotentialInteraction = new Interaction(InteractionType.None, obj);

        if(obj.CompareTag("NPC"))
        {
            newPotentialInteraction.type = InteractionType.Talk;
        }

        if(newPotentialInteraction.type != InteractionType.None)
        {
            _potentialInteractions.AddFirst(newPotentialInteraction);
            RequestUIUpdate(true);
        }
    }

    private void RemovePotentialInteraction(GameObject obj)
    {
        LinkedListNode<Interaction> currentNode = _potentialInteractions.First;

        //Loop through LinkedList until object is found and removed
        while(currentNode != null)
        {
            if(currentNode.Value.interactableObject == obj)
            {
                _potentialInteractions.Remove(currentNode);
                break;
            }
            currentNode = currentNode.Next;
        }

        //Toggle UI depending on if there are more interactions or not 
        RequestUIUpdate(_potentialInteractions.Count > 0);
    }
    private void RequestUIUpdate(bool isVisible)
    {
        if (isVisible)
            _toggleInteractionUI.RaiseEvent(true, _potentialInteractions.First.Value.type);
        else
            _toggleInteractionUI.RaiseEvent(false, InteractionType.None);
    }
    private void OnInteractionEnd()
    {
        if (currentInteractionType == InteractionType.Talk)
            //Show UI in case player wants to interact again 
            RequestUIUpdate(true);

        _inputReader.EnableGameplayInput();

    }

}
