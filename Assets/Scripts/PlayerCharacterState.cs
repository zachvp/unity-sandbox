using UnityEngine;
using System;

public class PlayerCharacterState : MonoBehaviour
{
    [Flags]
    public enum State
    {
        NONE = 0,
        LHS_BLOCKED = 1,
        RHS_BLOCKED = 1 << 1,
        GROUNDED = 1 << 2
    }


    public short bufferSize;
    public State[] states;

    private short index;


    public void Awake()
    {
        states = new State[bufferSize];
    }

    public bool BufferContainsState(State includeState, State excludeState)
    {
        foreach (var s in states)
        {
            if ((s & includeState) > 0 && (s & excludeState) == 0)
            {
                return true;
            }
        }

        return false;
    }

    public void Store(State s)
    {
        states[index] = s;
        index = (short) (((short) (index + 1)) % bufferSize);
    }

    public State ComputeState(bool lhsBlocked, bool rhsBlocked, bool grounded)
    {
        var result = State.NONE;

        result |= lhsBlocked ? State.LHS_BLOCKED : State.NONE;
        result |= rhsBlocked ? State.RHS_BLOCKED : State.NONE;
        result |= grounded ? State.GROUNDED : State.NONE;

        return result;
    }
}
