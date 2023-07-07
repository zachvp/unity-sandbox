using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Buffer<T>
{
    public T[] values;
    public int index;
    public float interval; // in seconds

    public void Store(T s)
    {
        values[index] = s;
        index = (index + 1) % values.Length;
    }
}

public static class CoreUtilities
{
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
