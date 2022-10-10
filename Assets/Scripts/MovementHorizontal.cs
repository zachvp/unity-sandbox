using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using System;
using static UnityEngine.GraphicsBuffer;

public class MovementHorizontal : MonoBehaviour
{
    public short speed;

    public bool Trigger(short input, bool leftBlocked, bool rightBlocked)
    {
        var triggered = input > 0 && !rightBlocked;
        triggered |= input < 0 && !leftBlocked;

        if (triggered)
        {
            EventBus.Trigger(MoveEventUnit.EventHook, gameObject, (short)(input * speed));
        }

        return triggered;
    }
}
