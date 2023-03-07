using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine.Events;

public class InputHandlerAnalogStick : MonoBehaviour
{
    public CustomInputAction action;
    public PlayerInput input;
    public InputAxis2DArgs args;
    public UnityAction<InputAxis2DArgs> actionDelegate;

    // todo: remove
    public InputActionPhase currentPhase;
    public Vector2 axis;
    // end remove

    public void Trigger(InputAction.CallbackContext context)
    {
        // todo: remove
        currentPhase = context.phase;

        axis = context.ReadValue<Vector2>();
        
        EventBus.Trigger(GestureInputEventUnit.Hook, axis);
        // end remove

        args.playerID = (short) input.playerIndex;
        args.phase = context.phase;
        args.action = action;
        args.axis = context.ReadValue<Vector2>();

        if (actionDelegate != null)
        {
            actionDelegate(args);
        }
    }
}
