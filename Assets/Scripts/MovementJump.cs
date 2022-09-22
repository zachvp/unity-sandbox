using UnityEngine;
using UnityEngine.Events;
using System;

public class MovementJump : MonoBehaviour
{
    public short jump;
    public UnityEvent<short> movementEvent;
    public DataBool isAwake;

    public void Trigger(bool isActive)
    {
        if (isActive && isAwake.value)
        {
            movementEvent.Invoke(jump);
        }
    }
}
