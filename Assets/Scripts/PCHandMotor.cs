using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PCHandMotor : MonoBehaviour
{
    // TODO: separate pickup stuff into separate class
    public TriggerVolume grabTrigger;
    public CoreBody hand;
    public PCPlatformMotor motor;
    public Transform holdAnchor;
    public MovementRadial movementHeldPickup;
    public float interactionBlockDelay;
    public State state;

    public void Awake()
    {
        Notifications.CommandPC += HandleCommand;
    }

    public void HandleCommand(PCInputArgs args)
    {
        var ball = SceneRefs.Instance.ball;

        switch (args.type)
        {
            case CoreActionMap.Player.MOVE_HAND:
                movementHeldPickup.Trigger(args.vVec2);
                break;
            case CoreActionMap.Player.THROW:
                if (state == State.GRIP)
                {
                    ball.Throw(hand.velocity, args.vVec2, motor);

                    state &= ~State.GRIP;
                    state |= State.BLOCKED;

                    StartCoroutine(CoreUtilities.DelayedTask(interactionBlockDelay, () =>
                    {
                        state &= ~State.BLOCKED;
                        ball.ThrowReset();

                    }));
                }
                break;
            case CoreActionMap.Player.GRIP:
                if (grabTrigger.isTriggered)
                {
                    // todo: this seems hacky - maybe have singleton SceneRefs to directly access ball.
                    ball = grabTrigger.overlappingObjects[0].GetComponentInParent<Ball>();

                    if (ball != null && state == State.NONE)
                    {
                        ball.Grab(holdAnchor);

                        state |= State.BLOCKED;
                        state |= State.GRIP;

                        StartCoroutine(CoreUtilities.DelayedTask(interactionBlockDelay, () =>
                        {
                            state &= ~State.BLOCKED;
                        }));
                    }
                }

                if (ball && state == State.GRIP)
                {
                    ball.Release();

                    state |= State.BLOCKED;
                    state &= ~State.GRIP;

                    StartCoroutine(CoreUtilities.DelayedTask(interactionBlockDelay, () =>
                    {
                        state &= ~State.BLOCKED;
                        ball.ReleaseReset();

                    }));
                }
                break;
        }
    }

    [Flags]
    public enum State
    {
        NONE = 0,
        GRIP = 1 << 0,
        BLOCKED = 1 << 1
    }
}
