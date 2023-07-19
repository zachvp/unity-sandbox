using UnityEngine.InputSystem;
using UnityEngine;

using System;
using UnityEngine.UI;

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
        JUMP_PHASE_1 = 1 << 4,
        JUMP_PHASE_2 = 1 << 5,
    }

    public Rigidbody2D body;
    public Collider2D attachedCollider;
    public TriggerVolume triggerDown;
    public bool isGrounded;
    //public Direction2D triggerDirection;
    public Command command;

    public Vector2 velocity;
    public Vector2 pastPos;

    public float speed = 5;
    public float jumpStrength = 10;
    public float gravity = 1;

    public void Start()
    {
        pastPos = body.position;
    }

    public void Update()
    {
        if (Keyboard.current.upArrowKey.wasPressedThisFrame && triggerDown.isTriggered)
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
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"collision with: {collision.collider}");
        var contacts = new ContactPoint2D[collision.contactCount];

        foreach (var c in contacts)
        {
            if (Vector2.Dot(c.normal, Vector2.up) > 0.9f)
            {
                isGrounded = true;
            }
        }
    }

    public void FixedUpdate()
    {
        Move0();

        //var roundedPos = body.position;
        //roundedPos.Set(Mathf.RoundToInt(body.position.x), Mathf.RoundToInt(body.position.y));

        //body.MovePosition(roundedPos);
    }

    private void Move0()
    {
        var newPos = body.position;

        if (command.HasFlag(Command.JUMP))
        {
            velocity.y = jumpStrength;
            command &= ~Command.JUMP;
        }
        else if (triggerDown.isTriggered)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y -= gravity;
        }

        if (command.HasFlag(Command.MOVE_RIGHT))
        {
            velocity.x = speed;
            velocity.y += 0.001f; // fudge y to avoid getting stuck on ground
            command &= ~Command.MOVE_RIGHT;
        }
        if (command.HasFlag(Command.MOVE_LEFT))
        {
            velocity.x = -speed;
            velocity.y += 0.001f; // fudge y to avoid getting stuck on ground
            command &= ~Command.MOVE_LEFT;
        }
        if (command.HasFlag(Command.MOVE_NONE))
        {
            velocity.x = 0;
            command &= ~Command.MOVE_NONE;
        }

        newPos += velocity * Time.fixedDeltaTime;
        pastPos = body.position;

        body.MovePosition(newPos);
        //body.position = newPos;

        //if (Mathf.Abs(newPos.sqrMagnitude - body.position.sqrMagnitude) > 0)
        //{
        //    var b = 0;
        //}

        //if (Mathf.Abs(velocity.y) < 5 && !triggerDirection.HasFlag(Direction2D.DOWN))
        //{
        //    Debug.Log($"y pos: {body.position.y}");
        //    Debug.DrawRay(body.position, Vector2.right * 16, Color.red, 8);
        //}
    }

    private void Move1()
    {
        if (command.HasFlag(Command.MOVE_RIGHT))
        {
            var newPos = body.position;

            newPos.x += 0.5f;

            body.MovePosition(newPos);
            command ^= Command.MOVE_RIGHT;
        }

        if (command.HasFlag(Command.MOVE_LEFT))
        {
            var newPos = body.position;

            newPos.x -= 0.5f;

            body.MovePosition(newPos);
            command ^= Command.MOVE_LEFT;
        }

        if (!triggerDown.isTriggered)
        {
            var newPos = body.position;

            newPos.y -= 0.75f;

            body.MovePosition(newPos);
        }

        if (command.HasFlag(Command.JUMP))
        {
            var newPos = body.position;

            newPos.y += 0.75f * 8 * Time.deltaTime;
            // todo: set position with curve
            command ^= Command.JUMP;
            command |= Command.JUMP_PHASE_1;

            body.MovePosition(newPos);
        }
    }
}
