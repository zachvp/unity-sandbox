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

public abstract class CoreCommand
{
    public CustomInputAction type;
}

public abstract class CoreCommand<T>
{
    public CustomInputAction command;
    public T data0;

    public CoreCommand(T in0)
    {
        data0 = in0;
    }
}

public class JumpCommand : CoreCommand
{
    public JumpCommand()
    {
        type = CustomInputAction.JUMP;
    }
}

public interface ICommandProcessor
{
    public void Process(CoreCommand command);
}

public interface ICommandProcessor<T>
{
    public void Process(CoreCommand<T> command);
}
