using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Buffer<T>
{
    public T[] values;
    public short index;
    public float interval; // in seconds

    public void Store(T s)
    {
        values[index] = s;
        index = (short) (((short) (index + 1)) % values.Length);
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
}
