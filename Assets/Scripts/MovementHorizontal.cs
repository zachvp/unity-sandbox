using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MovementHorizontal : MonoBehaviour
{
    public short speed;
    public GameObject targetOfEvent;

    public void Trigger(short input)
    {
        EventBus.Trigger(MoveEventUnit.EventHook, targetOfEvent, (short)(input * speed));
    }
}
