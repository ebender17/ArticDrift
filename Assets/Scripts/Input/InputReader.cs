using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReaderSO", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions
{
    //Assign with delete {} to initialize with empty delegate 
    // to skip null check when we used them 

    //Gameplay 
    public event UnityAction<Vector2> moveEvent = delegate { };
    public event UnityAction jumpEvent = delegate { };
    public event UnityAction jumpCanceledEvent = delegate { };

    private GameInput _gameInput;

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

    
}
