using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Collections;

public class EventHandlerHeldAxis : MonoBehaviour
{
    public InputActionPhase initialPhase = InputActionPhase.Performed;
    public InputActionPhase endPhase = InputActionPhase.Canceled;
    public UnityEvent<float> filteredEvent;

    private InputActionPhase currentPhase;
    private float value;

    public void Trigger(InputAction.CallbackContext context)
    {
        currentPhase = context.phase;

        if (context.phase == initialPhase)
        {
            value = context.ReadValue<float>();

            Debug.LogFormat("zvp: value: {0}", value);

            StartCoroutine(CoroutineHold());
        }
        else if (context.phase == endPhase)
        {
            value = context.ReadValue<float>();

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
