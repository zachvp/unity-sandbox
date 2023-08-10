using UnityEngine;

// todo: associate command handler with a particular player controller
public class TestCommandHandler : MonoBehaviour
{
    public int playerIndex;

    public void Awake()
    {
        Events.instance.onPCCommand += HandleCommand;
    }

    public void Initialize(int playerIdx)
    {
        playerIndex = playerIdx;
    }

    public void HandleCommand(PCInputArgs command)
    {
        if (command.playerIndex == playerIndex)
        {
            Debug.Log($"handle command: {command}");
        }
    }
}
