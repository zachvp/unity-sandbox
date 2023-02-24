using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Unity.VisualScripting;

public class EventHandlerButton : MonoBehaviour
{
    public InputActionPhase initialPhase = InputActionPhase.Performed;
    public InputActionPhase endPhase = InputActionPhase.Canceled;
    public CustomHook hook;

    public void Trigger(InputAction.CallbackContext context)
    {
        if (context.phase == initialPhase || context.phase == endPhase)
        {
            EventBus.Trigger(EnumHelper.GetStringID(hook), gameObject, context.phase == initialPhase);
            EventBus.Trigger(EnumHelper.GetStringID(hook), context.phase == initialPhase);

            if (context.phase == initialPhase)
            {
                EventBus.Trigger(OnCustomInputTrigger.Hook);
            }
        }
    }
}
