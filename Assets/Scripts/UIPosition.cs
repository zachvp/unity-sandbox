using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPosition : MonoBehaviour
{
    public Vector3 worldPositionRead;
    public Vector3 worldPositionWrite;

    void Awake()
    {
        worldPositionWrite = transform.position;
    }

    void Update()
    {
        transform.position = worldPositionWrite;
        worldPositionRead = transform.position;
    }
}
