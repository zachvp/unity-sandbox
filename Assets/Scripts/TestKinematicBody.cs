using UnityEngine;
using UnityEngine.InputSystem;

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
    public Collider2D checkCollider;
    public TriggerVolume triggerDown;

    public Vector2 velocity;
    public short speed = 50;
    public short jumpStrength = 200;
    public short gravity = 8;
    public Command command;

    public void Start()
    {
        //Physics2D.IgnoreCollision(checkCollider, attachedCollider);
        //Physics2D.IgnoreCollision(checkCollider, triggerDown.collider);
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

    public void FixedUpdate()
    {
        Move0();

        //var roundedPos = body.position;
        //roundedPos.Set(Mathf.RoundToInt(body.position.x), Mathf.RoundToInt(body.position.y));

        //body.MovePosition(roundedPos);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        var contacts = new ContactPoint2D[4];
        collision.GetContacts(contacts);
        for (var i = 0; i < collision.contactCount; i++)
        {
            var c = contacts[i];
            Debug.DrawRay(c.point, c.normal * 4, Color.green, 8);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        //foreach (var c in contacts)
        //{
        //    Debug.DrawRay(c.point, c.normal * 8, Color.blue);
        //}
    }

    private void Move0()
    {
        if (command.HasFlag(Command.JUMP))
        {
            velocity.y = jumpStrength;
            command ^= Command.JUMP;
        }
        else if (triggerDown.isTriggered)
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

        if (!triggerDown.isTriggered)
        {
            velocity.y -= gravity;
        }

        // check new pos for overlaps
        var newPos = body.position + velocity * Time.deltaTime;
        //checkCollider.transform.position = newPos;

        //var filter = new ContactFilter2D();
        //var overlappingObjects = new Collider2D[1];

        //filter.useLayerMask = true;
        //filter.layerMask = 1 << 8 | 1 << 9 | 1 << 11;

        //var isTriggered = checkCollider.OverlapCollider(filter, overlappingObjects) > 0;
        
        //if (isTriggered)
        //{
        //    Debug.Log($"check collider hit object, no movement: {overlappingObjects[0]}");
        //    velocity = Vector2.zero;
        //}
        //else
        //{
        //    body.MovePosition(newPos);
        //}

        body.MovePosition(newPos);

        if (Mathf.Abs(velocity.y) < 5 && !triggerDown.isTriggered)
        {
            Debug.Log($"y pos: {body.position.y}");
            Debug.DrawRay(body.position, Vector2.right * 16, Color.red, 8);
        }
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
