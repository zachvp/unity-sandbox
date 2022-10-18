using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System.Collections;

public class InputHandlerAnalogStick : MonoBehaviour
{
    public InputActionPhase currentPhase;
    public Vector2 axis;

    public void Trigger(InputAction.CallbackContext context)
    {
        currentPhase = context.phase;

        axis = context.ReadValue<Vector2>();
        EventBus.Trigger(GestureInputEventUnit.EventHook, axis);
    }
}
