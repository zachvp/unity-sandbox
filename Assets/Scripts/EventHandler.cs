using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class EventHandler : MonoBehaviour
{
    public string debugText;
    public InputActionPhase phase;
    public UnityEvent filteredEvent;
    public DataVector2 digitalAxis0;


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

    public void ApplyVector2(InputAction.CallbackContext context)
    {
        //Debug.LogFormat("zvp: apply vector2: {0}", context.ReadValue<Vector2>());

        this.digitalAxis0.data = context.ReadValue<Vector2>();

        filteredEvent.Invoke();
    }
}
