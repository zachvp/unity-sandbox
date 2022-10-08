using UnityEngine;
using System;

[Flags]
public enum AxisType
{
    X,
    Y,
    BOTH
}

public class VectorDistanceExceeded : MonoBehaviour
{
    public bool value;
    public Vector2 startingVector;
    public Vector2 previousVector;
    public AxisType type;

    public Vector2[] samples = new Vector2[4];
    public short index = 0;

    public float dbgDistance;

    public void Awake()
    {
        previousVector = startingVector;
    }

    public bool Compute(Vector2 newValue, short threshold)
    {
        dbgDistance = Vector2.SqrMagnitude(previousVector - newValue);

        value = Vector2.SqrMagnitude(previousVector - newValue) > threshold;
        previousVector = newValue;

        return value;
    }

    public bool Sample(Vector2 newValue, short threshold)
    {
        // todo: extend for other cases
        if ((type | AxisType.X) == type)
        {
            samples[index] = newValue;
        }

        Debug.LogError("zvp: case not handled");
        return false;
    }
}
