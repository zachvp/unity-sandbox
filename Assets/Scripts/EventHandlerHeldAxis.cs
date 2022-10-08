using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Collections;
using Unity.VisualScripting;

// todo: rename to InputHandlerHeldAxis
public class EventHandlerHeldAxis : MonoBehaviour
{
    public InputActionPhase initialPhase = InputActionPhase.Performed;
    public InputActionPhase endPhase = InputActionPhase.Canceled;
    public GameObject targetOfEvent;

    private InputActionPhase currentPhase;
    private short axis;


    public void Trigger(InputAction.CallbackContext context)
    {
        currentPhase = context.phase;

        if (context.phase == initialPhase)
        {
            axis = (short) context.ReadValue<float>();

            StartCoroutine(CoroutineHold());
        }
        else if (context.phase == endPhase)
        {
            axis = (short) context.ReadValue<float>();

            EventBus.Trigger(MoveInputEventUnit.EventHook, targetOfEvent, axis);
        }
    }

    public IEnumerator CoroutineHold()
    {
        while (currentPhase != endPhase)
        {
            EventBus.Trigger(MoveInputEventUnit.EventHook, targetOfEvent, axis);

            yield return null;
        }
    }
}
