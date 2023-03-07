using UnityEngine;
using System;
using System.Collections;


// todo: rename to platform actor state
public class PlayerCharacterState : MonoBehaviour
{
    public VolumeTrigger right;
    public VolumeTrigger left;
    public VolumeTrigger down;

    public Direction2D triggerState;
    public PlatformState platformState;
    public Buffer<Direction2D> triggerStateBuffer;

    public Vector2 lastGroundVelocity;

    public void Start()
    {
        StartCoroutine(CoreUtilities.RepeatTask(triggerStateBuffer.interval, () =>
        {
                triggerState = EnumHelper.FromBool(left.isActive, right.isActive, down.isActive, false);
                triggerStateBuffer.Store(triggerState);
        }));
    }

    public bool BufferContainsState(Direction2D included)
    {
        return BufferContainsState(included, Direction2D.NONE);
    }

    public bool BufferContainsState(Direction2D included, Direction2D excluded)
    {
        for (var i = 0; i < triggerStateBuffer.values.Length; i++)
        {
            if ((triggerStateBuffer.values[i] & included) > 0 && (triggerStateBuffer.values[i] & excluded) == 0)
            {
                return true;
            }
        }

        return false;
    }
}
