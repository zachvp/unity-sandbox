using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCMetadata : MonoBehaviour
{
    // 1-time write vars
    public int playerID;
    public PCInputCommandEmitter commandEmitter;

    public void OnEnable()
    {
        Notifications.onPCCommandEmitterSpawn += HandlePCCommandEmitterSpawn;
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
        commandEmitter = SceneRefs.Instance.pcCommandEmitters[playerID];
    }
}
