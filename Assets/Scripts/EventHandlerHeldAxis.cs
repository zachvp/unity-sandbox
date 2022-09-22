using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Collections;

public class EventHandlerHeldAxis : MonoBehaviour
{
    public InputActionPhase initialPhase = InputActionPhase.Performed;
    public InputActionPhase endPhase = InputActionPhase.Canceled;
    public UnityEvent<Vector2> filteredEvent;

    private InputActionPhase currentPhase;
    private Vector2 value;

    public void Trigger(InputAction.CallbackContext context)
    {
        currentPhase = context.phase;

        if (context.phase == initialPhase)
        {
            value = context.ReadValue<Vector2>();

            StartCoroutine(CoroutineHold());
        }
        else if (context.phase == endPhase)
        {
            value = context.ReadValue<Vector2>();

            filteredEvent.Invoke(value);
        }
    }

    public IEnumerator CoroutineHold()
    {
        while (currentPhase != endPhase)
        {
            filteredEvent.Invoke(value);
            yield return null;
        }
    }
}
