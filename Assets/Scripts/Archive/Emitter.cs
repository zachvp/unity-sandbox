using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Emitter : MonoBehaviour
{
    public UnityEvent testEvent;
    //public 

    public void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            testEvent.Invoke();

            Animator blah;
            
            //blah.sta
        }
    }
}
