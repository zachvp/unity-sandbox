using UnityEngine;
using UnityEngine.InputSystem;

public class EventHandler : MonoBehaviour
{
    public string debugText;
    public InputActionPhase phase;

    public void Trigger(InputAction.CallbackContext context)
    {
        if (context.phase == phase)
        {
            //context.action.PerformInteractiveRebinding()
            //    .WithControlsExcluding("Mouse")
            //    .OnMatchWaitForAnother(0.1f)
            //    .Start();

            Debug.Log(debugText);
        }
    }
}
