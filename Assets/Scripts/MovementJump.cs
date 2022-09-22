using UnityEngine;
using UnityEngine.Events;
using System;

public class MovementJump : MonoBehaviour
{
    public int jump;
    public UnityEventInt intEvent;

    public void Trigger(bool isActive)
    {
        Debug.LogFormat("zvp: trigger jump; active: {0}", isActive);

        intEvent.Invoke(200);

        if (isActive)
        {
            //movementEvent.Invoke(jump);
        }
    }
}
