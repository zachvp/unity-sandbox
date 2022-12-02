using System;

public enum CustomHook
{
    JumpInput,
    MoveInput,
    GestureInput,
    GripInput,
    ThrowInput
}

[Flags]
public enum UnityProperties
{
    NONE = 0,
    ACTIVATION = 1 << 0,
    POSITION = 1 << 1,
    VELOCITY = 1 << 2
}

[Flags]
public enum Direction2D
{
    NONE = 0,
    UP = 1 << 0,
    DOWN = 1 << 1,
    LEFT = 1 << 2,
    RIGHT = 1 << 3
}

public static class EnumHelper
{
    public static string GetStringID(Enum hook)
    {
        var hookCast = Convert.ToInt32(hook);

        return hookCast.ToString();
    }

    public static bool ContainsFlags(Enum mask, Enum flags)
    {
        var maskCast = Convert.ToInt32(mask);
        var flagsCast = Convert.ToInt32(flags);

        return (maskCast | flagsCast) == maskCast;
    }

    public static Direction2D FromBool(bool left, bool right, bool down, bool up)
    {
        var result = Direction2D.NONE;

        result |= left ? Direction2D.LEFT : Direction2D.NONE;
        result |= right ? Direction2D.RIGHT : Direction2D.NONE;
        result |= down ? Direction2D.DOWN : Direction2D.NONE;
        result |= up ? Direction2D.UP : Direction2D.NONE;

        return result;
    }
}
