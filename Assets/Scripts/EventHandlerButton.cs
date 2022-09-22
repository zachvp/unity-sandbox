using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class EventHandlerButton : MonoBehaviour
{
    public InputActionPhase initialPhase = InputActionPhase.Performed;
    public InputActionPhase endPhase = InputActionPhase.Canceled;
    public UnityEvent<bool> filteredEvent;

    public void Trigger(InputAction.CallbackContext context)
    {
        if (context.phase == initialPhase)
        {
            Debug.LogFormat("zvp: button phase: {0}", context.phase);

            //context.action.PerformInteractiveRebinding()
            //    .WithControlsExcluding("Mouse")
            //    .OnMatchWaitForAnother(0.1f)
            //    .Start();
            filteredEvent.Invoke(true);
        }
    }
}
