using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCMetadata : MonoBehaviour
{
    // 1-time write vars
    public int playerID;

    public void Start()
    {
        PCIDRegistry.Instance.Register(this, (id) =>
        {
            playerID = id;
            Debug.Log($"pc assigned id: {id}");
        });
    }
}
