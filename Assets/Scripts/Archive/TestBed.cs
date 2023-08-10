using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

// reference code:
// context.action.PerformInteractiveRebinding()
//    .WithControlsExcluding("Mouse")
//    .OnMatchWaitForAnother(0.1f)
//    .Start();

public class TestBed : MonoBehaviour
{
    public PlayerInputManager manager;

    public void Awake()
    {
        Debug.LogFormat("TESTBED present in scene");
        //EventBus.Register<bool>(JumpInputEventUnit.EventHook, Trigger);
        //EventBus.Register<EmptyEventArgs>(OnCustomInputTrigger.Hook, (EmptyEventArgs a) => { Debug.Log("handle global event in script"); });
        //EventBus.Register<bool>(OnCustomInputTriggerArgs.Hook, (bool b) => { Debug.Log($"handle global arg event in script: {b}"); });

        // EventBus.Trigger(EnumHelper.GetStringID(hook), context.phase == initialPhase);
        //Timer t;

        
    }

    public void Trigger(bool t)
    {
        Debug.Log($"jump triggered: {t}");
    }

    public void OnPlayerJoin(PlayerInput i)
    {
        Debug.Log($"onplayerjoin: {i.playerIndex}");
    }

    public void OnPlayerLeave(PlayerInput i)
    {
        Debug.Log($"onplayerleave: {i.playerIndex}");
    }

    public void OnControlsChanged(PlayerInput i)
    {
        Debug.Log($"onpcontrols changed: {i.playerIndex}");
    }

    public void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
