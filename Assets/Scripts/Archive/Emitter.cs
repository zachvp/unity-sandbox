using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

// reference code:
// context.action.PerformInteractiveRebinding()
//    .WithControlsExcluding("Mouse")
//    .OnMatchWaitForAnother(0.1f)
//    .Start();

public class Emitter : MonoBehaviour
{
    public UnityEvent testEvent;

    public void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            testEvent.Invoke();
        }
    }
}
