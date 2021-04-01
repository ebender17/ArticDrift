using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReaderSO", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IDialogueActions
{
    //Assign with delete {} to initialize with empty delegate 
    // to skip null check when we used them 

    //Gameplay 
    public event UnityAction<Vector2> moveEvent = delegate { };
    public event UnityAction jumpEvent = delegate { };
    public event UnityAction jumpCanceledEvent = delegate { };
    public event UnityAction interactionEvent = delegate { };

    //Dialogue
    public event UnityAction advanceDialogueEvent = delegate { };

    private GameInput _gameInput;

    private void OnEnable()
    {
        if(_gameInput == null)
        {
            _gameInput = new GameInput();
            _gameInput.Gameplay.SetCallbacks(this);
            _gameInput.Dialogue.SetCallbacks(this);
        }

        EnableGameplayInput();
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            jumpEvent.Invoke();
        if (context.phase == InputActionPhase.Canceled)
            jumpCanceledEvent.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveEvent.Invoke(context.ReadValue<Vector2>());
    }

    //Used by Cinemachine
    public void OnRotateCamera(InputAction.CallbackContext context)
    {
       
    }
    public void OnAdvanceDialogue(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {

            advanceDialogueEvent();
        }
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            interactionEvent.Invoke();
            
    }

    public void EnableGameplayInput()
    {
        _gameInput.Gameplay.Enable();

        _gameInput.Dialogue.Disable();
    }

    public void EnableDialogueInput()
    {
        _gameInput.Dialogue.Enable();

        _gameInput.Gameplay.Disable();
    }

    public void DisableAllInput()
    {
        _gameInput.Gameplay.Disable();
        _gameInput.Dialogue.Disable();
    }

}
