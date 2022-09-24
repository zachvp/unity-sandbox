using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Collections;

// todo: rename to InputHandlerHeldAxis
public class EventHandlerHeldAxis : MonoBehaviour
{
    public InputActionPhase initialPhase = InputActionPhase.Performed;
    public InputActionPhase endPhase = InputActionPhase.Canceled;
    // todo: refactor to short
    public UnityEvent<float> filteredEvent;

    private InputActionPhase currentPhase;
    private float axis;

    public void Trigger(InputAction.CallbackContext context)
    {
        currentPhase = context.phase;

        if (context.phase == initialPhase)
        {
            axis = context.ReadValue<float>();

            StartCoroutine(CoroutineHold());
        }
        else if (context.phase == endPhase)
        {
            axis = context.ReadValue<float>();

            filteredEvent.Invoke(axis);
        }
    }

    public IEnumerator CoroutineHold()
    {
        while (currentPhase != endPhase)
        {
            filteredEvent.Invoke(axis);
            yield return null;
        }
    }
}
