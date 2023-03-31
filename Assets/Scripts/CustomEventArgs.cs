using UnityEngine;
using System;
using UnityEngine.InputSystem;

public struct InputButtonArgs
{
    public short playerID;
    public InputActionPhase phase;
    public CustomInputAction action;
}

[Serializable]
public struct InputAxis1DArgs
{
    public short playerID;
    public CustomInputAction action;
    public short axis;
}

[Serializable]
public struct InputAxis2DArgs
{
    public short playerID;
    public CustomInputAction action;
    public Vector2 axis;
}
