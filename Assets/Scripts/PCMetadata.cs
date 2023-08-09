using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCMetadata : MonoBehaviour
{
    // 1-time write vars
    public int playerID;
    public PCInputCommandEmitter commandEmitter;

    public void Start()
    {
        PCIDRegistry.Instance.Register(this, (id) =>
        {
            playerID = id;

            commandEmitter = SceneRefs.Instance.pcCommandEmitters[playerID];
            Debug.Log($"pc assigned id: {id}");
        });
    }
}
