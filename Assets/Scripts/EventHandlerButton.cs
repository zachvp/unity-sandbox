using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class EventHandlerButton : MonoBehaviour
{
    public InputActionPhase initialPhase = InputActionPhase.Performed;
    public InputActionPhase endPhase = InputActionPhase.Canceled;
    public UnityEvent filteredEvent;
    public DataBool data;

    public void Trigger(InputAction.CallbackContext context)
    {
        if (context.phase == initialPhase || context.phase == endPhase)
        {
            //context.action.PerformInteractiveRebinding()
            //    .WithControlsExcluding("Mouse")
            //    .OnMatchWaitForAnother(0.1f)
            //    .Start();

            data.data = context.phase == initialPhase;
            filteredEvent.Invoke();
        }
    }
}
