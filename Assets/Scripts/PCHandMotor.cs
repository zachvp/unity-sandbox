using UnityEngine;
using UnityEngine.InputSystem;

public class PCHandMotor : MonoBehaviour
{
    // TODO: separate pickup stuff into separate class
    public InputHandlerButton inputGrip;
    public InputHandlerButton inputThrow;
    public InputHandlerAnalogStick inputGesture;
    public TriggerVolume grabTrigger;
    public Rigidbody2D pickupObject;
    public Rigidbody2D heldObject;
    public Rigidbody2D releasedObject;
    public CoreBody hand;
    public MovementRadial movementHeldPickup;
    public float interactionBlockDelay;
    public HandState state;

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
                var releasedBody = releasedObject.GetComponent<CoreBody>();

                heldObject.gameObject.SetActive(false);
                releasedObject.transform.position = heldObject.transform.position;
                releasedObject.gameObject.SetActive(true);
                releasedBody.Trigger(hand.velocity);

                state &= ~HandState.GRIP;
                state |= HandState.BLOCKED;

                StartCoroutine(CoreUtilities.DelayedTask(interactionBlockDelay, () =>
                {
                    pickupObject.transform.position = releasedObject.transform.position;
                    pickupObject.gameObject.SetActive(true);
                    pickupObject.GetComponent<CoreBody>().Trigger(releasedBody.velocity);
                    releasedObject.gameObject.SetActive(false);

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
                if (state == HandState.NONE)
                {
                    heldObject.gameObject.SetActive(true);
                    heldObject.rotation = pickupObject.rotation;
                    pickupObject.gameObject.SetActive(false);

                    state |= HandState.BLOCKED;
                    state |= HandState.GRIP;

                    StartCoroutine(CoreUtilities.DelayedTask(interactionBlockDelay, () =>
                    {
                        state &= ~HandState.BLOCKED;
                    }));
                }
            }

            if (state == HandState.GRIP)
            {
                releasedObject.gameObject.SetActive(true);
                heldObject.gameObject.SetActive(false);
                releasedObject.transform.position = heldObject.transform.position;
                releasedObject.rotation = heldObject.rotation;

                state |= HandState.BLOCKED;
                state &= ~HandState.GRIP;

                StartCoroutine(CoreUtilities.DelayedTask(interactionBlockDelay, () =>
                {
                    releasedObject.gameObject.SetActive(false);
                    pickupObject.gameObject.SetActive(true);
                    pickupObject.transform.position = releasedObject.transform.position;
                    pickupObject.rotation = releasedObject.rotation;

                    state &= ~HandState.BLOCKED;
                }));
            }
        }
    }
}
