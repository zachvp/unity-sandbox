using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCSpawnContainer : MonoBehaviour
{
    [Tooltip("The object templates to spawn")]
    public GameObject[] spawnObjects;

    public void Start()
    {
        foreach (var spawnObject in spawnObjects)
        {
            Instantiate(spawnObject);
        }
    }
}
