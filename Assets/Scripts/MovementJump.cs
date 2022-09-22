using UnityEngine;
using UnityEngine.Events;
using System;

public class MovementJump : MonoBehaviour
{
    public int jump;
    public UnityEvent<int> movementEvent;

    public void Trigger(bool isActive)
    {
        Debug.LogFormat("zvp: trigger jump; active: {0}", isActive);

        if (isActive)
        {
            movementEvent.Invoke(jump);
        }
    }
}
