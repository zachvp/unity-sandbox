using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine;

public class PCInputCommandEmitter : MonoBehaviour
{
    public PlayerInput playerInput;
    public CoreActionMap actionMapType;

    // Contains the combination of the most recent input data.
    public PCInputArgs data;

    public void OnEnable()
    {
        playerInput.onActionTriggered += HandleActionTriggered;
        data.playerIndex = playerInput.playerIndex;
    }

    public void OnDisable()
    {
        playerInput.onActionTriggered -= HandleActionTriggered;
    }

    public void HandleActionTriggered(InputAction.CallbackContext context)
    {
        if (EnumHelper.GetActionMap(context.action.actionMap.name) == actionMapType)
        {
            Debug.Log($"action triggered: {context.action.name}");
            var actionType = EnumHelper.GetPlayerAction(context.action.name);

            UpdateData(actionType, context);

            EventBus.Trigger(CommandEvent.Hook, data);
        }
    }

    public void UpdateData(CoreActionMapPlayer actionType, InputAction.CallbackContext context)
    {
        data.type = actionType;

        switch (actionType)
        {
            case CoreActionMapPlayer.JUMP:
            case CoreActionMapPlayer.GRIP:
            case CoreActionMapPlayer.START:
            case CoreActionMapPlayer.THROW:
                data.value.vBool = context.phase == InputActionPhase.Started;
                break;
            case CoreActionMapPlayer.MOVE:
                data.value.vFloat = context.ReadValue<float>();
                break;
            case CoreActionMapPlayer.MOVE_HAND:
                data.value.vVec2 = context.ReadValue<Vector2>();
                break;
            default:
                Debug.LogError($"Unhandled case: {actionType}");
                break;
        }
    }
}
