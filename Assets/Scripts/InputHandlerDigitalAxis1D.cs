using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class InputHandlerDigitalAxis1D : MonoBehaviour
{
    public PlayerInput playerInput;
    public CoreCommand action;
    public InputAxis1DArgs args;

    public void Trigger(InputAction.CallbackContext context)
    {
        args.playerID = playerInput.playerIndex;
        args.axis = Mathf.RoundToInt(context.ReadValue<float>());
        
        args.action = action;
        EventBus.Trigger(InputAxis1DEvent.Hook, args);
    }
}
