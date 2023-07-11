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
    public HandState state;
    public Trait grabbables;

    private Ball ball;

    public void Awake()
    {
        EventBus.Register<PCInputArgs>(CommandEvent.Hook, HandleCommand);
    }

    public void HandleCommand(PCInputArgs args)
    {
        switch (args.type)
        {
            case CoreActionMap.Player.MOVE_HAND:
                movementHeldPickup.Trigger(args.vVec2);
                break;
            case CoreActionMap.Player.THROW:
                if (state == HandState.GRIP)
                {
                    ball.Throw(hand.velocity, args.vVec2, motor);

                    state &= ~HandState.GRIP;
                    state |= HandState.BLOCKED;

                    StartCoroutine(CoreUtilities.DelayedTask(interactionBlockDelay, () =>
                    {
                        state &= ~HandState.BLOCKED;
                        ball.ThrowReset();

                    }));
                }
                break;
            case CoreActionMap.Player.GRIP:
                if (grabTrigger.isTriggered)
                {
                    // todo: this seems hacky - maybe have singleton SceneRefs to directly access ball.
                    ball = grabTrigger.overlappingObjects[0].GetComponentInParent<Ball>();

                    if (ball != null && state == HandState.NONE)
                    {
                        ball.Grab(holdAnchor);

                        state |= HandState.BLOCKED;
                        state |= HandState.GRIP;

                        StartCoroutine(CoreUtilities.DelayedTask(interactionBlockDelay, () =>
                        {
                            state &= ~HandState.BLOCKED;
                        }));
                    }
                }

                if (ball && state == HandState.GRIP)
                {
                    ball.Release();

                    state |= HandState.BLOCKED;
                    state &= ~HandState.GRIP;

                    StartCoroutine(CoreUtilities.DelayedTask(interactionBlockDelay, () =>
                    {
                        state &= ~HandState.BLOCKED;
                        ball.ReleaseReset();

                    }));
                }
                break;
        }
    }
}
