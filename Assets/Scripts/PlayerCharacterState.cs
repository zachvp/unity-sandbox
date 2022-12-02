using UnityEngine;
using System;
using System.Collections;


// todo: rename to platform actor state
public class PlayerCharacterState : MonoBehaviour
{
    public VolumeTrigger right;
    public VolumeTrigger left;
    public VolumeTrigger down;

    public Direction2D stateTrigger;
    public Buffer<Direction2D> stateBuffer;

    public Direction2D directionFace;

    public void Update()
    {
        stateTrigger = EnumHelper.FromBool(left.isActive, right.isActive, down.isActive, false);

        // todo: should be frame independent? e.g. does this window depend on FPS?
        stateBuffer.Store(stateTrigger);
    }

    public bool BufferContainsState(Direction2D included, Direction2D excluded)
    {
        for (var i = 0; i < stateBuffer.values.Length; i++)
        {
            if ((stateBuffer.values[i] & included) > 0 && (stateBuffer.values[i] & excluded) == 0)
            {
                return true;
            }
        }

        return false;
    }
}
