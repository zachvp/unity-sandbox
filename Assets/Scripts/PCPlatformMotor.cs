using UnityEngine;
using UnityEngine.InputSystem;
using System;

// TODO: SWITCH FROM SHORT TO INT -- =OOOO 

public class PCPlatformMotor : MonoBehaviour
{
    // -- read vars
    public CoreBody body;
    public ActorStatePlatform state;
    public InputHandlerDigitalAxis1D inputMove;
    public InputHandlerButton inputJump;
    public float jumpStrength = 100;
    public float groundMoveSpeed = 100;
    public float airMoveSpeed = 70;
    public float wallJumpDelay = 0.1f;
    public Vector2Int wallJumpSpeed = new Vector2Int(40, 80);

    // -- write vars
    public float adjustedVelocityX;

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

        // grounded
        if (state.down.isTriggered)
        {
            state.lastGroundVelocity = body.velocity;
        }
        else
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
            if (state.down.isTriggered)
            {
                adjustedVelocityX = (groundMoveSpeed * inputMove.args.axis);
                //body.TriggerX((short)(groundMoveSpeed * inputMove.args.axis));
            }
            else
            {
                adjustedVelocityX += airMoveSpeed * inputMove.args.axis;
                //body.TriggerX((short)(airMoveSpeed * inputMove.args.axis));
            }
        }
        else if (state.platformState.HasFlag(PlatformState.MOVE_NEUTRAL))
        {
            adjustedVelocityX = 0;
        }

        if (state.platformState.HasFlag(PlatformState.JUMP))
        {
            //Debug.Log($"set jump velocity: {velocity.y}");
            body.TriggerY(jumpStrength);

            state.platformState &= ~PlatformState.JUMP;
        }
        else if (state.platformState.HasFlag(PlatformState.WALL_JUMP))
        {
            var velocity = wallJumpSpeed;
            velocity.x *= inputMove.args.axis;
            body.TriggerY(velocity.y);
            adjustedVelocityX = velocity.x;

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

        adjustedVelocityX = Mathf.Clamp(adjustedVelocityX, -1.1f*groundMoveSpeed, 1.1f*groundMoveSpeed);

        if (Math.Abs(adjustedVelocityX) > 0)
        {
            body.TriggerX(adjustedVelocityX);
        }
    }

    public void OnInputJump(InputButtonArgs args)
    {
        if (args.phase == InputActionPhase.Started)
        {
            // ground jump
            if (state.down.isTriggered)
            {
                state.platformState |= PlatformState.JUMP;
                //Debug.Log($"enter jump state");
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
        }
    }

    public bool TriggerWallCling(Direction2D triggerState, short inputAxis, CoreBody body)
    {
        // check if next to a wall
        var rightCondition = inputAxis > 0 && triggerState.HasFlag(Direction2D.RIGHT);
        var leftCondition = inputAxis < 0 && triggerState.HasFlag(Direction2D.LEFT);

        // check if input pushes off
        var result = Mathf.Abs(inputAxis) > 0 && !triggerState.HasFlag(Direction2D.DOWN) && body.velocity.y < 1;

        result &= leftCondition || rightCondition;

        return result;
    }
}
