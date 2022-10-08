using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class Emitter : MonoBehaviour
{
    public UnityEvent testEvent;

    public void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            testEvent.Invoke();

            Debug.LogFormat("zvp: trigger test 0");
            EventBus.Trigger(Test_0.EventHook, gameObject, (short) 3);
        }
    }
}
