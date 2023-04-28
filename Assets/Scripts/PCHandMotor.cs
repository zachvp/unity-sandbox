using UnityEngine;
using UnityEngine.InputSystem;

public class PCHandMotor : MonoBehaviour
{
    // TODO: separate pickup stuff into separate class
    public InputHandlerButton inputGrip;
    public InputHandlerButton inputThrow;
    public InputHandlerAnalogStick inputGesture;
    public TriggerVolume grabTrigger;
    public TriggerVolume heldTrigger;
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
            if (state == HandState.GRIP && !heldTrigger.isTriggered)
            {
                Debug.DrawRay(transform.position, inputGesture.args.axis * 32, Color.yellow, 12);
                ball.Throw(hand.velocity, inputGesture.args.axis, motor);

                state &= ~HandState.GRIP;
                state |= HandState.BLOCKED;
                
                StartCoroutine(CoreUtilities.DelayedTask(interactionBlockDelay, () =>
                {
                    state &= ~HandState.BLOCKED;
                    ball.ThrowReset();

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
                //grabTrigger.overlappingObjects[0].GetComponent<Ball>();
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

            if (ball && state == HandState.GRIP && !heldTrigger.isTriggered)
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
        }
    }
}
