using System;
using UnityEngine;

public class PCMetadata : MonoBehaviour
{
    // 1-time write vars
    public int playerID;
    public PCInputCommandEmitter commandEmitter;
    public Action onInitialized;

    public void OnEnable()
    {
        Events.instance.onPCCommandEmitterSpawn += HandlePCCommandEmitterSpawn;
    }

    public void Start()
    {
        PCIDRegistry.Instance.Register(this, (id) =>
        {
            playerID = id;

            Debug.Log($"pc assigned id: {id}");
        });
    }

    public void HandlePCCommandEmitterSpawn(PCInputCommandEmitter emitter)
    {
        if (commandEmitter == null)
        {
            commandEmitter = emitter;
        }

        Notifier.Send(onInitialized);
    }
}
