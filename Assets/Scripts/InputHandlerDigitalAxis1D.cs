using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;

public class InputHandlerDigitalAxis1D : MonoBehaviour
{
    public PlayerInput playerInput;
    public InputAxis1DArgs args;
    public CustomInputAction action;

    public void Trigger(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            StartCoroutine(CoroutineHold());
        }

        args.playerID = (short) playerInput.playerIndex;
        args.axis = (short) context.ReadValue<float>();
        args.phase = context.phase;
        args.action = action;
        EventBus.Trigger(InputAxis1DEvent.Hook, args);
    }

    public IEnumerator CoroutineHold()
    {
        while (args.phase != InputActionPhase.Canceled)
        {
            EventBus.Trigger(InputAxis1DEvent.Hook, args);

            yield return null;
        }
    }
}
