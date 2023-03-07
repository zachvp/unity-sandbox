using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using System;

public class PlayerCharacterActions : MonoBehaviour
{
    public CoreBody body;
    public PlayerCharacterState state;
    public EventHandlerHeldAxis inputMove;
    public EventHandlerButton inputJump;
    public short jumpStrength = 100;
    public Vector2Int wallJumpSpeed = new Vector2Int(40, 80);
    public short groundMoveSpeed = 100;
    public short airMoveSpeed = 70;
    public float wallJumpDelay = 0.1f;

    public void Awake()
    {
        EventBus.Register<InputButtonArgs>(InputButtonEvent.Hook, OnInputButton);
        EventBus.Register<InputAxis1DArgs>(InputAxis1DEvent.Hook, OnInputAxis1D);
    }

    public void Update()
    {
        // todo: check input data each frame rather than splitting into events.

        if (state.down.isActive)
        {
            state.lastGroundVelocity = body.velocity;
        }
    }

    public void OnInputButton(InputButtonArgs args)
    {
        switch(args.action)
        {
            case CustomInputAction.JUMP:
                switch(args.phase)
                {
                    case InputActionPhase.Started:
                        // ground jump
                        if (state.down.isActive)
                        {
                            body.TriggerY(jumpStrength);
                        }
                        // wall jump
                        else if (state.BufferContainsState(Direction2D.RIGHT | Direction2D.LEFT))
                        {
                            var velocity = wallJumpSpeed;
                            velocity.x *= inputMove.args.axis;

                            body.Trigger(velocity);

                            state.platformState |= PlatformState.WALL_JUMP;
                            StartCoroutine(CoreUtilities.DelayedTask(wallJumpDelay, () =>
                            {
                                if (state.platformState.HasFlag(PlatformState.WALL_JUMP))
                                {
                                    state.platformState ^= PlatformState.WALL_JUMP;
                                }
                            }));
                        }
                        break;
                }
                break;
        }
    }

    public void OnInputAxis1D(InputAxis1DArgs args)
    {
        switch (args.action)
        {
            case (CustomInputAction.MOVE):
                if (state.down.isActive)
                {
                    // walk
                    body.TriggerX((short)(groundMoveSpeed * args.axis));
                }
                else
                {
                    // wall cling & release
                    if (TriggerWallCling(state.triggerState, args.axis, body))
                    {
                        body.StopVertical();
                    }
                    else
                    {
                        body.Reset();
                    }

                    // todo: implement air movement
                    if (Mathf.Abs(args.axis) > 0 && !state.platformState.HasFlag(PlatformState.WALL_JUMP))
                    {
                        body.TriggerX((short)(airMoveSpeed * args.axis));
                    }
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
