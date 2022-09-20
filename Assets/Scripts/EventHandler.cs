using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class EventHandler : MonoBehaviour
{
    public string debugText;
    public InputActionPhase phase;
    public UnityEvent filteredEvent;


    public void Trigger(InputAction.CallbackContext context)
    {
        if (context.phase == phase)
        {
            //context.action.PerformInteractiveRebinding()
            //    .WithControlsExcluding("Mouse")
            //    .OnMatchWaitForAnother(0.1f)
            //    .Start();

            filteredEvent.Invoke();
        }
    }
}
