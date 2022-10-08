using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Unity.VisualScripting;

public class EventHandlerButton : MonoBehaviour
{
    public InputActionPhase initialPhase = InputActionPhase.Performed;
    public InputActionPhase endPhase = InputActionPhase.Canceled;
    public GameObject targetOfEvent;

    public void Trigger(InputAction.CallbackContext context)
    {
        if (context.phase == initialPhase || context.phase == endPhase)
        {
            //context.action.PerformInteractiveRebinding()
            //    .WithControlsExcluding("Mouse")
            //    .OnMatchWaitForAnother(0.1f)
            //    .Start();
            
            EventBus.Trigger(JumpInputEventUnit.EventHook, targetOfEvent, context.phase == initialPhase);
        }
    }
}
