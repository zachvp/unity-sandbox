using UnityEngine;
using UnityEngine.Events;
using System;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;

public class MovementJump : MonoBehaviour
{
    public short jump;

    public void Trigger(bool isActive, bool isGrounded)
    {
        if (isActive && isGrounded)
        {
            EventBus.Trigger(JumpEventUnit.EventHook, gameObject, jump);
        }
    }
}
