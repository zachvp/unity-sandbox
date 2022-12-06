using System;

// todo: separate into separate generic buffer class
//[Flags]
//public enum State
//{
//    NONE = 0,
//    LHS_BLOCKED = 1,
//    RHS_BLOCKED = 1 << 1,
//    GROUNDED = 1 << 2
//}

//public short bufferSize;
//public State[] states;

//private short index;
// end todo

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
