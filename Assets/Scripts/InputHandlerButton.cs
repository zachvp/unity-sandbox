using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Unity.VisualScripting;

public class InputHandlerButton : MonoBehaviour
{
    public CustomInputAction action;
    public PlayerInput input;
    public InputButtonArgs args;
    public UnityAction<InputButtonArgs> actionDelegate;

    public void Trigger(InputAction.CallbackContext context)
    {
        args.action = action;
        args.playerID = (short) input.playerIndex;
        args.phase = context.phase;

        EventBus.Trigger(InputButtonEvent.Hook, args);
        if (actionDelegate != null)
        {
            actionDelegate(args);
        }
    }
}
