using UnityEngine;
using UnityEngine.Events;
using System;

public class MovementJump : MonoBehaviour
{
    public short jump;
    public UnityEvent<short> movementEvent;

    public void Trigger(bool isActive)
    {
        if (isActive)
        {
            movementEvent.Invoke(jump);
        }
    }
}
