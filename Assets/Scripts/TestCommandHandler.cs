using UnityEngine;
using Unity.VisualScripting;

public class TestCommandHandler : MonoBehaviour
{
    public void Awake()
    {
        EventBus.Register<PCInputArgs>(CommandEvent.Hook, HandleCommand);
    }

    public void HandleCommand(PCInputArgs command)
    {
        Debug.Log($"handle command: {command.type} | {command.value}");
    }
}
