using UnityEngine;
using UnityEngine.InputSystem;

public class PCHandMotor : MonoBehaviour
{
    // TODO: separate pickup stuff into separate class
    public InputHandlerButton inputGrip;
    public InputHandlerButton inputThrow;
    public InputHandlerAnalogStick inputGesture;
    public TriggerVolume grabTrigger;
    public CoreBody hand;
    public Transform holdAnchor;
    public MovementRadial movementHeldPickup;
    public float interactionBlockDelay;
    public HandState state;
    public Trait grabbables;

    private Ball ball;

    public void Awake()
    {
        inputGrip.actionDelegate += OnInputGrip;
        inputThrow.actionDelegate += OnInputThrow;
        inputGesture.actionDelegate += OnInputGesture;
    }

    public void OnInputGesture(InputAxis2DArgs args)
    {
        movementHeldPickup.Trigger(args.axis);
    }

    public void OnInputThrow(InputButtonArgs args)
    {
        if (args.phase == InputActionPhase.Started)
        {
            if (state == HandState.GRIP)
            {
                ball.Throw(hand.velocity);

                state &= ~HandState.GRIP;
                state |= HandState.BLOCKED;

                StartCoroutine(CoreUtilities.DelayedTask(interactionBlockDelay, () =>
                {
                    ball.ThrowReset();
                    state &= ~HandState.BLOCKED;
                }));
            }
        }
    }

    public void OnInputGrip(InputButtonArgs args)
    {
        if (args.phase == InputActionPhase.Started)
        {
            if (grabTrigger.isTriggered)
            {
                ball = grabTrigger.overlappingObjects[0].GetComponent<Ball>();
                Debug.Log($"grab ball: {ball}");

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
                    ball.ReleaseReset();
                    state &= ~HandState.BLOCKED;
                }));
            }
        }
    }
}
