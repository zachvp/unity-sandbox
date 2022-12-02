using UnityEngine;
using System;
using System.Collections;


// todo: rename to platform actor state
public class PlayerCharacterState : MonoBehaviour
{
    public VolumeTrigger right;
    public VolumeTrigger left;
    public VolumeTrigger down;

    [Flags]
    public enum State
    {
        NONE = 0,
        LHS_BLOCKED = 1,
        RHS_BLOCKED = 1 << 1,
        GROUNDED = 1 << 2
    }

    public State stateCurrent;
    public Buffer<State> stateBuffer;

    public Direction2D directionFace;

    public void Update()
    {
        stateCurrent = ComputeState(left.isActive, right.isActive, down.isActive);

        // todo: should be frame independent? e.g. does this window depend on FPS?
        stateBuffer.Store(stateCurrent);
    }

    public bool BufferContainsState(State includeState, State excludeState)
    {
        for (var i = 0; i < stateBuffer.values.Length; i++)
        {
            if ((stateBuffer.values[i] & includeState) > 0 && (stateBuffer.values[i] & excludeState) == 0)
            {
                return true;
            }
        }

        return false;
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
