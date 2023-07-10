using UnityEngine;
using UnityEngine.InputSystem;

public class OnPlayerJoinEvent : MonoBehaviour
{
    public GameObject PlayerInputObjectTemplate;

    public void Start()
    {
        PlayerInputManager.instance.onPlayerJoined += HandlePlayerJoin;
        PlayerInputManager.instance.onPlayerLeft += HandlePlayerLeave;
    }

    public void HandlePlayerJoin(PlayerInput playerInput)
    {
        Debug.LogFormat($"player joined: {playerInput.playerIndex}");
        //var newHandler = Instantiate(commandHandler);

        // todo: attach to player input object
        var newHandler = gameObject.AddComponent<TestCommandHandler>();
        newHandler.Initialize(playerInput.playerIndex);
    }

    public void HandlePlayerLeave(PlayerInput playerInput)
    {
        // this is called at least when
        // application closes (observed when unity player stopped in editor)
        Debug.LogWarning($"player left: {playerInput.playerIndex}; unknown what led to this");
    }
}
