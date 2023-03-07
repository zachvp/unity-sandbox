using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;

// todo: rename to InputHandlerHeldAxis
public class EventHandlerHeldAxis : MonoBehaviour
{
    // todo: remove
    public InputActionPhase currentPhase;
    public short axis;

    public PlayerInput playerInput;
    public InputAxis1DArgs args;
    public CustomInputAction action;

    public void Trigger(InputAction.CallbackContext context)
    {
        // todo: remove
        currentPhase = context.phase;
        axis = (short)context.ReadValue<float>();

        if (context.phase == InputActionPhase.Started)
        {
            StartCoroutine(CoroutineHold());
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            EventBus.Trigger(MoveInputEventUnit.Hook, axis);
        }
        // end remove

        args.playerID = (short) playerInput.playerIndex;
        args.axis = (short) context.ReadValue<float>();
        args.phase = context.phase;
        args.action = action;
        EventBus.Trigger(InputAxis1DEvent.Hook, args);
    }

    public IEnumerator CoroutineHold()
    {
        while (currentPhase != InputActionPhase.Canceled)
        {
            EventBus.Trigger(MoveInputEventUnit.Hook, axis); // todo: remove
            EventBus.Trigger(InputAxis1DEvent.Hook, args);

            yield return null;
        }
    }
}
