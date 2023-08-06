using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoreUtilities
{
    public static IEnumerator RunTask(Action task)
    {
        while (true)
        {
            task();
            yield return null;
        }
    }

    public static IEnumerator RepeatTask(float interval, Action task)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            task();
            yield return null;
        }
    }

    public static IEnumerator PostFixedUpdateTask(Action task)
    {
        yield return new WaitForFixedUpdate();
        task();
        yield return null;
    }

    public static IEnumerator DelayedTask(float delay, Action task)
    {
        yield return new WaitForSeconds(delay);
        task();
        yield return null;
    }

    // rounds given number to closest multiple of unit
    public static float RoundTo(float num, float unit)
    {
        return Mathf.Round(num / unit) * unit;
    }
}

public static class CoreConstants
{
    public const float DEADZONE_FLOAT = 0.01f;
    public const float DEADZONE_VELOCITY = 2;
    public const float UNIT_ROUND_POSITION = 1f / 16f;
    public const float THRESHOLD_DOT = 0.84f;
}

public static class Notifier
{
    public static void Send(Action handler)
    {
        // Temp variable for thread safety.
        var threadsafeHandler = handler;
        if (threadsafeHandler != null)
        {
            threadsafeHandler();
        }
    }

    public static void Send<T>(Action<T> eventHandler, T args)
    {
        // Temp variable for thread safety.
        var threadsafeHandler = eventHandler;
        if (threadsafeHandler != null)
        {
            threadsafeHandler(args);
        }
    }
}

public static class Notifications
{
    public static Action<PCInputArgs> CommandPC;

    public static void Reset()
    {
        CommandPC = null;
    }
}
