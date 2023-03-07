using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PCHandMotor : MonoBehaviour
{
    public EventHandlerButton inputGrip;
    public EventHandlerButton inputThrow;
    public InputHandlerAnalogStick inputGesture;
    public VolumeTrigger grabTrigger;
    public GameObject pickupObject;
    public GameObject heldObject;
    public GameObject releasedObject;
    public CoreBody hand;
    public MovementHeldPickup movementHeldPickup;
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

                heldObject.SetActive(false);
                releasedObject.transform.position = heldObject.transform.position;
                releasedObject.SetActive(true);
                releasedBody.Trigger(hand.velocity);
                state &= ~HandState.GRIP;
                state |= HandState.BLOCKED;

                StartCoroutine(CoreUtilities.DelayedTask(interactionBlockDelay, () =>
                {
                    pickupObject.transform.position = releasedObject.transform.position;
                    pickupObject.SetActive(true);
                    pickupObject.GetComponent<CoreBody>().Trigger(releasedBody.velocity);
                    releasedObject.SetActive(false);
                    state &= ~HandState.BLOCKED;
                }));
            }
        }
    }

    public void OnInputGrip(InputButtonArgs args)
    {
        if (args.phase == InputActionPhase.Started)
        {
            if (grabTrigger.isActive)
            {
                if (state == HandState.NONE)
                {
                    heldObject.SetActive(true);
                    pickupObject.SetActive(false);
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
                releasedObject.transform.position = heldObject.transform.position;
                releasedObject.SetActive(true);
                heldObject.SetActive(false);
                state |= HandState.BLOCKED;
                state &= ~HandState.GRIP;

                StartCoroutine(CoreUtilities.DelayedTask(interactionBlockDelay, () =>
                {
                    pickupObject.transform.position = releasedObject.transform.position;
                    releasedObject.SetActive(false);
                    pickupObject.SetActive(true);
                    state &= ~HandState.BLOCKED;
                }));
            }
        }
    }
}
