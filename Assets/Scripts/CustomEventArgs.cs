using UnityEngine;
using System;
using UnityEngine.InputSystem;

public struct InputButtonArgs
{
    public int playerID;
    public InputActionPhase phase;
    public CustomInputAction action;
}

[Serializable]
public struct InputAxis1DArgs
{
    public int playerID;
    public CustomInputAction action;
    public int axis;
}

[Serializable]
public struct InputAxis2DArgs
{
    public int playerID;
    public CustomInputAction action;
    public Vector2 axis;
}
