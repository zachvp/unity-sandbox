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
    public Buffer<Direction2D> triggerStateBuffer;

    public Direction2D directionFace;

    public void Update()
    {
        triggerState = EnumHelper.FromBool(left.isActive, right.isActive, down.isActive, false);

        // todo: should be frame independent? e.g. does this window depend on FPS?
        triggerStateBuffer.Store(triggerState);
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
