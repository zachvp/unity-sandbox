using UnityEngine;
using System;

public enum CustomHook
{
    JumpInput,
    MoveInput,
    GestureInput,
    GripInput
}

public static class EnumHelper
{
    public static string GetStringID(CustomHook hook)
    {
        return ((int) hook).ToString();
    }
}
