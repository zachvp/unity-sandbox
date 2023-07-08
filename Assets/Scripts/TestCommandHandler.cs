using UnityEngine;
using Unity.VisualScripting;

public class TestCommandHandler : MonoBehaviour
{
    public void Awake()
    {
        EventBus.Register<CoreActionMapPlayer>(CommandEvent.Hook, HandleCommand);
    }

    public void HandleCommand(CoreActionMapPlayer command)
    {
        Debug.Log($"handle command: {command}");
    }
}
