using UnityEngine;
using System.Collections.Generic;

public class ActorStatePlatform : MonoBehaviour
{
    public TriggerVolume right;
    public TriggerVolume left;
    public TriggerVolume down;

    public Direction2D triggerState;
    public PlatformState platformState;

    public LinkedList<Direction2D> triggerStateBuffer;
    public float triggerStateBufferLifetime = 0.5f;

    // input state
    public float inputMove;

    public void Awake()
    {
        triggerStateBuffer = new LinkedList<Direction2D>();
    }

    public void Update()
    {
        triggerState = EnumHelper.FromBool(left.isTriggered, right.isTriggered, down.isTriggered, false);

        // new buffer
        var entry = triggerStateBuffer.AddLast(triggerState);

        // todo: make config value
        StartCoroutine(CoreUtilities.DelayedTask(triggerStateBufferLifetime, () =>
        {
            triggerStateBuffer.Remove(entry);
        }));
    }
}
