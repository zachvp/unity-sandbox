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
}

public static class CoreConstants
{
    public const float FLOAT_DEADZONE = 0.01f;
    public const float VELOCITY_DEADZONE = 2;
}
