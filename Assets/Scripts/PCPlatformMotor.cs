using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using System;

public class PCPlatformMotor : MonoBehaviour
{
    public CoreBody body;
    public PlayerCharacterState state;
    public EventHandlerHeldAxis inputMove;
    public EventHandlerButton inputJump;
    public short jumpStrength = 100;
    public short groundMoveSpeed = 100;
    public short airMoveSpeed = 70;
    public float wallJumpDelay = 0.1f;
    public Vector2Int wallJumpSpeed = new Vector2Int(40, 80);

    public void Awake()
    {
        inputJump.actionDelegate += OnInputJump;
    }

    public void Update()
    {
        // todo: check input data each frame rather than splitting into events.

        // movement
        if (!state.platformState.HasFlag(PlatformState.DISABLE_MOVE))
        {
            if (Mathf.Abs(inputMove.args.axis) > 0)
            {
                state.platformState |= PlatformState.MOVE;
                state.platformState &= ~PlatformState.MOVE_NEUTRAL;
            }
            else
            {
                state.platformState |= PlatformState.MOVE_NEUTRAL;
                state.platformState &= ~PlatformState.MOVE;
            }
        }

        // wall cling
        if (!state.down.isActive)
        {
            // wall cling & release
            if (TriggerWallCling(state.triggerState, inputMove.args.axis, body))
            {
                state.platformState |= PlatformState.WALL_CLING;
                state.platformState &= ~PlatformState.WALL_RELEASE;
            }
            else
            {
                state.platformState |= PlatformState.WALL_RELEASE;
                state.platformState &= ~PlatformState.WALL_CLING;
            }
        }

        // todo: implement air movement
        if (state.platformState.HasFlag(PlatformState.MOVE))
        {
            if (state.down.isActive)
            {
                body.TriggerX((short)(groundMoveSpeed * inputMove.args.axis));
            }
            else
            {
                body.TriggerX((short)(airMoveSpeed * inputMove.args.axis));
            }
        }

        if (state.platformState.HasFlag(PlatformState.JUMP))
        {
            body.TriggerY(jumpStrength);

            state.platformState &= ~PlatformState.JUMP;
        }
        else if (state.platformState.HasFlag(PlatformState.WALL_JUMP))
        {
            var velocity = wallJumpSpeed;
            velocity.x *= inputMove.args.axis;

            body.Trigger(velocity);
            state.platformState &= ~PlatformState.WALL_JUMP;
        }

        // wall cling/release
        if (state.platformState.HasFlag(PlatformState.WALL_CLING))
        {
            body.StopVertical();
        }
        else if (state.platformState.HasFlag(PlatformState.WALL_RELEASE))
        {
            body.Reset();
            state.platformState &= ~PlatformState.WALL_RELEASE;
            state.platformState &= ~PlatformState.WALL_CLING;
        }

        if (state.down.isActive)
        {
            state.lastGroundVelocity = body.velocity;
        }
    }

    public void OnInputJump(InputButtonArgs args)
    {
        switch (args.phase)
        {
            case InputActionPhase.Started:
                // ground jump
                if (state.down.isActive)
                {
                    state.platformState |= PlatformState.JUMP;
                }

                // wall jump
                else if (state.BufferContainsState(Direction2D.RIGHT | Direction2D.LEFT))
                {
                    state.platformState |= PlatformState.WALL_JUMP;
                    state.platformState |= PlatformState.DISABLE_MOVE;
                    state.platformState &= ~PlatformState.MOVE;

                    StartCoroutine(CoreUtilities.DelayedTask(wallJumpDelay, () =>
                    {
                        state.platformState &= ~PlatformState.DISABLE_MOVE;
                    }));
                }
                break;
        }
    }

    public bool TriggerWallCling(Direction2D triggerState, short inputAxis, CoreBody body)
    {
        var rightCondition = inputAxis > 0 && triggerState.HasFlag(Direction2D.RIGHT);
        var leftCondition = inputAxis < 0 && triggerState.HasFlag(Direction2D.LEFT);
        var result = Mathf.Abs(inputAxis) > 0 && !triggerState.HasFlag(Direction2D.DOWN) && body.velocity.y < 1;

        result &= leftCondition || rightCondition;

        return result;
    }
}
