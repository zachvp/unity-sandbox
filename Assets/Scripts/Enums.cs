using System;
using System.Collections.Generic;

public enum CustomHook : Int32
{
    INPUT_BUTTON,
    INPUT_AXIS1D,
    INPUT_AXIS2D,

    COMMAND
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

[Flags]
public enum PlatformState
{
    NONE = 0,
    JUMP = 1 << 0,
    MOVE = 1 << 1,
    MOVE_NEUTRAL = 1 << 2,
    WALL_JUMP = 1 << 3,
    WALL_JUMPING = 1 << 4,
    WALL_CLING = 1 << 5,
    WALL_RELEASE = 1 << 6,
}

[Flags]
public enum HandState
{
    NONE = 0,
    GRIP = 1 << 0,
    BLOCKED = 1 << 1
}

[Flags]
public enum Trait
{
    NONE = 0,
    PICKUP = 1 << 0
}

public enum CoreActionMap
{
    NONE,
    PLAYER
}

public enum CoreActionMapPlayer : Int32
{
    NONE,

    START,
    JUMP,
    MOVE,
    MOVE_HAND,
    GRIP,
    THROW
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

    public static CoreActionMapPlayer GetPlayerAction(string name)
    {
        var result = CoreActionMapPlayer.NONE;
        var map = new Dictionary<string, CoreActionMapPlayer>
        {
            { "start", CoreActionMapPlayer.START },
            { "jump", CoreActionMapPlayer.JUMP },
            { "move", CoreActionMapPlayer.MOVE },
            { "move hand", CoreActionMapPlayer.MOVE_HAND },
            { "grip", CoreActionMapPlayer.GRIP },
            { "throw", CoreActionMapPlayer.THROW },
        };

        if (map.ContainsKey(name.ToLower()))
        {
            result = map[name.ToLower()];
        }

        return result;
    }

    public static CoreActionMap GetActionMap(string name)
    {
        var result = CoreActionMap.NONE;
        var map = new Dictionary<string, CoreActionMap>
        {
            { "player", CoreActionMap.PLAYER },
        };

        if (map.ContainsKey(name.ToLower()))
        {
            result = map[name.ToLower()];
        }

        return result;
    }
}
