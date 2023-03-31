using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;

public class InputHandlerDigitalAxis1D : MonoBehaviour
{
    public PlayerInput playerInput;
    public CustomInputAction action;
    public InputAxis1DArgs args;

    public void Trigger(InputAction.CallbackContext context)
    {
        args.playerID = (short) playerInput.playerIndex;
        args.axis = (short) Mathf.RoundToInt(context.ReadValue<float>());
        
        args.action = action;
        EventBus.Trigger(InputAxis1DEvent.Hook, args);
    }
}
