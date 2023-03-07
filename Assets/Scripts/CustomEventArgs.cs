using UnityEngine;
using UnityEngine.InputSystem;

public struct InputButtonArgs
{
    public short playerID;
    public InputActionPhase phase;
    public CustomInputAction action;
}

public struct InputAxis1DArgs
{
    public short playerID;
    public InputActionPhase phase;
    public CustomInputAction action;
    public short axis;
}

public struct InputAxis2DArgs
{
    public short playerID;
    public InputActionPhase phase;
    public CustomInputAction action;
    public Vector2 axis;
}
