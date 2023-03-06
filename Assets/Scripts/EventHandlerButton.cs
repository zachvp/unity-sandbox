using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Unity.VisualScripting;

public class EventHandlerButton : MonoBehaviour
{
    public InputActionPhase initialPhase = InputActionPhase.Performed;
    public InputActionPhase endPhase = InputActionPhase.Canceled;
    public CustomHook hook;
    public PlayerInput input;

    public void Awake()
    {
        // todo: this is a temp fix for NREs; move to use only this or only member outlets
        input = GetComponent<PlayerInput>();
    }

    public void Trigger(InputAction.CallbackContext context)
    {
        if (context.phase == initialPhase || context.phase == endPhase)
        {
            // todo: remove game object call
            EventBus.Trigger(EnumHelper.GetStringID(hook), gameObject, context.phase == initialPhase);
            EventBus.Trigger(EnumHelper.GetStringID(hook), context.phase == initialPhase);

            //if (context.phase == initialPhase)
            //{
            //    EventBus.Trigger(OnCustomInputTrigger.Hook);
            //    EventBus.Trigger(OnCustomInputTriggerArgs.Hook, true);
            //    Debug.Log($"button handler input id: {input.playerIndex}");
            //}
        }
    }
}
