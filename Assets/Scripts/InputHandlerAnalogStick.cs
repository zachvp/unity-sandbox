using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputHandlerAnalogStick : MonoBehaviour
{
    public PlayerInput input;
    public CustomInputAction action;
    public InputAxis2DArgs args;
    public UnityAction<InputAxis2DArgs> actionDelegate;

    public void Trigger(InputAction.CallbackContext context)
    {
        args.playerID = (short) input.playerIndex;
        args.action = action;
        args.axis = context.ReadValue<Vector2>();

        if (actionDelegate != null)
        {
            actionDelegate(args);
        }
    }
}
