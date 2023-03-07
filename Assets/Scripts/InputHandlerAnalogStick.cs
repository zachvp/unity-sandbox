using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputHandlerAnalogStick : MonoBehaviour
{
    public CustomInputAction action;
    public PlayerInput input;
    public InputAxis2DArgs args;
    public UnityAction<InputAxis2DArgs> actionDelegate;

    public void Trigger(InputAction.CallbackContext context)
    {
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
