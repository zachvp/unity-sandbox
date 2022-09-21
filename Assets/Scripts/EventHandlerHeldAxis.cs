using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Collections;

public class EventHandlerHeldAxis : MonoBehaviour
{
    public InputActionPhase initialPhase = InputActionPhase.Performed;
    public InputActionPhase endPhase = InputActionPhase.Canceled;
    public UnityEvent filteredEvent;
    public DataVector2 axis;

    private InputActionPhase currentPhase;

    public void Trigger(InputAction.CallbackContext context)
    {
        currentPhase = context.phase;

        if (context.phase == initialPhase)
        {
            axis.data = context.ReadValue<Vector2>();

            StartCoroutine(CoroutineHold());
        }
        else if (context.phase == endPhase)
        {
            axis.data = context.ReadValue<Vector2>();

            filteredEvent.Invoke();
        }
    }

    public IEnumerator CoroutineHold()
    {
        while (currentPhase != endPhase)
        {
            filteredEvent.Invoke();
            yield return null;
        }
    }
}
