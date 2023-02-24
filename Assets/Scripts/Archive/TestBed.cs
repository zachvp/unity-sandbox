using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

// reference code:
// context.action.PerformInteractiveRebinding()
//    .WithControlsExcluding("Mouse")
//    .OnMatchWaitForAnother(0.1f)
//    .Start();

public class TestBed : MonoBehaviour
{
    public void Awake()
    {
        Debug.LogFormat("TESTBED present in scene");
        EventBus.Register<bool>(JumpInputEventUnit.EventHook, Trigger);
        EventBus.Register<EmptyEventArgs>(OnCustomInputTrigger.Hook, (EmptyEventArgs a) => { Debug.Log("handle global event in script"); });
        EventBus.Register<bool>(OnCustomInputTriggerArgs.Hook, (bool b) => { Debug.Log($"handle global arg event in script: {b}"); });

        // EventBus.Trigger(EnumHelper.GetStringID(hook), context.phase == initialPhase);
        //Timer t;
    }

    public void Trigger(bool t)
    {
        Debug.Log($"jump triggered: {t}");
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
