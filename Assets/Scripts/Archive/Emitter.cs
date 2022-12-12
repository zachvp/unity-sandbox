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

    public void Awake()
    {
        
    }

    public void Update()
    {
        // todo: look into this function
        //Mathf.SmoothDamp()

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            
        }
    }
}
