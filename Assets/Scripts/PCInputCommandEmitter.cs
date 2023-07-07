using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine;

public class PCInputCommandEmitter : MonoBehaviour
{
    public PlayerInput playerInput;

    public void OnEnable()
    {
        playerInput.onActionTriggered += HandleActionTriggered;
        Debug.Log($"playerInput.currentControlScheme: {playerInput.currentControlScheme}");
        Debug.Log($"playerInput.currentActionMap: {playerInput.currentActionMap}");
    }

    public void OnDisable()
    {
        playerInput.onActionTriggered -= HandleActionTriggered;
    }

    public void HandleActionTriggered(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //switch (context.action.)
            Debug.Log($"action triggered: {context.action.name}");
            EventBus.Trigger(CommandEvent.Hook, CoreCommand.JUMP);
        }
    }
}
