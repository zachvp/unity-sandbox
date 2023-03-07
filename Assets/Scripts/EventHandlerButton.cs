using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Unity.VisualScripting;

// todo: rename to inputhandler button
public class EventHandlerButton : MonoBehaviour
{
    // todo: remove
    public InputActionPhase initialPhase = InputActionPhase.Performed;
    public InputActionPhase endPhase = InputActionPhase.Canceled;
    public CustomHook hook = CustomHook.INPUT_BUTTON;
    // end remove

    public CustomInputAction action;
    public PlayerInput input;
    public InputButtonArgs args;

    public void Awake()
    {
        // todo: this is a temp fix for NREs; move to use only this or only member outlets
        input = GetComponent<PlayerInput>();
    }

    public void Trigger(InputAction.CallbackContext context)
    {
        args.action = action;
        args.playerID = (short) input.playerIndex;
        args.phase = context.phase;

        EventBus.Trigger(InputButtonEvent.Hook, args);

        // todo: remove after refactoring hand
        if (context.phase == initialPhase || context.phase == endPhase)
        {
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
