using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place on NPC Actors for Dialogue. 
/// </summary>
public class NPCDialogueController : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private ActorSO _actor = default;
    [SerializeField] private DialogueDataSO _dialogue = default;

    //TODO: Call InteractWithCharacter by listening to event instead of being called from interaction manager
    //[Header("Listening to channels")]
    //[SerializeField] VoidEventChannelSO _interactionEvent = default;

    [Header("Broadcasting on channels")]
    [SerializeField] private DialogueDataChannelSO _startDialogueEvent = default;
   

    public void InteractWithCharacter()
    {
        if(_dialogue != null)
        {
            StartDialogue();
            
        }
    }

    private void StartDialogue()
    {
        if (_startDialogueEvent != null)
        {
            _startDialogueEvent.RaiseEvent(_dialogue);
        }
    }
}
