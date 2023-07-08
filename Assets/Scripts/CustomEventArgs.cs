using UnityEngine;
using System;
using UnityEngine.InputSystem;

public struct InputButtonArgs
{
    public int playerID;
    public InputActionPhase phase;
    public CoreActionMapPlayer action;
}

[Serializable]
public struct InputAxis1DArgs
{
    public int playerID;
    public CoreActionMapPlayer action;
    public int axis;
}

[Serializable]
public struct InputAxis2DArgs
{
    public int playerID;
    public CoreActionMapPlayer action;
    public Vector2 axis;
}

[Serializable]
public struct PCInputArgs
{
    public CoreActionMapPlayer type;
    public int playerIndex;
    public MultiValue value;
}
