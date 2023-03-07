using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

public class TestKinematicBody : MonoBehaviour
{
    [Flags]
    public enum Command
    {
        NONE = 0,
        JUMP = 1 << 0,
        MOVE_LEFT = 1 << 1,
        MOVE_RIGHT = 1 << 2,
        MOVE_NONE = 1 << 3,
        ADJUST = 1 << 4
    }

    public Rigidbody2D body;
    public Collider2D attachedCollider;
    public LayerMask mask;
    public TriggerVolume triggerDown;

    public short speed = 50;
    public short jumpStrength = 200;
    public short maxSpeed = 100;
    public short gravity = 8;
    public Direction2D collisionDirection;
    public Command command;

    public Vector2 velocity;
    public Vector2 adjustPosition;

    public float maxHeight;
    

    public void Update()
    {
        collisionDirection = EnumHelper.FromBool(false, false, triggerDown.isTriggered, false);

        if (Keyboard.current.upArrowKey.wasPressedThisFrame && collisionDirection.HasFlag(Direction2D.DOWN))
        {
            command |= Command.JUMP;
        }
        
        // horizontal
        if (Keyboard.current.rightArrowKey.isPressed && !Keyboard.current.leftArrowKey.isPressed)
        {
            command |= Command.MOVE_RIGHT;
        }
        else if (Keyboard.current.leftArrowKey.isPressed && !Keyboard.current.rightArrowKey.isPressed)
        {
            command |= Command.MOVE_LEFT;
        }
        else
        {
            command |= Command.MOVE_NONE;
        }

        maxHeight = Mathf.Max(maxHeight, transform.position.y);
    }

    public void FixedUpdate()
    {
        if (command.HasFlag(Command.JUMP))
        {
            velocity.y = jumpStrength;
            command ^= Command.JUMP;
        } else if (collisionDirection.HasFlag(Direction2D.DOWN))
        {
            velocity.y = 0;
        }

        if (command.HasFlag(Command.MOVE_RIGHT))
        {
            velocity.x = speed;
            command ^= Command.MOVE_RIGHT;
        }
        if (command.HasFlag(Command.MOVE_LEFT))
        {
            velocity.x = -speed;
            command ^= Command.MOVE_LEFT;
        }
        if (command.HasFlag(Command.MOVE_NONE))
        {
            velocity.x = 0;
            command ^= Command.MOVE_NONE;
        }

        if (!collisionDirection.HasFlag(Direction2D.DOWN))
        {
            velocity.y -= gravity;
        }

        if (command.HasFlag(Command.ADJUST))
        {
            velocity += adjustPosition;
            command ^= Command.ADJUST;
        }

        var newPos = body.position + velocity * Time.fixedDeltaTime;

        body.MovePosition(newPos);
    }
}
