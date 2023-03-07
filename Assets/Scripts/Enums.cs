using System;

public enum CustomHook : Int32
{
    INPUT_BUTTON,
    INPUT_AXIS1D,
    JumpInput,
    MoveInput,
    // todo: rename to 2daxisinput
    GestureInput,
    GripInput,
    ThrowInput,

    Jump,
    Move,
    WallCling
}

public enum CustomInputAction : Int32
{
    JUMP,
    MOVE,
    GESTURE,
    GRIP,
    THROW
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
