using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Collections;

public class EventHandler : MonoBehaviour
{
    public InputActionPhase initialPhase;
    public InputActionPhase endPhase;
    public UnityEvent filteredEvent;
    public DataVector2 digitalAxis0;

    private InputActionPhase currentPhase;


    public void Trigger(InputAction.CallbackContext context)
    {
        if (context.phase == initialPhase)
        {
            //context.action.PerformInteractiveRebinding()
            //    .WithControlsExcluding("Mouse")
            //    .OnMatchWaitForAnother(0.1f)
            //    .Start();

            filteredEvent.Invoke();
        }
    }

    public void ApplyVector2(InputAction.CallbackContext context)
    {
        currentPhase = context.phase;

        if (context.phase == initialPhase)
        {
            digitalAxis0.data = context.ReadValue<Vector2>();

            StartCoroutine(CoroutineDigitalAxis0());
        }
    }

    public IEnumerator CoroutineDigitalAxis0()
    {
        while (currentPhase != endPhase)
        {
            filteredEvent.Invoke();
            yield return null;
        }
    }
}
