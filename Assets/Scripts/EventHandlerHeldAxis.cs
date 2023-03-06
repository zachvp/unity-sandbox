using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Collections;
using Unity.VisualScripting;

// todo: rename to InputHandlerHeldAxis
public class EventHandlerHeldAxis : MonoBehaviour
{
    public InputActionPhase currentPhase;
    public short axis;

    public void Trigger(InputAction.CallbackContext context)
    {
        currentPhase = context.phase;
        axis = (short)context.ReadValue<float>();

        if (context.phase == InputActionPhase.Started)
        {
            axis = (short)context.ReadValue<float>();

            StartCoroutine(CoroutineHold());
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            axis = (short)context.ReadValue<float>();

            // todo: remove game object call
            EventBus.Trigger(MoveInputEventUnit.Hook, gameObject, axis);
            EventBus.Trigger(MoveInputEventUnit.Hook, axis);
        }
    }

    public IEnumerator CoroutineHold()
    {
        while (currentPhase != InputActionPhase.Canceled)
        {
            // todo: remove game object call
            EventBus.Trigger(MoveInputEventUnit.Hook, gameObject, axis);
            EventBus.Trigger(MoveInputEventUnit.Hook, axis);

            yield return null;
        }
    }
}
