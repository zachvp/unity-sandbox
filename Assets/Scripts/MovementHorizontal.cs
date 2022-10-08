using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using System;

public class MovementHorizontal : MonoBehaviour
{
    public short speed;
    public GameObject[] targetsOfEvent;

    public bool Trigger(short input, bool leftBlocked, bool rightBlocked)
    {
        var triggered = input > 0 && !rightBlocked;
        triggered |= input < 0 && !leftBlocked;

        if (triggered)
        {
            foreach (var target in targetsOfEvent)
            {
                EventBus.Trigger(MoveEventUnit.EventHook, target, (short)(input * speed));
                //Debug.LogFormat("zvp: trigger move event on target: {0}", target);
            }
        }

        return triggered;
    }
}
