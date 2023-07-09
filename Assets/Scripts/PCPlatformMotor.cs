using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;
using static TestKinematicBody;

public class PCPlatformMotor : MonoBehaviour
{
    // -- read vars
    public CoreBody body;
    public ActorStatePlatform state;

    public float jumpStrength = 100;
    public float groundMoveSpeed = 100;
    public float airMoveSpeed = 25;
    public float maxSpeedX = 200;
    public float wallJumpDelay = 0.1f;
    public Vector2 wallJumpSpeed = new Vector2Int(40, 80);

    // -- write vars
    public float adjustedVelocityX;

    public void Awake()
    {
        EventBus.Register<PCInputArgs>(CommandEvent.Hook, HandleCommand);
    }

    public void HandleCommand(PCInputArgs args)
    {
        // todo: distinguish between player IDs
        // maybe register to receive commands for a specific player
        // call singleton command handler, pass in reference to self in the form of a command processing interface

        // todo: separate state computation to separate class, then feed state result each frame to motor?

        switch (args.type)
        {
            case CoreActionMapPlayer.JUMP:
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
                break;

            case CoreActionMapPlayer.MOVE:
                state.inputMove = args.value.vFloat;

                if (!state.platformState.HasFlag(PlatformState.DISABLE_MOVE))
                {
                    //if (Mathf.Abs(inputMove.args.axis) > 0)
                    if (Mathf.Abs(state.inputMove) > CoreConstants.FLOAT_DEADZONE)
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
                break;

        }
    }

    public void Update()
    {
        // wall cling & release
        if (!state.down.isTriggered)
        {
            if (TriggerWallCling(state.triggerState, state.inputMove, body))
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
                //adjustedVelocityX = (groundMoveSpeed * inputMove.args.axis);
                adjustedVelocityX = groundMoveSpeed * state.inputMove;
            }
            else
            {
                //adjustedVelocityX = airMoveSpeed * inputMove.args.axis;
                adjustedVelocityX = groundMoveSpeed * state.inputMove;
            }
        }
        else if (state.platformState.HasFlag(PlatformState.MOVE_NEUTRAL))
        {
            adjustedVelocityX = 0;
        }

        if (state.platformState.HasFlag(PlatformState.JUMP))
        {
            body.TriggerY(jumpStrength);

            state.platformState &= ~PlatformState.JUMP;
        }
        else if (state.platformState.HasFlag(PlatformState.WALL_JUMP))
        {
            var velocity = wallJumpSpeed;
            velocity.x *= state.inputMove;
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
            body.ResetVertical();
            state.platformState &= ~PlatformState.WALL_RELEASE;
            state.platformState &= ~PlatformState.WALL_CLING;
        }

        adjustedVelocityX = Mathf.Clamp(adjustedVelocityX, -maxSpeedX, maxSpeedX);

        if (Math.Abs(adjustedVelocityX) > 0)
        {
            body.TriggerX(adjustedVelocityX);
        }
    }

    public bool TriggerWallCling(Direction2D triggerState, float inputAxis, CoreBody body)
    {
        // check if next to a wall
        var rightCondition = inputAxis > CoreConstants.FLOAT_DEADZONE && triggerState.HasFlag(Direction2D.RIGHT);
        var leftCondition = inputAxis < -CoreConstants.FLOAT_DEADZONE && triggerState.HasFlag(Direction2D.LEFT);

        // check if input pushes off
        var result = Mathf.Abs(inputAxis) > CoreConstants.FLOAT_DEADZONE &&
                     !triggerState.HasFlag(Direction2D.DOWN) &&
                     Mathf.Abs(body.velocity.y) < CoreConstants.VELOCITY_DEADZONE;

        result &= leftCondition || rightCondition;

        return result;
    }
}
